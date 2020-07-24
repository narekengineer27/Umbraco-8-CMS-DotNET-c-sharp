namespace Umbraco.Plugins.Connector.Content
{
    using System.Linq;
    using Umbraco.Core.Composing;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Core.PropertyEditors;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Helpers;

    public class _15_UpdateGenericDocumentTypeForSvgIcons : IComponent
    {
        public static string
            CONTAINER = "Total Code Data Types",
            DOCUMENT_TYPE_ALIAS = "totalCodeGenericPage",
            TAB = "Page Details";

        private readonly IContentTypeService contentTypeService;
        private readonly IDataTypeService dataTypeService;
        private readonly IFileService fileService;
        private readonly ILogger logger;

        public _15_UpdateGenericDocumentTypeForSvgIcons(IContentTypeService contentTypeService, IDataTypeService dataTypeService, IFileService fileService, ILogger logger)
        {
            this.contentTypeService = contentTypeService;
            this.dataTypeService = dataTypeService;
            this.fileService = fileService;
            this.logger = logger;
        }

        private void UpdateDocumentType()
        {
            const string
            svgViewerEditorAlias = "svgCustomView",
            svgViewerPropertyAlias = "pageCustomSvgIcon",
            svgViewerPropertyName = "Page Custom Svg Icon",
            svgViewerPropertyDescription = "Displays the page icon from the strip-menu svg file",
            svgViewerDataTypeName = "Page Custom Svg Icon";

            try
            {
                var container = dataTypeService.GetContainers(CONTAINER, 1).FirstOrDefault();
                var containerId = -1;

                if (container != null)
                    containerId = container.Id;

                var contentType = contentTypeService.Get(DOCUMENT_TYPE_ALIAS);
                if (contentType != null)
                {
                    var exists = dataTypeService.GetDataType(svgViewerDataTypeName) != null;
                    if (!exists)
                    {
                        var notCreated = Web.Composing.Current.PropertyEditors.TryGet(svgViewerEditorAlias, out IDataEditor editor);
                        if (notCreated)
                        {
                            DataType svgDataType = new DataType(editor, containerId)
                            {
                                Name = svgViewerDataTypeName
                            };
                            dataTypeService.Save(svgDataType);
                        }
                        else
                        {
                            var firstEmbeddedResource = new SvgViewerEmbeddedResources().Resources[0];
                            if (!ContentHelper.AssetAlreadyExists(firstEmbeddedResource.FileName, firstEmbeddedResource.OutputDirectory))
                            {
                                ContentHelper.CopyPhysicalAssets(new SvgViewerEmbeddedResources());
                                ConnectorContext.AuditService.Add(AuditType.Save, 1, contentType.Id, "Resources", "Embedded Resources Copied");
                            }
                            // will need to restart the app, in order to force a refresh to attempt to create the data type again
                            throw new DataTypeNotCreatedException("In order to create the Data Type, a Reload is required");
                        }
                    }

                    if (!contentType.PropertyTypeExists(svgViewerPropertyAlias))
                    {
                        var svgDataType = dataTypeService.GetDataType(svgViewerPropertyName);
                        PropertyType svgPropertyType = new PropertyType(svgDataType, svgViewerPropertyAlias)
                        {
                            Name = svgViewerPropertyName,
                            Description = svgViewerPropertyDescription,
                            Variations = ContentVariation.Nothing
                        };
                        contentType.AddPropertyType(svgPropertyType, TAB);

                        contentTypeService.Save(contentType);
                        ConnectorContext.AuditService.Add(AuditType.Save, -1, contentType.Id, "Document Type", $"Document Type '{DOCUMENT_TYPE_ALIAS}' has been updated");
                    }
                }

            }
            catch (System.Exception ex)
            {
                logger.Error(typeof(_15_UpdateGenericDocumentTypeForSvgIcons), ex.Message);
                logger.Error(typeof(_15_UpdateGenericDocumentTypeForSvgIcons), ex.StackTrace);
            }
        }

        public void Initialize()
        {
            UpdateDocumentType();
        }

        public void Terminate() { }

        internal class DataTypeNotCreatedException : System.Exception
        {
            public DataTypeNotCreatedException(string Message) : base(Message) { }
        }
    }
}
