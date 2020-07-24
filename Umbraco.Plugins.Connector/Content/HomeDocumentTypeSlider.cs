namespace Umbraco.Plugins.Connector.Content
{
    using System.Linq;
    using Umbraco.Core.Composing;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Core.PropertyEditors;
    using Umbraco.Core.Services;
    using Umbraco.Web.PropertyEditors;

    public class _17_HomeDocumentTypeSlider : IComponent
    {
        public static string
            DATA_TYPE_CONTAINER = "Total Code Data Types",
            DOCUMENT_TYPE_CONTAINER = "Total Code Container",
            DOCUMENT_TYPE_ALIAS = "totalCodeHomePage",
            NESTED_DOCUMENT_TYPE_ALIAS = "BannerItem",
            NESTED_DOCUMENT_TYPE_NAME = "Banner Item",
            NESTED_DOCUMENT_TYPE_DESCRIPTION = "Banner Item Details",
            NESTED_DOCUMENT_TYPE_PARENT_ALIAS = "totalCodeBasePage",
            NESTED_DOCUMENT_TYPE_ICON = "icon-trophy",
            SLIDERS_TAB = "Sliders";

        private readonly IDataTypeService dataTypeService;
        private readonly IContentTypeService contentTypeService;
        private readonly ILogger logger;

        public _17_HomeDocumentTypeSlider(IContentTypeService contentTypeService, IDataTypeService dataTypeService, ILogger logger)
        {
            this.dataTypeService = dataTypeService;
            this.contentTypeService = contentTypeService;
            this.logger = logger;
        }

        private void UpdateDocumentType()
        {
            const string
                propertyAlias = "bannerSlider",
                propertyName = "Banner Slider",
                propertyDescription = "Slider with banner images";

            try
            {
                #region Nested Document Type
                var container = contentTypeService.GetContainers(DOCUMENT_TYPE_CONTAINER, 1).FirstOrDefault();
                int containerId = -1;

                if (container != null)
                    containerId = container.Id;

                var contentType = contentTypeService.Get(NESTED_DOCUMENT_TYPE_ALIAS);
                if (contentType == null)
                {

                    ContentType docType = (ContentType)contentType ?? new ContentType(containerId)
                    {
                        Name = NESTED_DOCUMENT_TYPE_NAME,
                        Alias = NESTED_DOCUMENT_TYPE_ALIAS,
                        AllowedAsRoot = false,
                        Description = NESTED_DOCUMENT_TYPE_DESCRIPTION,
                        Icon = NESTED_DOCUMENT_TYPE_ICON,
                        IsElement = true,
                        SortOrder = 0,
                        ParentId = contentTypeService.Get(NESTED_DOCUMENT_TYPE_PARENT_ALIAS).Id,
                        Variations = ContentVariation.Culture
                    };

                    docType.AddPropertyGroup(SLIDERS_TAB);

                    #region Nested Document Type Properties
                    PropertyType ImagePropType = new PropertyType(dataTypeService.GetDataType(-88), "sliderItemImage")
                    {
                        Name = "Image",
                        Description = "Image used in the Slider",
                        Variations = ContentVariation.Nothing
                    };
                    docType.AddPropertyType(ImagePropType, SLIDERS_TAB);

                    PropertyType ButtonLabelPropType = new PropertyType(dataTypeService.GetDataType(-88), "sliderItemButtonLabel")
                    {
                        Name = " Button Label",
                        Description = "Label for the Button",
                        Variations = ContentVariation.Nothing
                    };
                    docType.AddPropertyType(ButtonLabelPropType, SLIDERS_TAB);

                    PropertyType TitlePropType = new PropertyType(dataTypeService.GetDataType(-88), "sliderItemTitle")
                    {
                        Name = "Title",
                        Description = "Title for the banner item",
                        Variations = ContentVariation.Nothing
                    };
                    docType.AddPropertyType(TitlePropType, SLIDERS_TAB);

                    PropertyType SubtitlePropType = new PropertyType(dataTypeService.GetDataType(-88), "sliderItemSubtitle")
                    {
                        Name = "Subtitle",
                        Description = "Subtitle for the banner item",
                        Variations = ContentVariation.Nothing
                    };
                    docType.AddPropertyType(SubtitlePropType, SLIDERS_TAB);

                    PropertyType UrlPropType = new PropertyType(dataTypeService.GetDataType(-88), "sliderItemUrl")
                    {
                        Name = "Url",
                        Description = "The Link to the item",
                        Variations = ContentVariation.Nothing
                    };
                    docType.AddPropertyType(UrlPropType, SLIDERS_TAB);
                    #endregion

                    contentTypeService.Save(docType);
                    ConnectorContext.AuditService.Add(AuditType.New, -1, docType.Id, "Document Type", $"Document Type '{NESTED_DOCUMENT_TYPE_ALIAS}' has been created");
                }

                else
                {
                    if (contentType.PropertyTypeExists("sliderItemImage") && contentType.PropertyTypes.Single(x => x.Alias == "sliderItemImage").DataTypeId == -88)
                    {
                        var mediaPickerContainer = dataTypeService.GetContainers(DATA_TYPE_CONTAINER, 1).FirstOrDefault();
                        var mediaPickerContainerId = -1;

                        if (mediaPickerContainer != null) mediaPickerContainerId = mediaPickerContainer.Id;

                        var dataTypeExists = dataTypeService.GetDataType("sliderItemImageMediaPicker") != null;
                        if (!dataTypeExists)
                        {
                            var created = Web.Composing.Current.PropertyEditors.TryGet("Umbraco.MediaPicker", out IDataEditor editor);
                            if (created)
                            {
                                DataType mediaPickerSliderImage = new DataType(editor, mediaPickerContainerId)
                                {
                                    Name = "sliderItemImageMediaPicker",
                                    ParentId = mediaPickerContainerId,
                                    Configuration = new MediaPickerConfiguration
                                    {
                                        DisableFolderSelect = true,
                                        Multiple = false,
                                        OnlyImages = true
                                    }
                                };
                                dataTypeService.Save(mediaPickerSliderImage);
                                contentType.PropertyTypes.Single(x => x.Alias == "sliderItemImage").DataTypeId = mediaPickerSliderImage.Id;
                                contentTypeService.Save(contentType);
                            }
                        }
                    }
                }
                #endregion

                #region Update Home Document Type
                var homeDocumentType = contentTypeService.Get(DOCUMENT_TYPE_ALIAS);

                var dataTypeContainer = dataTypeService.GetContainers(DATA_TYPE_CONTAINER, 1).FirstOrDefault();
                var dataTypeContainerId = -1;

                if (dataTypeContainer != null) dataTypeContainerId = dataTypeContainer.Id;

                var exists = dataTypeService.GetDataType(propertyName) != null;
                if (!exists)
                {
                    var created = Web.Composing.Current.PropertyEditors.TryGet("Umbraco.NestedContent", out IDataEditor editor);
                    if (!created)
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
                    }
                }
                if (!homeDocumentType.PropertyTypeExists("bannerSlider"))
                {
                    var bannerSliderNestedDataType = dataTypeService.GetDataType(propertyName);
                    PropertyType BannerSlider = new PropertyType(bannerSliderNestedDataType, propertyAlias)
                    {
                        Name = propertyName,
                        Description = propertyDescription,
                        Variations = ContentVariation.Culture
                    };

                    var propertyGroup = homeDocumentType.PropertyGroups.SingleOrDefault(x => x.Name == SLIDERS_TAB);
                    if (propertyGroup == null)
                        homeDocumentType.AddPropertyGroup(SLIDERS_TAB);

                    homeDocumentType.AddPropertyType(BannerSlider, SLIDERS_TAB);
                    contentTypeService.Save(homeDocumentType);

                    ConnectorContext.AuditService.Add(AuditType.Save, -1, homeDocumentType.Id, "Document Type", $"Document Type '{DOCUMENT_TYPE_ALIAS}' has been updated");
                }

                #endregion
            }
            catch (System.Exception ex)
            {
                logger.Error(typeof(_17_HomeDocumentTypeSlider), ex.Message);
                logger.Error(typeof(_17_HomeDocumentTypeSlider), ex.StackTrace);
            }
        }

        public void Initialize()
        {
            UpdateDocumentType();
        }

        public void Terminate() { }
    }
}
