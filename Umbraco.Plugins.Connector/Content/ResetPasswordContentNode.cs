namespace Umbraco.Plugins.Connector.Content
{
    using System;
    using System.Linq;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Models;

    public class ResetPasswordContentNode
    {
        private readonly IContentService contentService;
        private readonly IContentTypeService contentTypeService;
        private readonly ILogger logger;

        public ResetPasswordContentNode(IContentService contentService, IContentTypeService contentTypeService, ILogger logger)
        {
            this.contentService = contentService;
            this.contentTypeService = contentTypeService;
            this.logger = logger;
        }

        public int CreateResetPasswordPage(Tenant tenant)
        {
            var home = TenantHelper.GetCurrentTenantHome(contentService, tenant.TenantUId.ToString());
            var docType = contentTypeService.Get(_07_ResetPasswordViaEmailDocumentType.DOCUMENT_TYPE_ALIAS);
            try
            {
                var nodeName = "Reset Password";

                IContent resetPasswordNode = contentService.Create(nodeName, home.Id, _07_ResetPasswordViaEmailDocumentType.DOCUMENT_TYPE_ALIAS);
                resetPasswordNode.Name = nodeName;
                resetPasswordNode.SetCultureName(nodeName, tenant.Languages.Default);
                if (tenant.Languages.Default == "fa")
                {
                    resetPasswordNode.SetCultureName("بازیابی کلمه عبور", tenant.Languages.Default);
                }

                // Alternate Languages
                foreach (var language in tenant.Languages.Alternate)
                {
                    resetPasswordNode.SetCultureName($"{nodeName}-{language}", language.Trim());
                    if (language.Trim() == "fa")
                    {
                        resetPasswordNode.SetCultureName("بازیابی کلمه عبور", language.Trim());
                    }
                }

                contentService.Save(resetPasswordNode);

                ConnectorContext.AuditService.Add(AuditType.New, -1, resetPasswordNode.Id, "Content Node", $"ContentNode for {tenant.TenantUId.ToString()} has been created");
                return resetPasswordNode.Id;
            }
            catch (Exception ex)
            {
                logger.Error(typeof(ResetPasswordContentNode), ex.Message);
                logger.Error(typeof(ResetPasswordContentNode), ex.StackTrace);
                throw;
            }
        }
    }
}
