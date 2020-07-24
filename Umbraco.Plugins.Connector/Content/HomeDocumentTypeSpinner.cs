using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Composing;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Plugins.Connector.Helpers;

namespace Umbraco.Plugins.Connector.Content
{
    public class _38_HomeDocumentTypeSpinner : IComponent
    {
        private readonly IContentTypeService _contentTypeService;
        private readonly IDataTypeService _dataTypeService;
        private readonly ILogger _logger;

        public static string
                        HOME_SPINNER_PROPERTY_ALIAS = "spinnerImage",
                        HOME_SPINNER_PROPERTY_NAME = "Spinner Image",
                        HOME_SPINNER_PROPERTY_DESCRIPTION = "Image for Spinner",
                        SPINNER_PROPERTY_TYPE_NAME = "Media Picker",
                        IMAGE_CROPPER_PROPERTY_TYPE_NAME = "Image Cropper",
                        FORM_IMAGE_CROPPER_ALIAS = "Form Spinner",
                        TAB_NAME = "Content",
                        DOCUMENT_TYPE_ALIAS = "totalCodeHomePage";

        public static int
                        FORM_IMAGE_CROPPER_ALIAS_HEIGHT = 32,
                        FORM_IMAGE_CROPPER_ALIAS_WIDTH = 32;


        public _38_HomeDocumentTypeSpinner(IContentTypeService contentTypeService,
            IDataTypeService dataTypeService,
            ILogger logger)
        {
            _contentTypeService = contentTypeService;
            _dataTypeService = dataTypeService;
            _logger = logger;
        }

        public void Initialize()
        {
            try
            {
                var homeDocType = _contentTypeService.Get(DOCUMENT_TYPE_ALIAS);
                if (!homeDocType.PropertyTypeExists(HOME_SPINNER_PROPERTY_ALIAS))
                {
                    PropertyType spinner = new PropertyType(_dataTypeService.GetDataType(SPINNER_PROPERTY_TYPE_NAME), HOME_SPINNER_PROPERTY_ALIAS)
                    {
                        Name = HOME_SPINNER_PROPERTY_NAME,
                        Description = HOME_SPINNER_PROPERTY_DESCRIPTION,
                        Variations = ContentVariation.Culture
                    };
                    homeDocType.AddPropertyType(spinner, TAB_NAME);
                    _contentTypeService.Save(homeDocType);
                }

                var imgCropper = _dataTypeService.GetDataType(IMAGE_CROPPER_PROPERTY_TYPE_NAME);
                if (imgCropper != null)
                {
                    if (imgCropper.Configuration is Umbraco.Core.PropertyEditors.ImageCropperConfiguration)
                    {
                        var imageCropperConfiguration = (Umbraco.Core.PropertyEditors.ImageCropperConfiguration)imgCropper.Configuration;

                        if (imageCropperConfiguration == null)
                        {
                            imageCropperConfiguration = new Umbraco.Core.PropertyEditors.ImageCropperConfiguration() { Crops = new Core.PropertyEditors.ImageCropperConfiguration.Crop[0] };
                        }

                        if (imageCropperConfiguration.Crops == null)
                        {
                            imageCropperConfiguration.Crops = new Core.PropertyEditors.ImageCropperConfiguration.Crop[0];
                        }

                        if (imageCropperConfiguration.Crops.FirstOrDefault(p => p.Alias == FORM_IMAGE_CROPPER_ALIAS) == null)
                        {
                            var crops = new List<Core.PropertyEditors.ImageCropperConfiguration.Crop>();
                            crops.AddRange(imageCropperConfiguration.Crops);
                            crops.Add(new Core.PropertyEditors.ImageCropperConfiguration.Crop()
                            {
                                Alias = FORM_IMAGE_CROPPER_ALIAS,
                                Height = FORM_IMAGE_CROPPER_ALIAS_HEIGHT,
                                Width = FORM_IMAGE_CROPPER_ALIAS_WIDTH
                            });
                            imageCropperConfiguration.Crops = crops.ToArray();

                            _dataTypeService.Save(imgCropper);

                            _logger.Info(typeof(_38_HomeDocumentTypeSpinner), $"{IMAGE_CROPPER_PROPERTY_TYPE_NAME} Alias saved: {JsonConvert.SerializeObject(imageCropperConfiguration.Crops)}");
                        }
                        else
                        {
                            _logger.Info(typeof(_38_HomeDocumentTypeSpinner), $"{IMAGE_CROPPER_PROPERTY_TYPE_NAME} Alias {FORM_IMAGE_CROPPER_ALIAS} Already Exists");
                        }
                    }
                    else
                    {
                        _logger.Info(typeof(_38_HomeDocumentTypeSpinner), $"Invalid type for imgCropper.Configuration TYPE: {imgCropper.Configuration.GetType()}");
                    }
                }
                else
                {
                    _logger.Info(typeof(_38_HomeDocumentTypeSpinner), $"Datatype {IMAGE_CROPPER_PROPERTY_TYPE_NAME} Not Found");
                }
            }
            catch (System.Exception ex)
            {
                _logger.Error(typeof(_38_HomeDocumentTypeSpinner), ex.Message);
                _logger.Error(typeof(_38_HomeDocumentTypeSpinner), ex.StackTrace);
            }
        }

        public void Terminate() { }
    }
}
