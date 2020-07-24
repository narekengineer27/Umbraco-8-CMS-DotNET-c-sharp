namespace Umbraco.Plugins.Connector.Content
{
    using System.Linq;
    using Umbraco.Core.Models;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Services;
    using Umbraco.Core.Composing;
    using System.Collections.Generic;
    using Umbraco.Plugins.Connector.Helpers;
    public class _23_ErrorPageDocumentType : IComponent
    {
        public static string
            CONTAINER = "Total Code Container",
            DOCUMENT_TYPE_ALIAS = "ErrorPage",
            DOCUMENT_TYPE_NAME = "Generic Error Page",
            DOCUMENT_TYPE_DESCRIPTION = "Total Code Tenant Error Page (All Tenants)",
            DOCUMENT_PARENT_ALIAS = "totalCodeBasePage",
            ICON = "icon-application-error color-red",
            TEMPLATE_ALIAS = "GenericErrorTemplate",
            TEMPLATE_NAME = "Error Page Template",
            TEMPLATE_PARENT_ALIAS = "totalCodeLayout",
            CONTENT_TAB = "Content";

        private readonly IContentTypeService contentTypeService;
        private readonly IDataTypeService dataTypeService;
        private readonly IDomainService domainService;
        private readonly IFileService fileService;
        private readonly ILogger logger;

        public _23_ErrorPageDocumentType(IContentTypeService contentTypeService, IDataTypeService dataTypeService, IFileService fileService, IDomainService domainService, ILogger logger)
        {
            this.contentTypeService = contentTypeService;
            this.dataTypeService = dataTypeService;
            this.domainService = domainService;
            this.fileService = fileService;
            this.logger = logger;
        }

        private void CreateErrorPageDocumentType()
        {
            try
            {
                var container = contentTypeService.GetContainers(CONTAINER, 1).FirstOrDefault();
                int containerId = container.Id;

                var errorDocType = contentTypeService.Get(DOCUMENT_TYPE_ALIAS);
                if (errorDocType == null)
                {
                    errorDocType = new ContentType(containerId)
                    {
                        Name = DOCUMENT_TYPE_NAME,
                        Alias = DOCUMENT_TYPE_ALIAS,
                        AllowedAsRoot = true,
                        ParentId = contentTypeService.Get("totalCodeBasePage").Id,
                        Description = DOCUMENT_TYPE_DESCRIPTION,
                        Icon = ICON,
                        SortOrder = 0,
                        Variations = ContentVariation.Culture
                    };

                    errorDocType.AddPropertyGroup(CONTENT_TAB);

                    PropertyType errorTitlePropertyType = new PropertyType(dataTypeService.GetDataType(-88), "errorTitle")
                    {
                        Name = "Page Title",
                        Description = "Title to display to users when reaching this page",
                        Variations = ContentVariation.Culture
                    };
                    errorDocType.AddPropertyType(errorTitlePropertyType, CONTENT_TAB);

                    var richTextEditor = dataTypeService.GetDataType("Full Rich Text Editor");
                    string propertyName = "Page Content",
                        propertyDescription = "Text to be displayed on the Page";
                    PropertyType richTextPropType = new PropertyType(dataTypeService.GetDataType(richTextEditor.Id), "pageContent")
                    {
                        Name = propertyName,
                        Description = propertyDescription,
                        Variations = ContentVariation.Culture
                    };
                    errorDocType.AddPropertyType(richTextPropType, CONTENT_TAB);

                    if (fileService.GetTemplate(TEMPLATE_ALIAS) == null)
                    {
                        ITemplate masterTemplate = fileService.GetTemplate(TEMPLATE_PARENT_ALIAS);
                        Template newTemplate = new Template(TEMPLATE_NAME, TEMPLATE_ALIAS);
                        newTemplate.SetMasterTemplate(masterTemplate);
                        fileService.SaveTemplate(newTemplate);
                        errorDocType.AllowedTemplates = new List<ITemplate> { newTemplate };
                        errorDocType.SetDefaultTemplate(newTemplate);
                    }

                    contentTypeService.Save(errorDocType);
                    ConnectorContext.AuditService.Add(AuditType.New, -1, errorDocType.Id, "Document Type", $"Document Type '{DOCUMENT_TYPE_NAME}' has been created");

                    ContentHelper.CopyPhysicalAssets(new ErrorPageEmbeddedResources());

                    NodeHelper helper = new NodeHelper();
                    helper.CreateErrorNode(DOCUMENT_TYPE_ALIAS, "Page Not Found (404)", domainService);

                }
            }
            catch (System.Exception ex)
            {
                logger.Error(typeof(_23_ErrorPageDocumentType), ex.Message);
                logger.Error(typeof(_23_ErrorPageDocumentType), ex.StackTrace);
            }
        }



        public void Initialize()
        {
            CreateErrorPageDocumentType();
        }

        public void Terminate() { }
    }
}
