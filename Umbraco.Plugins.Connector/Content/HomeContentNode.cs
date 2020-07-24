namespace Umbraco.Plugins.Connector.Content
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Exceptions;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Models;
    using Umbraco.Plugins.Connector.Services;

    public class HomeContentNode
    {
        private readonly IContentService contentService;
        private readonly IContentTypeService contentTypeService;
        private readonly ILogger logger;
        private readonly LanguageDictionaryService languageDictionaryService;
        private readonly NodeHelper nodeHelper;
        private const string ENABLED = "Enabled", DISABLED = "Disabled (Soft Delete)";

        public HomeContentNode(IContentService contentService, ILocalizationService localizationService, IDomainService domainService, IContentTypeService contentTypeService, ILogger logger)
        {
            this.contentService = contentService;
            this.contentTypeService = contentTypeService;
            this.logger = logger;
            this.languageDictionaryService = new LanguageDictionaryService(localizationService, domainService, logger);
            this.nodeHelper = new NodeHelper();
        }

        public void Validate(Tenant tenant)
        {
            var existingTenant = TenantHelper.TenantExist(contentService, tenant.TenantUId.ToString());

            if (string.IsNullOrEmpty(tenant.SubDomain))
            {
                throw new TenantException(ExceptionCode.SubDomainIsRequired.CodeToString(), ExceptionCode.SubDomainIsRequired, tenant.TenantUId);
            }
            if (existingTenant)
            {
                throw new TenantException(ExceptionCode.TenantAlreadyExists.CodeToString(), ExceptionCode.TenantAlreadyExists, tenant.TenantUId, tenant.BrandName);
            }
        }

        public void ValidateTenant(string tenantUid)
        {
            var exists = TenantExists(tenantUid);
            if (!exists)
            {
                throw new TenantException(ExceptionCode.TenantNotFound.CodeToString(), ExceptionCode.TenantNotFound, tenantUid.ToString());
            }
        }

        public int CreateTenant(Tenant tenant)
        {
            var nodeName = tenant.Name;
            var nodeAlias = tenant.Name.Trim(' ').ToLower();
            //var nodeName = tenant.BrandName;
            //var nodeAlias = tenant.BrandName.Trim(' ').ToLower();
            var docType = contentTypeService.Get(Phase2MergedHomeDocumentType.DOCUMENT_TYPE_ALIAS);
            tenant.TenantPreferences.PaymentSettings = tenant.PaymentSettings; //little hack to match total coding system submission to umbraco properties
            tenant.PaymentSettings = null;
            Validate(tenant);
            try
            {
                IContent tenantNode = contentService.Create(nodeName, -1, Phase2MergedHomeDocumentType.DOCUMENT_TYPE_ALIAS);
                tenantNode.Name = nodeName;
                tenantNode.SetCultureName(nodeName, tenant.Languages.Default);

                // Alternate Languages
                foreach (var language in tenant.Languages.Alternate)
                {
                    tenantNode.SetCultureName($"{nodeName}-{language}", language.Trim());
                }

                // Set values for node
                tenantNode.SetValue("brandName", tenant.BrandName);
                tenantNode.SetValue("tenantUid", tenant.TenantUId);
                tenantNode.SetValue("domain", tenant.Domain);
                tenantNode.SetValue("subDomain", tenant.SubDomain);
                tenantNode.SetValue("helpdeskTelegramAccount", tenant.HelpdeskTelegramAccount);
                tenantNode.SetValue("apiKey", tenant.ApiKey);
                tenantNode.SetValue("appId", tenant.AppId);
                tenantNode.SetValue("defaultLanguage", tenant.Languages.Default);
                tenantNode.SetValue("tenantCurrencies", string.Join(",", tenant.Currencies.Codes));
                tenantNode.SetValue("tenantStatus", ENABLED);
                tenantNode.SetValue("languages", string.Join(", ", tenant.Languages.Alternate.ToList()));
                tenantNode.SetValue("tenantPreferencesProperty", JsonConvert.SerializeObject(tenant.TenantPreferences));

                tenantNode.SetValue("metaAuthor", "نیتروبت", tenant.Languages.Default);
                tenantNode.SetValue("metaCopyright", "کلیه حقوق برای بت و برد محفوظ است", tenant.Languages.Default);
                tenantNode.SetValue("metaDescription", "پیش بینی مسابقات ورزشی با بهترین ضرایب به صورت زنده و پیش از بازی روی تمامی ورزشها معتبر جهان. با بهترین خدمات پس از فروش و پرداخت سریع جوایز", tenant.Languages.Default);

                //tenantNode.SetValue("paymentSettingsProperty", JsonConvert.SerializeObject(tenant.PaymentSettings));

                contentService.Save(tenantNode);

                ConnectorContext.AuditService.Add(AuditType.New, -1, tenantNode.Id, "Content Node", $"ContentNode for {tenant.TenantUId} has been created");
                return tenantNode.Id;
            }
            catch (Exception ex)
            {
                logger.Error(typeof(HomeContentNode), ex.Message);
                logger.Error(typeof(HomeContentNode), ex.StackTrace);
                throw;
            }
        }

        public TenantDomains AddTenantDomain(TenantDomain tenantDomain, bool setupLocalUrls, bool secureUrls)
        {
            ValidateTenant(tenantDomain.TenantUid);

            var tenantHome = nodeHelper.GetTenantRoot(tenantDomain.TenantUid);

            string domainList = nodeHelper.GetTenantDomainsString(tenantDomain.TenantUid);

            if (tenantDomain.isPrimary)
            {
                nodeHelper.SetNodeContent(tenantHome, "domain", tenantDomain.Domain, false);
                nodeHelper.SaveNode(tenantHome);

                languageDictionaryService.AddCultureAndHostnameDomain(tenantDomain, secureUrls);
                nodeHelper.TryPublishSite(tenantHome.Id);
                return nodeHelper.GetTenantDomains(tenantDomain.TenantUid);
            }

            if (DomainAlreadyExists(tenantDomain.TenantUid, tenantDomain.Domain)) throw new TenantException(ExceptionCode.DomainAlreadyAssigned.CodeToString(), ExceptionCode.DomainAlreadyAssigned, tenantDomain.TenantUid, tenantDomain.Domain);

            domainList += string.IsNullOrEmpty(domainList) ? tenantDomain.Domain : $",{tenantDomain.Domain}";
            nodeHelper.SetNodeContent(tenantHome, "alternateDomains", domainList, false);
            nodeHelper.SaveNode(tenantHome);

            languageDictionaryService.AddCultureAndHostnameDomain(tenantDomain, secureUrls);
            nodeHelper.TryPublishSite(tenantHome.Id);

            return nodeHelper.GetTenantDomains(tenantDomain.TenantUid);
        }

        public TenantDomains RemoveTenantDomain(TenantDomain tenantDomain, bool secureUrls)
        {
            ValidateTenant(tenantDomain.TenantUid);

            var tenantHome = nodeHelper.GetTenantRoot(tenantDomain.TenantUid);
            var domains = nodeHelper.GetTenantDomains(tenantDomain.TenantUid);

            if (domains.AlternateDomains == null) throw new TenantException(ExceptionCode.DomainDoesNotExist.CodeToString(), ExceptionCode.DomainDoesNotExist, tenantDomain.TenantUid, tenantDomain.Domain);
            if (domains.AlternateDomains.SingleOrDefault(x => x == tenantDomain.Domain) == null) throw new TenantException(ExceptionCode.DomainDoesNotExist.CodeToString(), ExceptionCode.DomainDoesNotExist, tenantDomain.TenantUid, tenantDomain.Domain);

            var domainList = nodeHelper.GetTenantDomainsString(tenantDomain.TenantUid);

            domainList = domainList.Replace($"{tenantDomain.Domain}", string.Empty);
            var array = domainList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            domainList = string.Join(",", array);
            nodeHelper.SetNodeContent(tenantHome, "alternateDomains", domainList, false);
            nodeHelper.SaveNode(tenantHome);

            languageDictionaryService.RemoveCultureAndHostname(tenantDomain, secureUrls);
            nodeHelper.TryPublishSite(tenantHome.Id);

            return nodeHelper.GetTenantDomains(tenantDomain.TenantUid);
        }

        public static bool TenantExists(Tenant tenant)
        {
            return TenantExists(tenant.TenantUId);
        }

        public static bool TenantExists(string tenantUid)
        {
            return new NodeHelper().TenantExists(tenantUid.ToString());
        }

        public bool DomainAlreadyExists(string tenantUid, string domain)
        {
            var domains = nodeHelper.GetTenantDomains(tenantUid);
            return domains.AlternateDomains != null && domains.AlternateDomains.SingleOrDefault(x => x == domain) != null;
        }

        public static bool BrandnameExists(Tenant tenant)
        {
            var cservice = ConnectorContext.ContentService;
            return cservice.GetByLevel(1)
                ?.SingleOrDefault(x => x.GetValue("brandName").Equals(tenant.BrandName)) != null;
        }

        public string EditTenant(Tenant tenant)
        {
            var tenantNode = TenantHelper.GetCurrentTenantHome(contentService, tenant.TenantUId.ToString());
            if (tenantNode == null) throw new TenantException(ExceptionCode.TenantNotFound.CodeToString(), ExceptionCode.TenantNotFound, tenant.TenantUId);

            try
            {
                // Set values for node
                tenantNode.Name = tenant.Name;
                tenantNode.SetCultureName(tenant.Name, tenant.Languages.Default);
                List<String> lstcultures = new List<string>();
                foreach (var culture in tenantNode.AvailableCultures)
                {
                    lstcultures.Add(culture);//need this to not throw exception list modified
                }

                foreach (var culture in lstcultures)
                {
                    tenantNode.SetCultureName(tenant.Name, culture);
                }
                //tenantNode.Name = tenant.BrandName;
                //tenantNode.SetCultureName(tenant.BrandName, tenant.Languages.Default);
                tenantNode.SetValue("brandName", tenant.BrandName);
                tenantNode.SetValue("tenantUid", tenant.TenantUId);
                tenantNode.SetValue("domain", tenant.Domain);
                tenantNode.SetValue("subDomain", tenant.SubDomain);
                if (!string.IsNullOrEmpty(tenant.ApiKey))
                    tenantNode.SetValue("apiKey", tenant.ApiKey);
                if (!string.IsNullOrEmpty(tenant.AppId))
                    tenantNode.SetValue("appId", tenant.AppId);
                tenantNode.SetValue("defaultLanguage", tenant.Languages.Default);
                tenantNode.SetValue("tenantCurrencies", string.Join(",", tenant.Currencies.Codes));
                tenantNode.SetValue("tenantStatus", ENABLED);
                tenantNode.SetValue("languages", string.Join(", ", tenant.Languages.Alternate.ToList()));
                tenantNode.SetValue("tenantPreferencesProperty", JsonConvert.SerializeObject(tenant.TenantPreferences));

                contentService.Save(tenantNode);
                ConnectorContext.AuditService.Add(AuditType.Save, -1, tenantNode.Id, "Content Node", $"ContentNode for {tenant.TenantUId} has been edited");

                return tenant.TenantUId.ToString();
            }
            catch (Exception ex)
            {
                logger.Error(typeof(HomeContentNode), ex.Message);
                logger.Error(typeof(HomeContentNode), ex.StackTrace);
                throw;
            }
        }

        public string EnableTenant(string tenantUid)
        {
            var tenantNode = TenantHelper.GetCurrentTenantHome(contentService, tenantUid.ToString());
            if (tenantNode == null) throw new TenantException(ExceptionCode.TenantNotFound.CodeToString(), ExceptionCode.TenantNotFound, tenantUid.ToString());
            if (tenantNode.GetValue<string>("tenantStatus").Equals(ENABLED)) throw new TenantException(ExceptionCode.TenantAlreadyEnabled.CodeToString(), ExceptionCode.TenantAlreadyEnabled, tenantUid);

            try
            {
                tenantNode.SetValue("tenantStatus", ENABLED);
                contentService.Save(tenantNode);
                contentService.Unpublish(tenantNode);
                ConnectorContext.AuditService.Add(AuditType.Save, -1, tenantNode.Id, "Content Node", $"ContentNode for {tenantUid} has been enabled");

                return tenantUid.ToString();
            }
            catch (Exception ex)
            {
                logger.Error(typeof(HomeContentNode), ex.Message);
                logger.Error(typeof(HomeContentNode), ex.StackTrace);
                throw;
            }
        }

        public string DisableTenant(string tenantUid)
        {
            var tenantNode = TenantHelper.GetCurrentTenantHome(contentService, tenantUid.ToString());
            if (tenantNode == null) throw new TenantException(ExceptionCode.TenantNotFound.CodeToString(), ExceptionCode.TenantNotFound, tenantUid.ToString());
            if (tenantNode.GetValue<string>("tenantStatus").Equals(DISABLED)) throw new TenantException(ExceptionCode.TenantAlreadyDisabled.CodeToString(), ExceptionCode.TenantAlreadyDisabled, tenantUid);

            try
            {
                tenantNode.SetValue("tenantStatus", DISABLED);
                contentService.Save(tenantNode);
                contentService.Unpublish(tenantNode);
                ConnectorContext.AuditService.Add(AuditType.Unpublish, -1, tenantNode.Id, "Content Node", $"ContentNode for {tenantUid} has been disabled");
                return tenantUid.ToString();
            }
            catch (Exception ex)
            {
                logger.Error(typeof(HomeContentNode), ex.Message);
                logger.Error(typeof(HomeContentNode), ex.StackTrace);
                throw;
            }
        }
    }
}
