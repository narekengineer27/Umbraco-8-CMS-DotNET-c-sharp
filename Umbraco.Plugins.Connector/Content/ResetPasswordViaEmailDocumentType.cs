namespace Umbraco.Plugins.Connector.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Composing;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Helpers;

    public class _07_ResetPasswordViaEmailDocumentType : IComponent
    {
        public static string
            CONTAINER = "Total Code Container",
            PARENT_DOCUMENT_TYPE_ALIAS = "totalCodeBasePage",
            DOCUMENT_TYPE_ALIAS = "resetPasswordViaEmail",
            DOCUMENT_TYPE_NAME = "Total Code Reset Password",
            DOCUMENT_TYPE_DESCRIPTION = "Password Reset via Email Page",
            ICON = "icon-keyhole color-yellow",
            TEMPLATE_ALIAS = "TotalCodeTenantResetPassword",
            TEMPLATE_NAME = "Total Code Tenant Reset Password";

        private readonly IContentTypeService contentTypeService;
        private readonly IDataTypeService dataTypeService;
        private readonly IFileService fileService;
        private readonly ILogger logger;

        public _07_ResetPasswordViaEmailDocumentType(IContentTypeService contentTypeService, IDataTypeService dataTypeService, IFileService fileService, ILogger logger)
        {
            this.contentTypeService = contentTypeService;
            this.dataTypeService = dataTypeService;
            this.fileService = fileService;
            this.logger = logger;
        }

        private void CreateConfirmEmailDocumentType()
        {
            try
            {
                var container = contentTypeService.GetContainers(CONTAINER, 1).FirstOrDefault();
                var containerId = container.Id;
                var contentType = contentTypeService.Get(DOCUMENT_TYPE_ALIAS);
                var parentDocType = contentTypeService.Get(PARENT_DOCUMENT_TYPE_ALIAS);
                if (contentType == null)
                {
                    ContentType docType = (ContentType)contentType ?? new ContentType(containerId)
                    {
                        Name = DOCUMENT_TYPE_NAME,
                        Alias = DOCUMENT_TYPE_ALIAS,
                        AllowedAsRoot = false,
                        Description = DOCUMENT_TYPE_DESCRIPTION,
                        Icon = ICON,
                        SortOrder = 0,
                        Variations = ContentVariation.Culture,
                        ParentId = parentDocType.Id
                    };

                    // Create the Template if it doesn't exist
                    if (fileService.GetTemplate(TEMPLATE_ALIAS) == null)
                    {
                        //then create the template
                        Template newTemplate = new Template(TEMPLATE_NAME, TEMPLATE_ALIAS);
                        fileService.SaveTemplate(newTemplate);
                    }

                    // Set templates for document type
                    var template = fileService.GetTemplate(TEMPLATE_ALIAS);
                    docType.AllowedTemplates = new List<ITemplate> { template };
                    docType.SetDefaultTemplate(template);

                    contentTypeService.Save(docType);

                    // set as allowed content type in home
                    ContentHelper.AddAllowedDocumentType(contentTypeService, Phase2MergedHomeDocumentType.DOCUMENT_TYPE_ALIAS, DOCUMENT_TYPE_ALIAS);
                    ConfigureMasterTemplate();

                    ConnectorContext.AuditService.Add(AuditType.New, -1, docType.Id, "Document Type", $"Document Type '{DOCUMENT_TYPE_NAME}' has been created");

                    ContentHelper.CopyPhysicalAssets(new Milestone7EmbeddedResources());
                }
            }
            catch (System.Exception ex)
            {
                logger.Error(typeof(_07_ResetPasswordViaEmailDocumentType), ex.Message);
                logger.Error(typeof(_07_ResetPasswordViaEmailDocumentType), ex.StackTrace);
            }
        }
        public void ConfigureMasterTemplate()
        {
            var layoutAlias = "totalCodeLayout";
            ITemplate masterTemplate = fileService.GetTemplate(layoutAlias);
            if (masterTemplate == null)
            {
                // then create the master template
                masterTemplate = new Template(layoutAlias, layoutAlias);
                fileService.SaveTemplate(masterTemplate);
            }
            // assign templates to master
            var confirmTemplate = fileService.GetTemplate(TEMPLATE_ALIAS);
            confirmTemplate.SetMasterTemplate(masterTemplate);

            fileService.SaveTemplate(confirmTemplate);
        }
        public void Initialize()
        {
            CreateConfirmEmailDocumentType();
        }

        public void Terminate() { }
    }
}
