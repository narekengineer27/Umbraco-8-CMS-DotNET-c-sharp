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

    public class _10_CasinoPageIntegration : IComponent
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
                        NESTED_DOCUMENT_TYPE_ALIAS = "BannerItem",
                        DATA_TYPE_CONTAINER = "Total Code Data Types",
                        TEMPLATE_ALIAS = "totalCodeCasinoPageTemplate",
                        TEMPLATE_NAME = "Total Code Casino Template",
                        SLIDERS_TAB = "Sliders";

        public _10_CasinoPageIntegration(ILocalizationService localizationService, IDomainService domainService, IFileService fileService, ILogger logger, IContentTypeService contentTypeService, IDataTypeService dataTypeService)
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
            const string
                propertyAlias = "gameSlider",
                propertyName = "Game Slider",
                propertyDescription = "Slider with game banners";

            try
            {
                var casinoDocType = contentTypeService.Get(DOCUMENT_TYPE_ALIAS);
                // Create the Template if it doesn't exist
                if (fileService.GetTemplate(TEMPLATE_ALIAS) == null)
                {
                    //then create the template
                    Template newTemplate = new Template(TEMPLATE_NAME, TEMPLATE_ALIAS);
                    ITemplate masterTemplate = fileService.GetTemplate(PARENT_TEMPLATE_ALIAS);
                    newTemplate.SetMasterTemplate(masterTemplate);
                    fileService.SaveTemplate(newTemplate);

                    // Set template for document type
                    casinoDocType.AddTemplate(contentTypeService, newTemplate);

                    ContentHelper.CopyPhysicalAssets(new ReconfigureCasinoPageEmbeddedResources());

                    ConnectorContext.AuditService.Add(AuditType.Save, -1, newTemplate.Id, "Template", $"Teplate '{TEMPLATE_NAME}' has been created and assigned");
                }

                var dataTypeContainer = dataTypeService.GetContainers(DATA_TYPE_CONTAINER, 1).FirstOrDefault();
                var dataTypeContainerId = -1;

                if (dataTypeContainer != null) dataTypeContainerId = dataTypeContainer.Id;
                var exists = dataTypeService.GetDataType(propertyName) != null;
                if (!exists)
                {
                    var created = Web.Composing.Current.PropertyEditors.TryGet("Umbraco.NestedContent", out IDataEditor editor);
                    if (created)
                    {
                        DataType bannerSliderNestedDataType = new DataType(editor, dataTypeContainerId)
                        {
                            Name = propertyName,
                            ParentId = dataTypeContainerId,
                            Configuration = new NestedContentConfiguration
                            {
                                MinItems = 0,
                                MaxItems = 999,
                                ConfirmDeletes = true,
                                HideLabel = false,
                                ShowIcons = true,
                                ContentTypes = new[]
                                {
                                    new NestedContentConfiguration.ContentType { Alias = NESTED_DOCUMENT_TYPE_ALIAS, TabAlias = SLIDERS_TAB }
                                }
                            }
                        };
                        dataTypeService.Save(bannerSliderNestedDataType);
                        ConnectorContext.AuditService.Add(AuditType.Save, -1, casinoDocType.Id, "Document Type", $"Document Type '{DOCUMENT_TYPE_ALIAS}' has been updated");
                    }
                }
                if (!casinoDocType.PropertyTypeExists(propertyAlias))
                {
                    var bannerSliderNestedDataType = dataTypeService.GetDataType(propertyName);
                    PropertyType BannerSlider = new PropertyType(bannerSliderNestedDataType, propertyAlias)
                    {
                        Name = propertyName,
                        Description = propertyDescription,
                        Variations = ContentVariation.Culture
                    };
                    casinoDocType.AddPropertyGroup(SLIDERS_TAB);
                    casinoDocType.AddPropertyType(BannerSlider, SLIDERS_TAB);
                    contentTypeService.Save(casinoDocType);
                }
            }

            catch (Exception ex)
            {
                logger.Error(typeof(_10_CasinoPageIntegration), ex.Message);
                logger.Error(typeof(_10_CasinoPageIntegration), ex.StackTrace);
            }
        }

        public void Initialize()
        {
            Reconfigure();
        }

        public void Terminate() { }
    }
}
