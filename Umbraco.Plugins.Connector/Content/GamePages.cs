namespace Umbraco.Plugins.Connector.Content
{
    using System;
    using Umbraco.Core.Composing;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Helpers;
    public class _19_GamePages : IComponent
    {
        private readonly ILocalizationService localizationService;
        private readonly IDomainService domainService;
        private readonly IFileService fileService;
        private readonly ILogger logger;
        private readonly IContentTypeService contentTypeService;
        private readonly IDataTypeService dataTypeService;

        public static string
                        DOCUMENT_TYPE_ALIAS = "totalCodeGenericPage",
                        PARENT_TEMPLATE_ALIAS = "totalCodeLayout",
                        TEMPLATE_ALIAS = "totalCodeGamePageTemplate",
                        TEMPLATE_NAME = "Total Code Game Page Template",
                        TAB_NAME = "Page Details";

        public _19_GamePages(ILocalizationService localizationService, IDomainService domainService, IFileService fileService, ILogger logger, IContentTypeService contentTypeService, IDataTypeService dataTypeService)
        {
            this.localizationService = localizationService;
            this.domainService = domainService;
            this.logger = logger;
            this.contentTypeService = contentTypeService;
            this.fileService = fileService;
            this.dataTypeService = dataTypeService;
        }

        private void Reconfigure()
        {
            try
            {
                var genericDocType = contentTypeService.Get(DOCUMENT_TYPE_ALIAS);
                // Create the Template if it doesn't exist
                if (fileService.GetTemplate(TEMPLATE_ALIAS) == null)
                {
                    //then create the template
                    Template newTemplate = new Template(TEMPLATE_NAME, TEMPLATE_ALIAS);
                    ITemplate masterTemplate = fileService.GetTemplate(PARENT_TEMPLATE_ALIAS);
                    newTemplate.SetMasterTemplate(masterTemplate);
                    fileService.SaveTemplate(newTemplate);

                    // Set template for document type
                    genericDocType.AddTemplate(contentTypeService, newTemplate);

                    ContentHelper.CopyPhysicalAssets(new GamesPagesEmbeddedResources());

                    ConnectorContext.AuditService.Add(AuditType.Save, -1, newTemplate.Id, "Template", $"Teplate '{TEMPLATE_NAME}' has been created and assigned");
                }

                if (!genericDocType.PropertyTypeExists("gameType"))
                {
                    PropertyType propType = new PropertyType(dataTypeService.GetDataType(-92), "gameType")
                    {
                        Name = "Game Type",
                        Description = "The game this page will display",
                        Variations = ContentVariation.Nothing
                    };
                    genericDocType.AddPropertyType(propType, TAB_NAME);
                    contentTypeService.Save(genericDocType);
                    ConnectorContext.AuditService.Add(AuditType.Save, -1, genericDocType.Id, "DocumentType", $"Document Type '{DOCUMENT_TYPE_ALIAS}' has been updated");
                }
            }

            catch (Exception ex)
            {
                logger.Error(typeof(_19_GamePages), ex.Message);
                logger.Error(typeof(_19_GamePages), ex.StackTrace);
            }
        }

        public void Initialize()
        {
            Reconfigure();
        }

        public void Terminate() { }
    }
}
