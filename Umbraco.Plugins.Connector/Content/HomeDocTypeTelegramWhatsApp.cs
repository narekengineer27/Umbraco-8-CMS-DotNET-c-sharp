namespace Umbraco.Plugins.Connector.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Composing;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Core.Services;

    public class _27_HomeDocTypeTelegramWhatsApp : IComponent
    {
        public static string
            DOCUMENT_TYPE_ALIAS = "totalCodeHomePage",
            TAB = "Footer";

        private readonly IContentTypeService contentTypeService;
        private readonly IDataTypeService dataTypeService;
        private readonly IFileService fileService;
        private readonly ILogger logger;

        public _27_HomeDocTypeTelegramWhatsApp(IContentTypeService contentTypeService, IDataTypeService dataTypeService, IFileService fileService, ILogger logger)
        {
            this.contentTypeService = contentTypeService;
            this.dataTypeService = dataTypeService;
            this.fileService = fileService;
            this.logger = logger;
        }

        private void UpdateHomeDocumentType()
        {
            try
            {
                var contentType = contentTypeService.Get(DOCUMENT_TYPE_ALIAS);
                var changed = false;
                if (contentType != null)
                {
                    #region Telegram and WhatsApp

                    if (!contentType.PropertyTypeExists("telegramUsername"))
                    {
                        PropertyType tenantTelegramPropType = new PropertyType(dataTypeService.GetDataType(-88), "telegramUsername")
                        {
                            Name = "Telegram Username",
                            Description = "The Telegram Username",
                            Variations = ContentVariation.Culture
                        };
                        contentType.AddPropertyType(tenantTelegramPropType, TAB);
                        changed = true;
                    }

                    if (!contentType.PropertyTypeExists("whatsAppNumber"))
                    {
                        PropertyType tenantWhatsAppPropType = new PropertyType(dataTypeService.GetDataType(-88), "whatsAppNumber")
                        {
                            Name = "WhatsApp Number",
                            Description = "The Full Phone number including country code (+ NOT NECESSARY)",
                            Variations = ContentVariation.Culture
                        };
                        contentType.AddPropertyType(tenantWhatsAppPropType, TAB);
                        changed = true;
                    }

                    if (changed)
                        contentTypeService.Save(contentType);
                    #endregion
                }
            }
            catch (System.Exception ex)
            {
                logger.Error(typeof(_27_HomeDocTypeTelegramWhatsApp), ex.Message);
                logger.Error(typeof(_27_HomeDocTypeTelegramWhatsApp), ex.StackTrace);
            }
        }

        public void Initialize()
        {
            UpdateHomeDocumentType();
        }

        public void Terminate() { }
    }
}
