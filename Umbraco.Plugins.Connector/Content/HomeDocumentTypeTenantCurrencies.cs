namespace Umbraco.Plugins.Connector.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Composing;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Core.Services;

    public class _14_HomeDocumentTypeTenantCurrencies : IComponent
    {
        public static string
            DOCUMENT_TYPE_ALIAS = "totalCodeHomePage",
            TENANT_TAB = "Tenant Info";

        private readonly IContentTypeService contentTypeService;
        private readonly IDataTypeService dataTypeService;
        private readonly IFileService fileService;
        private readonly ILogger logger;

        public _14_HomeDocumentTypeTenantCurrencies(IContentTypeService contentTypeService, IDataTypeService dataTypeService, IFileService fileService, ILogger logger)
        {
            this.contentTypeService = contentTypeService;
            this.dataTypeService = dataTypeService;
            this.fileService = fileService;
            this.logger = logger;
        }

        private void UpdateHomeDocumentType()
        {
            const string currenciesName = "Tenant Currencies";
            const string currenciesDescription = "Total Code Tenant Used Currencies";
            const string currenciesAlias = "tenantCurrencies";
            try
            {
                var contentType = contentTypeService.Get(DOCUMENT_TYPE_ALIAS);
                if (contentType != null)
                {
                    #region Tenant Currencies
                    var tenantCurrencies = contentType.PropertyTypes.SingleOrDefault(x => x.Alias == currenciesAlias);
                    if (tenantCurrencies == null)
                    {
                        PropertyType tenantCurrenciesPropType = new PropertyType(dataTypeService.GetDataType(-92), currenciesAlias)
                        {
                            Name = currenciesName,
                            Description = currenciesDescription,
                            Variations = ContentVariation.Nothing
                        };
                        contentType.AddPropertyType(tenantCurrenciesPropType, TENANT_TAB);
                        contentTypeService.Save(contentType);
                    }
                    #endregion
                }
            }
            catch (System.Exception ex)
            {
                logger.Error(typeof(_14_HomeDocumentTypeTenantCurrencies), ex.Message);
                logger.Error(typeof(_14_HomeDocumentTypeTenantCurrencies), ex.StackTrace);
            }
        }

        public void Initialize()
        {
            UpdateHomeDocumentType();
        }

        public void Terminate() { }
    }
}
