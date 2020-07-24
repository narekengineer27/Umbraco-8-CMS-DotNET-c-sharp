namespace Umbraco.Plugins.Connector.Content
{
    using System;
    using System.Linq;
    using Umbraco.Core.Composing;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Core.PropertyEditors;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Web.PropertyEditors;

    public class _24_HomeDocumentTypeGameModeDialog : IComponent
    {
        private readonly ILocalizationService localizationService;
        private readonly IDomainService domainService;
        private readonly IFileService fileService;
        private readonly ILogger logger;
        private readonly IContentTypeService contentTypeService;
        private readonly IDataTypeService dataTypeService;

        public static string
                        DATA_TYPE_CONTAINER = "Total Code Data Types",
                        DOCUMENT_TYPE_ALIAS = "totalCodeHomePage",
                        TAB_NAME = "Game Mode Dialog";

        public _24_HomeDocumentTypeGameModeDialog(ILocalizationService localizationService, IDomainService domainService, IFileService fileService, ILogger logger, IContentTypeService contentTypeService, IDataTypeService dataTypeService)
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
                var homeDocType = contentTypeService.Get(DOCUMENT_TYPE_ALIAS);
                homeDocType.AddPropertyGroup(TAB_NAME);

                //if (!homeDocType.PropertyTypeExists("gameMessage"))
                //{

                //    var richTextEditor = dataTypeService.GetDataType("Full Rich Text Editor");
                //    PropertyType richTextPropType = new PropertyType(dataTypeService.GetDataType(richTextEditor.Id), "gameMessage")
                //    {
                //        Name = "Game Terms and Conditions",
                //        Description = "Terms and Conditions for playing games",
                //        Variations = ContentVariation.Culture
                //    };
                //    homeDocType.AddPropertyType(richTextPropType, TAB_NAME);
                //}

                if (homeDocType.PropertyTypeExists("gameMessage"))
                {
                    homeDocType.RemovePropertyType("gameMessage");
                    contentTypeService.Save(homeDocType);
                }

                if (!homeDocType.PropertyTypeExists("gameAgreeText"))
                {

                    PropertyType gameAgreePropType = new PropertyType(dataTypeService.GetDataType(-88), "gameAgreeText")
                    {
                        Name = "Agreement text",
                        Description = "Agreement text with checkbox",
                        Variations = ContentVariation.Culture
                    };
                    homeDocType.AddPropertyType(gameAgreePropType, TAB_NAME);

                    PropertyType playButtonTextPropType = new PropertyType(dataTypeService.GetDataType(-88), "playButtonText")
                    {
                        Name = "Play Button Text",
                        Description = "Text for the play button",
                        Variations = ContentVariation.Culture
                    };
                    homeDocType.AddPropertyType(playButtonTextPropType, TAB_NAME);

                    PropertyType demoButtonTextPropType = new PropertyType(dataTypeService.GetDataType(-88), "demoButtonText")
                    {
                        Name = "Demo Button Text",
                        Description = "Text for the demo button",
                        Variations = ContentVariation.Culture
                    };
                    homeDocType.AddPropertyType(demoButtonTextPropType, TAB_NAME);

                    var mediaPickerContainer = dataTypeService.GetContainers(DATA_TYPE_CONTAINER, 1).FirstOrDefault();
                    var mediaPickerContainerId = -1;

                    if (mediaPickerContainer != null) mediaPickerContainerId = mediaPickerContainer.Id;

                    var mediaPickerDataType = dataTypeService.GetDataType("Demo Page Media Picker");


                    PropertyType demoPageImages = new PropertyType(dataTypeService.GetDataType(mediaPickerDataType.Id), "demoPageImages")
                    {
                        Name = "Game Mode Images",
                        Description = "Images to display in the Game Mode Dialog",
                        Variations = ContentVariation.Culture
                    };
                    homeDocType.AddPropertyType(demoPageImages, TAB_NAME);

                    contentTypeService.Save(homeDocType);
                    ConnectorContext.AuditService.Add(AuditType.Save, -1, homeDocType.Id, "DocumentType", $"Document Type '{DOCUMENT_TYPE_ALIAS}' has been updated");
                }
            }

            catch (Exception ex)
            {
                logger.Error(typeof(_24_HomeDocumentTypeGameModeDialog), ex.Message);
                logger.Error(typeof(_24_HomeDocumentTypeGameModeDialog), ex.StackTrace);
            }
        }

        public void Initialize()
        {
            Reconfigure();
        }

        public void Terminate() { }
    }
}
