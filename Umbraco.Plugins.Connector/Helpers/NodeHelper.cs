namespace Umbraco.Plugins.Connector.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Core.Persistence;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Models;
    using Umbraco.Web;

    public static class ContextHelper
    {
        public static string Local(string url)
        {
            return !url.Contains("http") ? " (local)" : string.Empty;
        }
    }

    public class NodeHelper
    {
        private readonly IContentService contentService;
        private readonly IContentTypeService contentTypeService;
        private readonly IUmbracoContextFactory contextFactory;
        private readonly ILocalizationService localizationService;
        private readonly ILogger logger;

        public NodeHelper()
        {
            this.contentService = ConnectorContext.ContentService;
            this.contentTypeService = ConnectorContext.ContentTypeService;
            this.logger = ConnectorContext.Logger;
            this.contextFactory = ConnectorContext.ContextFactory;
            this.localizationService = ConnectorContext.LocalizationService;
        }

        public void AddLanguageVersionToNode(Tenant tenant)
        {
            var home = TenantHelper.GetCurrentTenantHome(contentService, tenant.TenantUId.ToString());
            var children = contentService.GetByLevel(2).Where(x => x.ParentId == home.Id);
            try
            {
                foreach (var language in tenant.Languages.Alternate)
                {
                    foreach (var child in children)
                    {
                        try
                        {
                            if (child.GetCultureName(language) == null)
                            {
                                child.SetCultureName($"{child.Name}-{language}", language);
                                contentService.Save(child);
                            }
                        }
                        catch { }
                    }
                }
                ConnectorContext.AuditService.Add(AuditType.Save, -1, home.Id, "Content Node", $"Children for {home.Name} have been updated with new language");
            }
            catch (Exception ex)
            {
                logger.Error(typeof(NodeHelper), ex.Message);
                logger.Error(typeof(NodeHelper), ex.StackTrace);
                throw;
            }
        }

        public int CreateErrorNode(string documentTypeAlias, string nodeName, IDomainService domainService)
        {
            var docType = contentTypeService.Get(documentTypeAlias);
            var languages = domainService.GetAll(false);
            try
            {
                IContent node = contentService.Create(nodeName, -1, documentTypeAlias);
                node.Name = nodeName;
                foreach (var language in languages)
                {
                    node.SetCultureName(nodeName, language.LanguageIsoCode);
                }

                contentService.Save(node);

                ConnectorContext.AuditService.Add(AuditType.New, -1, node.Id, "Error Content Node", $"Generic Error Node {nodeName} has been created");
                return node.Id;
            }
            catch (Exception ex)
            {
                logger.Error(typeof(NodeHelper), ex.Message);
                logger.Error(typeof(NodeHelper), ex.StackTrace);
                throw;
            }
        }

        public int CreateNode(Tenant tenant, int parentId, string documentTypeAlias, string nodeName, string nodeNameFa = "")
        {
            var home = TenantHelper.GetCurrentTenantHome(contentService, tenant.TenantUId.ToString());
            var docType = contentTypeService.Get(documentTypeAlias);
            try
            {
                IContent node = contentService.Create(nodeName, parentId, documentTypeAlias);
                node.Name = nodeName;
                node.SetCultureName(nodeName, tenant.Languages.Default);
                if (tenant.Languages.Default == "fa")
                {
                    node.SetCultureName(nodeNameFa, tenant.Languages.Default);
                }

                if (!string.IsNullOrEmpty(home.GetValue<string>("languages")))
                {
                    var alternateLanguages = home.GetValue<string>("languages").Split(',');
                    foreach (var language in alternateLanguages)
                    {
                        node.SetCultureName(nodeName, language.Trim());

                        if (language.Trim() == "fa")
                        {
                            node.SetCultureName(nodeNameFa, language.Trim());
                        }
                    }
                }

                contentService.Save(node);

                ConnectorContext.AuditService.Add(AuditType.New, -1, node.Id, "Content Node", $"Node {nodeName} for {tenant.TenantUId.ToString()} has been created");
                return node.Id;
            }
            catch (Exception ex)
            {
                logger.Error(typeof(NodeHelper), ex.Message);
                logger.Error(typeof(NodeHelper), ex.StackTrace);
                throw;
            }
        }

        public int GetNodeId(int tenantId, string documentTypeAlias, string nodeName)
        {
            int nodeid = 0;
            try
            {
                long total;
                var lstContent = contentService.GetPagedChildren(tenantId, 0, 1000, out total);
                foreach (var item in lstContent)
                {
                    string name = item.Name;
                    if (item.GetPublishName("en") != null )
                    {
                        name = item.GetPublishName("en");
                    }
                    if (item != null && name.ToLower().Contains(nodeName.ToLower()) && item.ContentType.Alias == documentTypeAlias)
                    {
                        nodeid= item.Id;
                        break;
                    }
                }
                return nodeid;
            }
            catch (Exception ex)
            {
                logger.Error(typeof(NodeHelper), ex.Message);
                logger.Error(typeof(NodeHelper), ex.StackTrace);
                throw;
            }
        }

        public string GetNodeGuid(int nodeId)
        {
            var node = contentService.GetById(nodeId);
            return node.Key.ToString().Replace("-", string.Empty);
        }

        public TenantDomains GetTenantDomains(string tenantUid)
        {
            var root = GetTenantRoot(tenantUid);
            var domains = new TenantDomains
            {
                Domain = root.GetValue("domain").ToString(),
                AlternateDomains = root.GetValue("alternateDomains")?.ToString().Split(','),
                SubDomain = root.GetValue("subDomain").ToString()
            };
            return domains;
        }

        public string GetTenantDomainsString(string tenantUid)
        {
            var root = GetTenantRoot(tenantUid);
            return root.GetValue<string>("alternateDomains") ?? string.Empty;
        }

        public IContent CopyTenant(IContent sourceTenant, int parentId, string tenantUId)
        {
            try
            {
                var newTenant = contentService.Copy(sourceTenant, parentId, false, -1);

                newTenant.SetValue("tenantUid", tenantUId);

                contentService.Save(newTenant);

                ConnectorContext.AuditService.Add(AuditType.Copy, -1, newTenant.Id, "Content Node", $"ContentNode for {tenantUId} has been created");

                return newTenant;
            }
            catch (Exception ex)
            {
                logger.Error(typeof(NodeHelper), ex.Message);
                logger.Error(typeof(NodeHelper), ex.StackTrace);
                throw;
            }
        }

        public IContent GetTenantRoot(string tenantUid)
        {
            var root = contentService.GetRootContent().SingleOrDefault(x => x.HasProperty("tenantUid") && x.Properties.Single(y => y.Alias == "tenantUid").GetValue().ToString() == tenantUid);
            return root;
        }

        public IContent GetTenantRoot(Guid tenantGuid)
        {
            return TenantHelper.GetCurrentTenantHome(contentService, tenantGuid.ToString());
        }

        public IEnumerable<IContent> GetAllTenants()
        {
            return contentService.GetByLevel(1).Where(x => x.HasProperty("tenantUid"));
        }

        public bool TenantExists(string tenantUid)
        {
            var root = GetTenantRoot(tenantUid);
            return root != null;
        }

        public void RefreshNodeName(string tenantUid, string tenantName)
        {
            var home = TenantHelper.GetCurrentTenantHome(contentService, tenantUid);
            var children = contentService.GetByLevel(2).Where(x => x.ParentId == home.Id);
            try
            {
                foreach (var language in localizationService.GetAllLanguages())
                {
                    if (home.GetCultureName(language.IsoCode) == null)
                    {
                        home.SetCultureName(tenantName, language.IsoCode);
                    }

                    foreach (var child in children)
                    {
                        if (child.GetCultureName(language.IsoCode) == null)
                        {
                            child.SetCultureName($"{tenantName}-{language}", language.IsoCode);
                            contentService.Save(child);
                        }
                    }
                }
                ConnectorContext.AuditService.Add(AuditType.Save, -1, home.Id, "Content Node", $"Children for {tenantName} have been refreshed");
            }
            catch (Exception ex)
            {
                logger.Error(typeof(NodeHelper), ex.Message);
                logger.Error(typeof(NodeHelper), ex.StackTrace);
                throw;
            }
        }

        public void SaveNode(int nodeId)
        {
            var node = contentService.GetById(nodeId);
            SaveNode(node);
        }

        public void SaveNode(IContent node)
        {
            contentService.Save(node);
        }

        public IContent SetNodeContent(int nodeId, string propertyAlias, object value, bool hasVariations = true)
        {
            var node = contentService.GetById(nodeId);
            return SetNodeContent(node, propertyAlias, value, hasVariations);
        }

        public IContent GetNodeContent(int nodeId)
        {
            return contentService.GetById(nodeId);
        }

        public IContent SetNodeContent(IContent node, string propertyAlias, object value, bool hasVariations = true)
        {
            if (hasVariations)
            {
                var languages = localizationService.GetAllLanguages();
                foreach (var language in languages)
                {
                    node.SetValue(propertyAlias, value, language.IsoCode);
                }
            }
            else
            {
                node.SetValue(propertyAlias, value);
            }

            return node;
        }

        public void TryPublishSite(int rootId)
        {
            try
            {
                using (contextFactory.EnsureUmbracoContext())
                {
                    var home = contentService.GetById(rootId);
                    contentService.SaveAndPublishBranch(home, true);
                    ConnectorContext.AuditService.Add(AuditType.Publish, -1, home.Id, "Content Node", $"Home '{home.Name}' has been published");
                }
            }
            catch
            {
                //ignore because of possible bug https://github.com/umbraco/Umbraco-CMS/issues/5281
            }
        }
    }
}
