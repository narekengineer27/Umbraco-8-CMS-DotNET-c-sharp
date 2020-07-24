namespace Umbraco.Plugins.Connector.Content
{
    using System;
    using System.Linq;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Models;

    public class ConfirmEmailContentNode
    {
        private readonly IContentService contentService;
        private readonly IContentTypeService contentTypeService;
        private readonly ILogger logger;

        public ConfirmEmailContentNode(IContentService contentService, IContentTypeService contentTypeService, ILogger logger)
        {
            this.contentService = contentService;
            this.contentTypeService = contentTypeService;
            this.logger = logger;
        }

        public int CreateConfirmEmail(Tenant tenant)
        {
            var home = TenantHelper.GetCurrentTenantHome(contentService, tenant.TenantUId.ToString());
            var docType = contentTypeService.Get(_03_ConfirmEmailDocumentType.DOCUMENT_TYPE_ALIAS);
            try
            {
                var nodeName = "Confirm Email";

                IContent confirmEmailNode = contentService.Create(nodeName, home.Id, _03_ConfirmEmailDocumentType.DOCUMENT_TYPE_ALIAS);
                confirmEmailNode.Name = nodeName;
                confirmEmailNode.SetCultureName(nodeName, tenant.Languages.Default);
                if (tenant.Languages.Default == "fa")
                {
                    confirmEmailNode.SetCultureName("ایمیل تایید", tenant.Languages.Default);
                }
                // Alternate Languages
                foreach (var language in tenant.Languages.Alternate)
                {
                    confirmEmailNode.SetCultureName($"{nodeName}-{language}", language.Trim());
                    if (language.Trim() == "fa")
                    {
                        confirmEmailNode.SetCultureName("ایمیل تایید", language.Trim());
                    }
                }

                contentService.Save(confirmEmailNode);

                ConnectorContext.AuditService.Add(AuditType.New, -1, confirmEmailNode.Id, "Content Node", $"ContentNode for {tenant.TenantUId.ToString()} has been created");
                return confirmEmailNode.Id;
            }
            catch (Exception ex)
            {
                logger.Error(typeof(ConfirmEmailContentNode), ex.Message);
                logger.Error(typeof(ConfirmEmailContentNode), ex.StackTrace);
                throw;
            }
        }
    }
}
