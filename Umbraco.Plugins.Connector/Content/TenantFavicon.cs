namespace Umbraco.Plugins.Connector.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Composing;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Core.Services;

    public class _29_TenantFavicon : IComponent
    {
        public static string
            DOCUMENT_TYPE_ALIAS = "totalCodeHomePage",
            TENANT_TAB = "Tenant Info";

        private readonly IContentTypeService contentTypeService;
        private readonly IDataTypeService dataTypeService;
        private readonly IFileService fileService;
        private readonly ILogger logger;

        public _29_TenantFavicon(IContentTypeService contentTypeService, IDataTypeService dataTypeService, IFileService fileService, ILogger logger)
        {
            this.contentTypeService = contentTypeService;
            this.dataTypeService = dataTypeService;
            this.fileService = fileService;
            this.logger = logger;
        }

        private void UpdateHomeDocumentType()
        {
            const string faviconName = "Tenant Favicon";
            const string faviconDescription = "Favicon (Browser Tab icon)";
            const string faviconAlias = "tenantFavicon";
            const string size16x16 = "16x16", size32x32 = "32x32", size72x72 = "72x72", size144x144 = "144x144", size192x192 = "192x192", size256x256 = "256x256";
            try
            {
                var contentType = contentTypeService.Get(DOCUMENT_TYPE_ALIAS);
                if (contentType != null)
                {
                    var changed = false;
                    #region Tenant Favicons
                    if (!contentType.PropertyTypeExists($"{faviconAlias}{size16x16}"))
                    {
                        PropertyType tenantFavicon16x16PropType = new PropertyType(dataTypeService.GetDataType(-90), $"{faviconAlias}{size16x16}")
                        {
                            Name = $"{faviconName} {size16x16}",
                            Description = $"{faviconDescription} size {size16x16} (*.ico)",
                            Variations = ContentVariation.Nothing
                        };
                        contentType.AddPropertyType(tenantFavicon16x16PropType, TENANT_TAB);
                        changed = true;
                    }

                    if (!contentType.PropertyTypeExists($"{faviconAlias}{size32x32}"))
                    {
                        PropertyType tenantFavicon32x32PropType = new PropertyType(dataTypeService.GetDataType(-90), $"{faviconAlias}{size32x32}")
                        {
                            Name = $"{faviconName} {size32x32}",
                            Description = $"{faviconDescription} size {size32x32} (*.png)",
                            Variations = ContentVariation.Nothing
                        };
                        contentType.AddPropertyType(tenantFavicon32x32PropType, TENANT_TAB);
                        changed = true;
                    }

                    if (!contentType.PropertyTypeExists($"{faviconAlias}{size72x72}"))
                    {
                        PropertyType tenantFavicon72x72PropType = new PropertyType(dataTypeService.GetDataType(-90), $"{faviconAlias}{size72x72}")
                        {
                            Name = $"{faviconName} {size72x72}",
                            Description = $"{faviconDescription} size {size72x72} (*.png)",
                            Variations = ContentVariation.Nothing
                        };
                        contentType.AddPropertyType(tenantFavicon72x72PropType, TENANT_TAB);
                        changed = true;
                    }

                    if (!contentType.PropertyTypeExists($"{faviconAlias}{size144x144}"))
                    {
                        PropertyType tenantFavicon144x144PropType = new PropertyType(dataTypeService.GetDataType(-90), $"{faviconAlias}{size144x144}")
                        {
                            Name = $"{faviconName} {size144x144}",
                            Description = $"{faviconDescription} size {size144x144} (*.png)",
                            Variations = ContentVariation.Nothing
                        };
                        contentType.AddPropertyType(tenantFavicon144x144PropType, TENANT_TAB);
                        changed = true;
                    }

                    if (!contentType.PropertyTypeExists($"{faviconAlias}{size192x192}"))
                    {
                        PropertyType tenantFavicon192x192PropType = new PropertyType(dataTypeService.GetDataType(-90), $"{faviconAlias}{size192x192}")
                        {
                            Name = $"{faviconName} {size192x192}",
                            Description = $"{faviconDescription} size {size192x192} (*.png)",
                            Variations = ContentVariation.Nothing
                        };
                        contentType.AddPropertyType(tenantFavicon192x192PropType, TENANT_TAB);
                        changed = true;
                    }

                    if (!contentType.PropertyTypeExists($"{faviconAlias}{size256x256}"))
                    {
                        PropertyType tenantFavicon256x256PropType = new PropertyType(dataTypeService.GetDataType(-90), $"{faviconAlias}{size256x256}")
                        {
                            Name = $"{faviconName} {size256x256}",
                            Description = $"{faviconDescription} size {size256x256} (*.png)",
                            Variations = ContentVariation.Nothing
                        };
                        contentType.AddPropertyType(tenantFavicon256x256PropType, TENANT_TAB);
                        changed = true;
                    }

                    if (changed)
                        contentTypeService.Save(contentType);
                    #endregion
                }
            }
            catch (System.Exception ex)
            {
                logger.Error(typeof(_29_TenantFavicon), ex.Message);
                logger.Error(typeof(_29_TenantFavicon), ex.StackTrace);
            }
        }

        public void Initialize()
        {
            UpdateHomeDocumentType();
        }

        public void Terminate() { }
    }
}
