namespace Umbraco.Plugins.Connector.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Umbraco.Core;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Dictionaries;
    using Umbraco.Plugins.Connector.Exceptions;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Models;
    using Language = Core.Models.Language;

    public class ImportResult
    {
        public string Key { get; set; }
        public bool Success { get; set; }
    }

    public class LanguageDictionaryService
    {
        private readonly IDomainService domainService;
        private readonly ILocalizationService localizationService;
        private readonly ILogger logger;
        private readonly NodeHelper nodeHelper;

        public LanguageDictionaryService(ILocalizationService localizationService, IDomainService domainService, ILogger logger)
        {
            this.localizationService = localizationService;
            this.domainService = domainService;
            this.logger = logger;
            nodeHelper = new NodeHelper();
        }

        public void AddCultureAndHostname(TenantDomain tenant, bool secureUrl)
        {
            const string protocolBase = "http";
            var protocol = secureUrl ? $"{protocolBase}s" : protocolBase;
            try
            {
                var tenantRoot = nodeHelper.GetTenantRoot(tenant.TenantUid);
                var languages = localizationService.GetAllLanguages();
                var subDomain = tenantRoot.GetValue<string>("subDomain");

                string domainName = $"{protocol}://{subDomain}.{tenant.Domain}/";
                var existingDomain = domainService.GetByName(domainName);
                if (Uri.IsWellFormedUriString(domainName, UriKind.RelativeOrAbsolute))
                {
                    if (existingDomain == null)
                    {
                        var defaultLanguageId = localizationService.GetDefaultLanguageId();
                        existingDomain = new UmbracoDomain(domainName)
                        {
                            LanguageId = defaultLanguageId,
                            RootContentId = tenantRoot.Id
                        };

                        domainService.Save(existingDomain);
                        ConnectorContext.AuditService.Add(AuditType.New, -1, existingDomain.Id, "Language", $"Domain '{domainName}' for Tenant '{tenant.TenantUid}' has been created");

                    }

                    foreach (var language in languages)
                    {
                        var localizedDomainName = $"{domainName}{language.IsoCode}";
                        var localizedDomain = domainService.GetByName(localizedDomainName);
                        if (localizedDomain == null)
                        {
                            localizedDomain = new UmbracoDomain(localizedDomainName)
                            {
                                LanguageId = language.Id,
                                RootContentId = tenantRoot.Id
                            };
                            domainService.Save(localizedDomain);
                            ConnectorContext.AuditService.Add(AuditType.New, -1, existingDomain.Id, "Language", $"Domain '{localizedDomainName}' for Tenant '{tenant.TenantUid}' has been created");

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(typeof(LanguageDictionaryService), ex.Message);
                logger.Error(typeof(LanguageDictionaryService), ex.StackTrace);
                throw;
            }
        }

        public void AddCultureAndHostnameDomain(TenantDomain tenant, bool secureUrl)
        {
            const string protocolBase = "http";
            var protocol = secureUrl ? $"{protocolBase}s" : protocolBase;
            try
            {
                var tenantRoot = nodeHelper.GetTenantRoot(tenant.TenantUid);
                var languages = localizationService.GetAllLanguages();
                var subDomain = tenantRoot.GetValue<string>("subDomain");

                var tenantHome = nodeHelper.GetTenantRoot(tenant.TenantUid);
                var domains = nodeHelper.GetTenantDomains(tenant.TenantUid);


                string[] langs = new string[] { };
                if (tenantRoot.HasProperty("languages") && tenantRoot.GetValue("languages") != null)
                {
                    var languages_val = tenantRoot.GetValue("languages").ToString();
                    if (languages_val.Contains(","))
                    {
                        langs = languages_val.Split(',');
                    }
                    else
                    {
                        langs = new string[] { languages_val };
                    }
                }
                string defaultLang = tenantRoot.GetValue("defaultLanguage").ToString();

                string domainName = $"{protocol}://{tenant.Domain}/";
                var existingDomain = domainService.GetByName(domainName);
                if (Uri.IsWellFormedUriString(domainName, UriKind.RelativeOrAbsolute))
                {
                    if (existingDomain == null)
                    {
                        var defaultLanguageId = localizationService.GetLanguageIdByIsoCode(defaultLang);
                        existingDomain = new UmbracoDomain(domainName)
                        {
                            LanguageId = defaultLanguageId,
                            RootContentId = tenantRoot.Id
                        };

                        domainService.Save(existingDomain);
                        ConnectorContext.AuditService.Add(AuditType.New, -1, existingDomain.Id, "Language", $"Domain '{domainName}' for Tenant '{tenant.TenantUid}' has been created");

                    }

                    foreach (var language in languages)
                    {
                        if (langs.Contains(language.IsoCode))
                        {
                            var localizedDomainName = $"{domainName}{language.IsoCode}";
                            var localizedDomain = domainService.GetByName(localizedDomainName);
                            if (localizedDomain == null)
                            {
                                localizedDomain = new UmbracoDomain(localizedDomainName)
                                {
                                    LanguageId = language.Id,
                                    RootContentId = tenantRoot.Id
                                };
                                domainService.Save(localizedDomain);
                                ConnectorContext.AuditService.Add(AuditType.New, -1, existingDomain.Id, "Language", $"Domain '{localizedDomainName}' for Tenant '{tenant.TenantUid}' has been created");

                            }
                        }
                    }
                }

                string domainWWW = $"{protocol}://www.{tenant.Domain}/";
                var existingDomainWWW = domainService.GetByName(domainWWW);
                if (Uri.IsWellFormedUriString(domainWWW, UriKind.RelativeOrAbsolute))
                {
                    if (existingDomainWWW == null)
                    {
                        var defaultLanguageId = localizationService.GetLanguageIdByIsoCode(defaultLang);
                        existingDomainWWW = new UmbracoDomain(domainWWW)
                        {
                            LanguageId = defaultLanguageId,
                            RootContentId = tenantRoot.Id
                        };

                        domainService.Save(existingDomainWWW);
                        ConnectorContext.AuditService.Add(AuditType.New, -1, existingDomainWWW.Id, "Language", $"Domain '{domainWWW}' for Tenant '{tenant.TenantUid}' has been created");

                    }

                    foreach (var language in languages)
                    {
                        if (langs.Contains(language.IsoCode))
                        {
                            var localizedDomainName = $"{domainWWW}{language.IsoCode}";
                            var localizedDomain = domainService.GetByName(localizedDomainName);
                            if (localizedDomain == null)
                            {
                                localizedDomain = new UmbracoDomain(localizedDomainName)
                                {
                                    LanguageId = language.Id,
                                    RootContentId = tenantRoot.Id
                                };
                                domainService.Save(localizedDomain);
                                ConnectorContext.AuditService.Add(AuditType.New, -1, existingDomainWWW.Id, "Language", $"Domain '{localizedDomainName}' for Tenant '{tenant.TenantUid}' has been created");

                            }
                        }
                    }
                }

                string domainSubdomain = $"{protocol}://{subDomain}.{tenant.Domain}/";
                var existingDomainSubdomain = domainService.GetByName(domainSubdomain);
                if (Uri.IsWellFormedUriString(domainSubdomain, UriKind.RelativeOrAbsolute))
                {
                    if (existingDomainSubdomain == null)
                    {
                        var defaultLanguageId = localizationService.GetLanguageIdByIsoCode(defaultLang);
                        existingDomainSubdomain = new UmbracoDomain(domainSubdomain)
                        {
                            LanguageId = defaultLanguageId,
                            RootContentId = tenantRoot.Id
                        };

                        domainService.Save(existingDomainSubdomain);
                        ConnectorContext.AuditService.Add(AuditType.New, -1, existingDomainSubdomain.Id, "Language", $"Domain '{domainSubdomain}' for Tenant '{tenant.TenantUid}' has been created");

                    }

                    foreach (var language in languages)
                    {
                        if (langs.Contains(language.IsoCode))
                        {
                            var localizedDomainName = $"{domainSubdomain}{language.IsoCode}";
                            var localizedDomain = domainService.GetByName(localizedDomainName);
                            if (localizedDomain == null)
                            {
                                localizedDomain = new UmbracoDomain(localizedDomainName)
                                {
                                    LanguageId = language.Id,
                                    RootContentId = tenantRoot.Id
                                };
                                domainService.Save(localizedDomain);
                                ConnectorContext.AuditService.Add(AuditType.New, -1, existingDomainSubdomain.Id, "Language", $"Domain '{localizedDomainName}' for Tenant '{tenant.TenantUid}' has been created");

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(typeof(LanguageDictionaryService), ex.Message);
                logger.Error(typeof(LanguageDictionaryService), ex.StackTrace);
                throw;
            }
        }

        public bool HasLanguage(string isoCode)
        {
            var languages = localizationService.GetAllLanguages().Select(x => x.IsoCode).ToList();
            return languages.Contains(isoCode);
        }

        public bool CheckExists(Type item)
        {
            var keyString = item.GetProperty("Key").Value<string>();
            return localizationService.GetDictionaryItemByKey(keyString) != null;
        }

        public void ClearCulturesAndHostnames(ExtendedTenant tenant)
        {
            foreach (var domain in domainService.GetAssignedDomains(tenant.StartContentId, true))
            {
                domainService.Delete(domain);
                ConnectorContext.AuditService.Add(AuditType.Save, -1, domain.Id, "Domain", $"Cultures and Hostinames for {tenant.Tenant.TenantUId} have been cleared");
            }
        }

        public void CreateDictionary(Tenant tenant)
        {
            var lang = localizationService.GetLanguageByIsoCode(tenant.Languages.Default);
            if (string.IsNullOrEmpty(tenant.Languages.Default))
            {
                throw new TenantException(ExceptionCode.DefaultLanguageIsMandatory.CodeToString(), ExceptionCode.DefaultLanguageIsMandatory, tenant.TenantUId);
            }
            else
            {
                try
                {
                    if (lang == null)
                    {
                        lang = new Language(tenant.Languages.Default)
                        {
                            IsDefault = true,
                            IsMandatory = true
                        };
                        localizationService.Save(lang);
                    }

                    foreach (var language in tenant.Languages.Alternate ?? new List<string>())
                    {
                        var translationLanguage = localizationService.GetLanguageByIsoCode(language);
                        if (translationLanguage == null)
                        {
                            translationLanguage = new Language(language)
                            {
                                FallbackLanguageId = lang.Id,
                                IsMandatory = false
                            };
                            localizationService.Save(translationLanguage);
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(typeof(LanguageDictionaryService), ex.Message);
                    logger.Error(typeof(LanguageDictionaryService), ex.StackTrace);
                    throw;
                }
            }
        }

        public void CreateDictionaryItem(Type item)
        {
            var keyString = item.GetProperty("Key").Value<string>();
            var value = item.GetProperty("Value").Value<string>();
            var parentKey = item.GetProperty("ParentKey")?.Value<string>();
            var dicItem = localizationService.GetDictionaryItemByKey(keyString) ?? new DictionaryItem(keyString);

            var lang = localizationService.GetLanguageByIsoCode(item.GetProperty("LanguageCode").Value<string>());
            if (lang == null)
            {
                lang = new Language(item.GetProperty("LanguageCode").Value<string>());
                localizationService.Save(lang);
            }

            List<IDictionaryTranslation> translations = new List<IDictionaryTranslation>();
            foreach (var translation in item.GetProperty("Translations").Value<Dictionary<string, string>>() ?? new Dictionary<string, string>())
            {
                var translationLanguage = localizationService.GetLanguageByIsoCode(translation.Key);
                if (translationLanguage == null)
                {
                    translationLanguage = new Language(translation.Key);
                    localizationService.Save(translationLanguage);
                }
                var trans = new DictionaryTranslation(translationLanguage, translation.Value);
                translations.Add(trans);
            }
            dicItem.Translations = translations;
            if (!string.IsNullOrEmpty(parentKey))
            {
                var parentGuid = localizationService.GetDictionaryItemByKey(parentKey)?.GetUdi().Guid;
                if (parentGuid != null)
                {
                    dicItem.ParentId = parentGuid;
                }
            }
            localizationService.AddOrUpdateDictionaryValue(dicItem, lang, value);
            localizationService.Save(dicItem);
        }

        public bool ParentExists(string parentName)
        {
            return localizationService.GetDictionaryItemByKey(parentName) != null;
        }

        public (IDictionaryItem, bool) CreateDictionaryItem(ImportExportDictionaryItem item)
        {
            var dicItem = localizationService.GetDictionaryItemByKey(item.Key) ?? new DictionaryItem(item.Key);

            var lang = localizationService.GetLanguageByIsoCode(item.LanguageCode);
            if (lang == null)
            {
                lang = new Language(item.LanguageCode);
                localizationService.Save(lang);
            }

            List<IDictionaryTranslation> translations = new List<IDictionaryTranslation>();
            var mainLanguage = localizationService.GetLanguageByIsoCode(item.LanguageCode);
            var mainTranslation = dicItem.Translations.FirstOrDefault(x => x.Language.Equals(mainLanguage));
            if (mainTranslation != null)
            {
                if (!string.IsNullOrEmpty(item.Value))
                {
                    mainTranslation.Value = item.Value;
                    translations.Add(mainTranslation);
                }
            }
            else
            {
                translations.Add(new DictionaryTranslation(mainLanguage, item.Value ?? string.Empty));
            }


            foreach (var translation in item.Translations ?? new Dictionary<string, string>())
            {
                var translationLanguage = localizationService.GetLanguageByIsoCode(translation.Key);
                if (translationLanguage == null)
                {
                    translationLanguage = new Language(translation.Key);
                    localizationService.Save(translationLanguage);
                }

                var t = dicItem.Translations.FirstOrDefault(x => x.Language.IsoCode.Equals(translation.Key));
                if (t != null)
                {
                    t.Value = translation.Value ?? string.Empty;
                    translations.Add(t);
                }
                else
                {
                    translations.Add(new DictionaryTranslation(translationLanguage, translation.Value ?? string.Empty));
                }
            }

            dicItem.Translations = translations;

            if (!string.IsNullOrEmpty(item.ParentKey))
            {
                var parentGuid = localizationService.GetDictionaryItemByKey(item.ParentKey)?.GetUdi().Guid;
                if (parentGuid != null && !dicItem.ParentId.Equals(parentGuid))
                {
                    dicItem.ParentId = parentGuid;
                }
            }

            try
            {
                localizationService.Save(dicItem);
                return (dicItem, true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return (dicItem, false);
            }
        }

        public Dictionary<string, bool> CreateDictionaryItems(List<ImportExportDictionaryItem> items)
        {
            Dictionary<string, bool> results = new Dictionary<string, bool>();
            foreach (var item in items)
            {
                var keyString = item.Key;
                var value = item.Value;
                var parentKey = item.ParentKey;
                var dicItem = localizationService.GetDictionaryItemByKey(keyString) ?? new DictionaryItem(keyString);

                var lang = localizationService.GetLanguageByIsoCode(item.LanguageCode);
                if (lang == null)
                {
                    lang = new Language(item.LanguageCode);
                    localizationService.Save(lang);
                }

                List<IDictionaryTranslation> translations = new List<IDictionaryTranslation>();
                foreach (var translation in item.Translations ?? new Dictionary<string, string>())
                {
                    var translationLanguage = localizationService.GetLanguageByIsoCode(translation.Key);
                    if (translationLanguage == null)
                    {
                        translationLanguage = new Language(translation.Key);
                        localizationService.Save(translationLanguage);
                    }
                    var trans = new DictionaryTranslation(translationLanguage, translation.Value);
                    translations.Add(trans);
                }

                dicItem.Translations = translations;

                if (!string.IsNullOrEmpty(parentKey))
                {
                    var parentGuid = localizationService.GetDictionaryItemByKey(parentKey)?.GetUdi().Guid;
                    if (parentGuid != null)
                    {
                        dicItem.ParentId = parentGuid;
                    }
                }

                try
                {
                    localizationService.AddOrUpdateDictionaryValue(dicItem, lang, value);
                    localizationService.Save(dicItem);

                    results.Add(dicItem.ItemKey, true);
                }
                catch
                {
                    results.Add(dicItem.ItemKey, false);
                }
            }
            return results;
        }

        public void CreateDictionaryItems(List<Type> items)
        {
            foreach (var item in items)
                CreateDictionaryItem(item);
        }

        public void DeleteAllDictionaryItems()
        {
            var allItems = GetAllDictionaryItems();
            foreach (var item in allItems.OrderByDescending(x => !string.IsNullOrEmpty(x.ParentKey)))
            {
                var key = localizationService.GetDictionaryItemByKey(item.Key);
                if (key != null)
                    localizationService.Delete(key);
            }
        }

        public void DeleteMissingCultureAndHostname(int rootId)
        {
            var domains = domainService.GetAssignedDomains(rootId, false);
            var languages = localizationService.GetAllLanguages();
            foreach (var domain in domains)
            {
                var lang = domain.LanguageIsoCode;
                if (languages.SingleOrDefault(x => x.IsoCode.Equals(lang)) == null)
                {
                    domainService.Delete(domain);
                }
            }
        }
        public IEnumerable<ExportDictionaryItem> GetAllDictionaryItems()
        {
            List<ExportDictionaryItem> allDictionaryItems = new List<ExportDictionaryItem>();
            foreach (var rootItem in localizationService.GetRootDictionaryItems())
            {
                var languageCode = localizationService.GetDefaultLanguageIsoCode();
                allDictionaryItems.Add(
                    new ExportDictionaryItem
                    {
                        Key = rootItem.ItemKey,
                        LanguageCode = languageCode,
                        Value = rootItem.GetDefaultValue(),
                        LanguageName = localizationService.GetAllLanguages().Single(x => x.IsoCode == languageCode).CultureName
                    });
                foreach (var childItem in localizationService.GetDictionaryItemChildren(rootItem.GetUdi().Guid))
                {
                    var languages = localizationService.GetAllLanguages();
                    foreach (var language in languages)
                    {
                        var translation = childItem.Translations.FirstOrDefault(x => x.Language.IsoCode.Equals(language.IsoCode));
                        if (translation == null)
                        {
                            var export = new ExportDictionaryItem
                            {
                                Key = childItem.ItemKey,
                                LanguageCode = language.IsoCode,
                                ParentKey = rootItem.ItemKey,
                                Value = string.Empty,
                                LanguageName = language.CultureName
                            };
                            allDictionaryItems.Add(export);
                        }
                        else
                        {
                            allDictionaryItems.Add(new ExportDictionaryItem
                            {
                                Key = childItem.ItemKey,
                                LanguageCode = translation.Language.IsoCode,
                                ParentKey = rootItem.ItemKey,
                                Value = translation.Value,
                                LanguageName = language.CultureName
                            });
                        }

                    }
                }
            }
            return allDictionaryItems;
        }

        public IEnumerable<DisplayDictionaryItem> GetAllDisplayDictionaryItems()
        {
            List<DisplayDictionaryItem> allDictionaryItems = new List<DisplayDictionaryItem>();
            foreach (var rootItem in localizationService.GetRootDictionaryItems())
            {
                var languageCode = localizationService.GetDefaultLanguageIsoCode();
                allDictionaryItems.Add(
                    new DisplayDictionaryItem
                    {
                        Key = rootItem.ItemKey,
                        LanguageCode = languageCode,
                        Value = rootItem.GetDefaultValue(),
                        LanguageName = localizationService.GetAllLanguages().Single(x => x.IsoCode == languageCode).CultureName,
                        Id = rootItem.Id
                    });
                foreach (var childItem in localizationService.GetDictionaryItemChildren(rootItem.GetUdi().Guid))
                {
                    var languages = localizationService.GetAllLanguages();
                    foreach (var language in languages)
                    {
                        var translation = childItem.Translations.FirstOrDefault(x => x.Language.IsoCode.Equals(language.IsoCode));
                        if (translation == null)
                        {
                            var export = new DisplayDictionaryItem
                            {
                                Key = childItem.ItemKey,
                                LanguageCode = language.IsoCode,
                                ParentKey = rootItem.ItemKey,
                                Value = string.Empty,
                                LanguageName = language.CultureName,
                                Id = childItem.Id
                            };
                            allDictionaryItems.Add(export);
                        }
                        else
                        {
                            allDictionaryItems.Add(new DisplayDictionaryItem
                            {
                                Key = childItem.ItemKey,
                                LanguageCode = translation.Language.IsoCode,
                                ParentKey = rootItem.ItemKey,
                                Value = translation.Value,
                                LanguageName = language.CultureName,
                                Id = childItem.Id
                            });
                        }

                    }
                }
            }
            return allDictionaryItems;
        }

        public TenantDomains GetTenantDomains(string tenantUid)
        {
            return nodeHelper.GetTenantDomains(tenantUid);
        }

        public List<ImportResult> ImportDictionaryItems(List<ImportExportDictionaryItem> items, bool clearAllFirst = false)
        {
            List<ImportResult> results = new List<ImportResult>();
            if (clearAllFirst)
            {
                DeleteAllDictionaryItems();
            }
            foreach (var item in items)
            {
                var success = CreateDictionaryItem(item);
                results.Add(new ImportResult
                {
                    Key = success.Item1.ItemKey,
                    Success = success.Item2
                });
            }
            return results;
        }

        public void RemoveCultureAndHostname(TenantDomain tenant, bool secureUrl)
        {
            const string protocolBase = "http";
            var protocol = secureUrl ? $"{protocolBase}s" : protocolBase;
            try
            {
                var tenantRoot = nodeHelper.GetTenantRoot(tenant.TenantUid);
                var languages = localizationService.GetAllLanguages();
                //var subDomain = tenantRoot.GetValue<string>("subDomain");

                string domainName = $"{protocol}://{tenant.Domain}/";
                var existingDomain = domainService.GetByName(domainName);

                if (existingDomain != null)
                {
                    domainService.Delete(existingDomain);
                    ConnectorContext.AuditService.Add(AuditType.Delete, -1, existingDomain.Id, "Language", $"Domain '{domainName}' for Tenant '{tenant.TenantUid}' has been deleted");
                }

                foreach (var language in languages)
                {
                    var localizedDomainName = $"{domainName}{language.IsoCode}";
                    var localizedDomain = domainService.GetByName(localizedDomainName);
                    if (localizedDomain != null)
                    {
                        domainService.Delete(localizedDomain);
                        ConnectorContext.AuditService.Add(AuditType.Delete, -1, localizedDomain.Id, "Language", $"Domain '{localizedDomainName}' for Tenant '{tenant.TenantUid}' has been deleted");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(typeof(LanguageDictionaryService), ex.Message);
                logger.Error(typeof(LanguageDictionaryService), ex.StackTrace);
                throw;
            }
        }

        public void SetCulturesAndHostnames(ExtendedTenant tenant, bool setupLocal = false, bool secureUrl = false)
        {
            Validate(tenant.Tenant);
            const string protocolBase = "http";
            var protocol = secureUrl ? $"{protocolBase}s" : protocolBase;
            try
            {
                var langExists = localizationService.GetLanguageByIsoCode(tenant.Tenant.Languages.Default) != null;
                if (langExists)
                {
                    // Create hostname and multi language
                    //string domainName = $"{tenant.Tenant.SubDomain}.{tenant.Tenant.Domain}/{tenant.Tenant.Languages.Default.ToLower()}/";
                    string domainName = $"{protocol}://{tenant.Tenant.SubDomain}.{tenant.Tenant.Domain}/";
                    var domain = domainService.GetByName(domainName);
                    if (Uri.IsWellFormedUriString(domainName, UriKind.RelativeOrAbsolute))
                    {
                        if (domain == null)
                        {
                            domain = new UmbracoDomain(domainName)
                            {
                                LanguageId = localizationService.GetLanguageIdByIsoCode(tenant.Tenant.Languages.Default),
                                RootContentId = tenant.StartContentId
                            };
                            domainService.Save(domain);
                            ConnectorContext.AuditService.Add(AuditType.New, -1, domain.Id, "Language", $"Default Domain for {tenant.Tenant.Languages.Default} has been created");
                        }

                        if (setupLocal)
                        {
                            string domainNameLocal = $"/{tenant.Tenant.SubDomain}/{tenant.Tenant.Languages.Default.ToLower()}";
                            var domainLocal = domainService.GetByName(domainNameLocal);
                            if (domainLocal == null)
                            {
                                domainLocal = new UmbracoDomain(domainNameLocal)
                                {
                                    LanguageId = localizationService.GetLanguageIdByIsoCode(tenant.Tenant.Languages.Default),
                                    RootContentId = tenant.StartContentId
                                };
                                domainService.Save(domainLocal);
                                ConnectorContext.AuditService.Add(AuditType.New, -1, domain.Id, "Language", $"Domain {domainNameLocal} (local) has been created for {tenant.Tenant.TenantUId}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(typeof(LanguageDictionaryService), ex.Message);
                logger.Error(typeof(LanguageDictionaryService), ex.StackTrace);
                throw;
            }
            try
            {
                foreach (var language in tenant.Tenant.Languages.Alternate ?? new List<string>())
                {
                    var langExists = localizationService.GetLanguageByIsoCode(language) != null;
                    if (langExists)
                    {
                        var lang = language.Replace(" ", string.Empty);
                        // Create hostname and multi language for secondary languages
                        var altDomainName = $"{protocol}://{tenant.Tenant.SubDomain}.{tenant.Tenant.Domain}/{language.ToLower()}/";
                        var altDomain = domainService.GetByName(altDomainName);
                        if (altDomain == null)
                        {
                            altDomain = new UmbracoDomain(altDomainName)
                            {
                                LanguageId = localizationService.GetLanguageIdByIsoCode(lang),
                                RootContentId = tenant.StartContentId
                            };
                            domainService.Save(altDomain);
                            ConnectorContext.AuditService.Add(AuditType.New, -1, altDomain.Id, "Language", $"Alternative Domain {altDomain} has been created");
                        }

                        if (setupLocal)
                        {
                            string altDomainNameLocal = $"/{tenant.Tenant.SubDomain}/{language.ToLower()}";
                            var altDomainLocal = domainService.GetByName(altDomainNameLocal);
                            if (altDomainLocal == null)
                            {
                                altDomainLocal = new UmbracoDomain(altDomainNameLocal)
                                {
                                    LanguageId = localizationService.GetLanguageIdByIsoCode(lang),
                                    RootContentId = tenant.StartContentId
                                };
                                domainService.Save(altDomainLocal);

                            }
                            ConnectorContext.AuditService.Add(AuditType.New, -1, altDomainLocal.Id, "Language", $"Alternative Domain {altDomainLocal} (local) has been created");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(typeof(LanguageDictionaryService), ex.Message);
                logger.Error(typeof(LanguageDictionaryService), ex.StackTrace);
                throw;
            }
        }

        public void Validate(Tenant tenant)
        {
            if (string.IsNullOrEmpty(tenant.Languages.Default))
            {
                throw new TenantException(ExceptionCode.DefaultLanguageIsMandatory.CodeToString(), ExceptionCode.DefaultLanguageIsMandatory, tenant.TenantUId);
            }
        }
    }
}