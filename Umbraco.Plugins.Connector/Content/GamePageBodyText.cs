namespace Umbraco.Plugins.Connector.Content
{
    using System;
    using Umbraco.Core.Composing;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Core.Services;

    public class _22_GamePageBodyText : IComponent
    {
        public static string
            DATA_TYPE_CONTAINER = "Total Code Data Types",
            CONTAINER = "Total Code Container",
            DOCUMENT_TYPE_ALIAS = "totalCodeGenericPage",
            TAB = "Content";

        private readonly IContentTypeService contentTypeService;
        private readonly IDataTypeService dataTypeService;
        private readonly ILogger logger;

        public _22_GamePageBodyText(IContentTypeService contentTypeService, IDataTypeService dataTypeService, ILogger logger)
        {
            this.contentTypeService = contentTypeService;
            this.dataTypeService = dataTypeService;
            this.logger = logger;
        }

        private void UpdateDocumentType()
        {
            try
            {
                // generic page
                var genericType = contentTypeService.Get(DOCUMENT_TYPE_ALIAS);
                if (genericType != null)
                {
                    var richTextEditor = dataTypeService.GetDataType("Full Rich Text Editor");
                    string propertyName = "Page Content",
                        propertyDescription = "Text to be displayed on the Page";

                    PropertyType richTextPropType = new PropertyType(dataTypeService.GetDataType(richTextEditor.Id), "pageContent")
                    {
                        Name = propertyName,
                        Description = propertyDescription,
                        Variations = ContentVariation.Culture
                    };

                    genericType.AddPropertyType(richTextPropType, TAB);
                    contentTypeService.Save(genericType);
                    ConnectorContext.AuditService.Add(AuditType.Save, -1, genericType.Id, "Document Type", $"Document Type '{DOCUMENT_TYPE_ALIAS}' has been updated");
                }
            }
            catch (Exception ex)
            {
                logger.Error(typeof(_22_GamePageBodyText), ex.Message);
                logger.Error(typeof(_22_GamePageBodyText), ex.StackTrace);
            }
        }

        public void Initialize()
        {
            UpdateDocumentType();
        }

        public void Terminate() { }
    }
}
