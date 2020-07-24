namespace Umbraco.Plugins.Connector.Content
{
    using System.Linq;
    using Umbraco.Core.Composing;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Core.PropertyEditors;
    using Umbraco.Core.Services;
    using Umbraco.Web.PropertyEditors;

    public class _30_ExternalUrlsToMenus : IComponent
    {
        public static string
            DATA_TYPE_CONTAINER = "Total Code Data Types",
            DOCUMENT_TYPE_CONTAINER = "Total Code Container",
            DOCUMENT_TYPE_ALIAS = "totalCodeHomePage",
            CONTENT_TAB = "Content",
            ACCOUNT_TAB = "Account Menu";

        private readonly IDataTypeService dataTypeService;
        private readonly IContentTypeService contentTypeService;
        private readonly ILogger logger;

        public _30_ExternalUrlsToMenus(IContentTypeService contentTypeService, IDataTypeService dataTypeService, ILogger logger)
        {
            this.dataTypeService = dataTypeService;
            this.contentTypeService = contentTypeService;
            this.logger = logger;
        }

        private void UpdateDocumentType()
        {
            const string
                propertyAlias = "externalUrls",
                propertyName = "External Urls",
                propertyDescription = "Custom Links for";

            try
            {
                #region Home Document Type
                var container = contentTypeService.GetContainers(DOCUMENT_TYPE_CONTAINER, 1).FirstOrDefault();
                int containerId = -1;

                if (container != null)
                    containerId = container.Id;

                var contentType = contentTypeService.Get(DOCUMENT_TYPE_ALIAS);
                if (contentType != null)
                {
                    var changed = false;
                    #region External Links Property Type
                    if (!contentType.PropertyTypeExists($"{propertyAlias}TopMenu"))
                    {
                        PropertyType topMenuPropType = new PropertyType(dataTypeService.GetDataType(1050), $"{propertyAlias}TopMenu")
                        {
                            Name = $"Top Menu {propertyName}",
                            Description = $"{propertyDescription} Top Menu",
                            Variations = ContentVariation.Culture
                        };
                        contentType.AddPropertyType(topMenuPropType, CONTENT_TAB);
                        changed = true;
                    }

                    if (!contentType.PropertyTypeExists($"{propertyAlias}MainMenu"))
                    {
                        PropertyType mainMenuPropType = new PropertyType(dataTypeService.GetDataType(1050), $"{propertyAlias}MainMenu")
                        {
                            Name = $"Main Menu {propertyName}",
                            Description = $"{propertyDescription} Main Menu",
                            Variations = ContentVariation.Culture
                        };
                        contentType.AddPropertyType(mainMenuPropType, CONTENT_TAB);
                        changed = true;
                    }

                    if (!contentType.PropertyTypeExists($"{propertyAlias}Footer"))
                    {
                        PropertyType mainMenuPropType = new PropertyType(dataTypeService.GetDataType(1050), $"{propertyAlias}Footer")
                        {
                            Name = $"Footer {propertyName}",
                            Description = $"{propertyDescription} Footer",
                            Variations = ContentVariation.Culture
                        };
                        contentType.AddPropertyType(mainMenuPropType, CONTENT_TAB);
                        changed = true;
                    }

                    if (!contentType.PropertyTypeExists($"{propertyAlias}AccountMenu"))
                    {
                        PropertyType accountMenuPropType = new PropertyType(dataTypeService.GetDataType(1050), $"{propertyAlias}AccountMenu")
                        {
                            Name = $"Account Menu {propertyName}",
                            Description = $"{propertyDescription} Account Menu",
                            Variations = ContentVariation.Culture
                        };
                        contentType.AddPropertyType(accountMenuPropType, CONTENT_TAB);
                        changed = true;
                    }
                    #endregion

                    if (changed)
                        contentTypeService.Save(contentType);
                    ConnectorContext.AuditService.Add(AuditType.New, -1, contentType.Id, "Document Type", $"Document Type '{DOCUMENT_TYPE_ALIAS}' has been updated");
                }
                #endregion
            }
            catch (System.Exception ex)
            {
                logger.Error(typeof(_30_ExternalUrlsToMenus), ex.Message);
                logger.Error(typeof(_30_ExternalUrlsToMenus), ex.StackTrace);
            }
        }

        public void Initialize()
        {
            UpdateDocumentType();
        }

        public void Terminate() { }
    }
}
