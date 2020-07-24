namespace Umbraco.Plugins.Connector.Content
{
    using Umbraco.Core.Composing;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Core.Services;
    public class _18_HomeAddAlternateDomains : IComponent
    {
        public static string
            DOCUMENT_TYPE_ALIAS = "totalCodeHomePage",
            TENANT_TAB = "Tenant Info";

        private readonly IContentTypeService contentTypeService;
        private readonly IDataTypeService dataTypeService;
        private readonly ILogger logger;

        public _18_HomeAddAlternateDomains(IContentTypeService contentTypeService, IDataTypeService dataTypeService, ILogger logger)
        {
            this.contentTypeService = contentTypeService;
            this.dataTypeService = dataTypeService;
            this.logger = logger;
        }

        private void UpdateHomeDocumentType()
        {
            try
            {
                var contentType = contentTypeService.Get(DOCUMENT_TYPE_ALIAS);
                if (contentType != null)
                {
                    if (!contentType.PropertyTypeExists("alternateDomains"))
                    {
                        PropertyType tenantAltDomainsPropType = new PropertyType(dataTypeService.GetDataType(-92), "alternateDomains")
                        {
                            Name = "Alternate Domains",
                            Description = "Tenant Alternate Domains",
                            Variations = ContentVariation.Nothing,
                            SortOrder = 3
                        };
                        contentType.AddPropertyType(tenantAltDomainsPropType, TENANT_TAB);

                        contentTypeService.Save(contentType);
                        ConnectorContext.AuditService.Add(AuditType.Save, -1, contentType.Id, "Document Type", $"Document Type '{DOCUMENT_TYPE_ALIAS}' has been updated");
                    }
                }
            }
            catch (System.Exception ex)
            {
                logger.Error(typeof(_18_HomeAddAlternateDomains), ex.Message);
                logger.Error(typeof(_18_HomeAddAlternateDomains), ex.StackTrace);
            }
        }

        public void Initialize()
        {
            UpdateHomeDocumentType();
        }

        public void Terminate() { }
    }
}
