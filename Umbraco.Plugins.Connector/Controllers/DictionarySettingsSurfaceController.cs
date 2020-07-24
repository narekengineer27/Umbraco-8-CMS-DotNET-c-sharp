namespace Umbraco.Plugins.Connector.Controllers
{
    using System;
    using System.IO;
    using System.Web;
    using System.Linq;
    using OfficeOpenXml;
    using System.Web.Mvc;
    using Umbraco.Web.Mvc;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Services;
    using System.Collections.Generic;
    using Umbraco.Plugins.Connector.Models;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Services;
    using Umbraco.Plugins.Connector.Dictionaries;
    using Umbraco.Core.Models;

    /// <summary>
    /// Summary description for ApiSettingsSurfaceController
    /// </summary>
    public class DictionarySettingsSurfaceController : SurfaceController
    {
        private readonly LanguageDictionaryService languageDictionaryService;
        private readonly ILocalizationService localizationService;

        public DictionarySettingsSurfaceController(ILocalizationService localizationService, IDomainService domainService, ILogger logger)
        {
            this.localizationService = localizationService;
            languageDictionaryService = new LanguageDictionaryService(localizationService, domainService, logger);
        }

        [HttpGet]
        public void DeleteAll()
        {
            languageDictionaryService.DeleteAllDictionaryItems();
        }

        [HttpGet]
        public FileContentResult ExportToExcel()
        {
            var items = languageDictionaryService.GetAllDictionaryItems();
            MemoryStream stream = null;
            var locale = "en";

            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("Dictionary");
                var excelWorksheet = excel.Workbook.Worksheets["Dictionary"];
                List<string[]> headerRow = ExportHeadersToList();

                string headerRange = "A1:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";
                excelWorksheet.Cells[headerRange].LoadFromArrays(headerRow);
                excelWorksheet.Cells[2, 1].LoadFromArrays(ExportToList(items, locale));
                stream = new MemoryStream(excel.GetAsByteArray());
            }
            return File(stream.ToArray(), "application/vnd.ms-excel", $"Total-Code-Umbraco-Dictionary-Items-{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.xlsx");
        }

        [HttpGet]
        public JsonResult RunAudit()
        {
            var registered = languageDictionaryService.GetAllDictionaryItems();
            var results = FileSystemHelper.GetAllRazorDictionaryReferences(registered);
            return base.Json(new
            {
                Success = results.Count > 0,
                count = results.Count,
                results
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create(AuditDictionaryItem item)
        {
            var languages = localizationService.GetAllLanguages();
            var mainLanguage = localizationService.GetDefaultLanguageIsoCode();
            var ok = false;

            var mainLanguageDictionary = new ImportExportDictionaryItem
            {
                Key = item.Key,
                ParentKey = item.ParentKey,
                Value = string.Empty,
                LanguageCode = mainLanguage
            };
            mainLanguageDictionary.Translations = new Dictionary<string, string>();

            if (!languageDictionaryService.ParentExists(item.ParentKey))
            {
                var parentDictionaryItem = new ImportExportDictionaryItem
                {
                    Key = item.ParentKey,
                    Value = item.Key,
                    LanguageCode = mainLanguage,
                    Translations = new Dictionary<string, string>()
                };
                languageDictionaryService.CreateDictionaryItem(parentDictionaryItem);
            }

            foreach (var language in languages)
            {
                if (language.IsoCode != mainLanguage)
                {
                    mainLanguageDictionary.Translations.Add(language.IsoCode, string.Empty);
                }
            }
            var result = languageDictionaryService.CreateDictionaryItem(mainLanguageDictionary);
            if (result.Item2) ok = true;
            else ok = false;
            return base.Json(new { Created = ok }, JsonRequestBehavior.DenyGet);
        }

        public JsonResult GetDictionaries()
        {
            var dictionaryItems = languageDictionaryService.GetAllDisplayDictionaryItems().ToList().OrderBy(x => x.Id);
            return base.Json(new { dictionaryItems, status = "OK" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ImportDictionaries(HttpPostedFileBase file, bool overrideExisting = false)
        {
            var entries = new ImportExportDictionaryItems();
            List<ImportResult> results = new List<ImportResult>();

            if (file?.ContentLength > 0)
            {
                const int RowStart = 2; // ignore headers
                ImportDigest excelDigest = null;
                var import = new List<ExcelCell>();
                bool importSuccessful = false;

                excelDigest = new ImportDigest(file.InputStream);
                import = excelDigest.Import();
                importSuccessful = excelDigest.ErrorList.Errors.Count == 0;
                //var languages = localizationService.GetAllLanguages();

                if (importSuccessful)
                {
                    for (int i = RowStart; i <= excelDigest.RowsImported; i++)
                    {
                        var current = new ImportExportDictionaryItem
                        {
                            ParentKey = import.Find(x => x.CellName == $"A{i}").CellValue?.ToString(),
                            Key = import.Find(x => x.CellName == $"B{i}").CellValue?.ToString(),
                            Value = import.Find(x => x.CellName == $"C{i}").CellValue != null ? import.Find(x => x.CellName == $"C{i}").CellValue
                            .ToString()
                            .Trim()
                            .Replace("\n", string.Empty)
                            .Replace("\t",string.Empty)
                            : string.Empty,
                            LanguageCode = import.Find(x => x.CellName == $"D{i}").CellValue?.ToString()
                        };
                        var previous = entries.Items.Count > 0 ? entries[current.Key] : null;

                        if (previous != null)
                        {
                            if (current.ParentKey.Equals(previous.Key) || current.ParentKey.Equals(previous.ParentKey))
                            {
                                if (current.Key.Equals(previous.Key))
                                {
                                    if (current.LanguageCode.Equals(previous.LanguageCode))
                                    {
                                        if (!string.IsNullOrEmpty(current.Value) && !string.IsNullOrEmpty(previous.Value))
                                        {
                                            if (!current.Value.Equals(previous.Value))
                                            {
                                                previous.Value = current.Value;
                                            }
                                        }
                                        else if (!string.IsNullOrEmpty(current.Value) && string.IsNullOrEmpty(previous.Value))
                                        {
                                            previous.Value = current.Value;
                                        }
                                        else if (string.IsNullOrEmpty(current.Value) && string.IsNullOrEmpty(previous.Value))
                                        {
                                            previous.Value = string.Empty;
                                        }

                                        entries[current.Key] = current;
                                    }
                                    else
                                    {
                                        previous.Translations = previous.Translations ?? new Dictionary<string, string>();
                                        var translation = previous.Translations.SingleOrDefault(x => x.Key.Equals(current.LanguageCode));
                                        if (translation.Equals(default(KeyValuePair<string, string>)))
                                        {
                                            previous.Translations.Add(current.LanguageCode, current.Value);
                                        }
                                        else if (string.IsNullOrEmpty(translation.Value))
                                        {
                                            previous.Translations[current.LanguageCode] = current.Value ?? string.Empty;
                                        }

                                        entries[current.Key] = previous;
                                    }
                                }
                                else
                                {
                                    CheckAndAdd(ref entries, current);
                                }
                            }
                            else
                            {
                                CheckAndAdd(ref entries, current);
                            }
                        }
                        else
                        {
                            CheckAndAdd(ref entries, current);
                        }
                    }
                }

                results = languageDictionaryService.ImportDictionaryItems(entries.Items, overrideExisting);
            }

            return base.Json(new
            {
                Success = results.Count > 0,
                count = results.Count,
                results
            }, JsonRequestBehavior.DenyGet);
        }

        private void CheckAndAdd(ref ImportExportDictionaryItems entries, ImportExportDictionaryItem entry)
        {
            if (entries[entry.Key] == null)
            {
                entries.Items.Add(entry);
            }
        }
        private List<string[]> ExportHeadersToList()
        {
            return typeof(ExportDictionaryItem).WriteHeaders();
        }

        private IEnumerable<object[]> ExportToList(IEnumerable<ExportDictionaryItem> items, string locale)
        {
            return items.WriteCells(locale);
        }
    }
}