namespace Umbraco.Plugins.Connector.Dictionaries
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Umbraco.Plugins.Connector.Interfaces;
    public class ImportExportDictionaryItem : IDictionaryItemImportExport
    {
        [Display(Name = "Parent Key")]
        public string ParentKey { get; set; }

        [Display(Name = "Key")]
        public string Key { get; set; }

        [Display(Name = "Translation")]
        public string Value { get; set; }

        [Display(Name = "Language Code")]
        public string LanguageCode { get; set; }

        [Display(Name = "Language Name")]
        public string LanguageName { get; set; }

        public Dictionary<string, string> Translations { get; set; }

    }

    public class DisplayDictionaryItem : IDictionaryItemImportExport
    {
        [Display(Name = "Parent")]
        public string ParentKey { get; set; }

        [Display(Name = "Key")]
        public string Key { get; set; }

        [Display(Name = "Translation")]
        public string Value { get; set; }

        [Display(Name = "Language Code")]
        public string LanguageCode { get; set; }

        [Display(Name = "Language Name")]
        public string LanguageName { get; set; }

        public int Id { get; set; }
    }

    public class ExportDictionaryItem
    {
        [Display(Name = "Parent Key")]
        public string ParentKey { get; set; }
        [Display(Name = "Key")]
        public string Key { get; set; }
        [Display(Name = "Translation")]
        public string Value { get; set; }
        [Display(Name = "Language Code")]
        public string LanguageCode { get; set; }
        [Display(Name = "Language Name")]
        public string LanguageName { get; set; }

    }
    public class ExportDictionaryTranslation
    {
        [Display(Name = "Language Code")]
        public string LanguageCode { get; set; }
        [Display(Name = "Translation")]
        public string Value { get; set; }
    }

    public class ImportExportDictionaryItems
    {
        public ImportExportDictionaryItems()
        {
            Items = Items ?? new List<ImportExportDictionaryItem>();
        }
        public List<ImportExportDictionaryItem> Items { get; set; }
        public ImportExportDictionaryItem this[string key]
        {
            get { return this.Items.SingleOrDefault(x => x.Key.Equals(key)); }
            set
            {
                Items.Remove(this.Items.SingleOrDefault(x => x.Key.Equals(key)));
                this.Items.Add(value);
            }
        }
    }

    public class AuditDictionaryItem
    {
        public string Key { get; set; }
        public string File { get; set; }
        public bool Registered { get; set; }
        public string ParentKey { get; set; }
    }
}
