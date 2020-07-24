namespace Umbraco.Plugins.Connector.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Composing;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Core.PropertyEditors;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Helpers;

    public class _01_HomeDocumentTypePhase2 : IComponent
    {
        public static string
            CONTAINER = "Total Code Data Types",
            DOCUMENT_TYPE_ALIAS = "totalCodeHomePage",
            TENANT_TAB = "Tenant Info";

        private readonly IContentTypeService contentTypeService;
        private readonly IDataTypeService dataTypeService;
        private readonly IFileService fileService;
        private readonly ILogger logger;

        public _01_HomeDocumentTypePhase2(IContentTypeService contentTypeService, IDataTypeService dataTypeService, IFileService fileService, ILogger logger)
        {
            this.contentTypeService = contentTypeService;
            this.dataTypeService = dataTypeService;
            this.fileService = fileService;
            this.logger = logger;
        }

        private void UpdateHomeDocumentType()
        {
            const string editorAlias = "tenantPreferences";
            const string propertyAlias = "tenantPreferencesProperty";
            const string dataTypeName = "Total Code Tenant Properties";
            try
            {
                var container = dataTypeService.GetContainers(CONTAINER, 1).FirstOrDefault();
                var containerId = -1;

                if (container != null)
                    containerId = container.Id;

                var contentType = contentTypeService.Get(DOCUMENT_TYPE_ALIAS);
                if (contentType != null)
                {
                    #region Tenant Currencies
                    if (!contentType.PropertyTypeExists("tenantCurrencies"))
                    {
                        PropertyType tenantCurrenciesPropType = new PropertyType(dataTypeService.GetDataType(-92), "tenantCurrencies")
                        {
                            Name = "Tenant Currencies",
                            Description = "Total Code Tenant Used Currencies",
                            Variations = ContentVariation.Nothing
                        };
                        contentType.AddPropertyType(tenantCurrenciesPropType, TENANT_TAB);
                        contentTypeService.Save(contentType);
                    }
                    #endregion

                    var exists = dataTypeService.GetDataType(dataTypeName) != null;
                    if (!exists)
                    {
                        var created = Web.Composing.Current.PropertyEditors.TryGet(editorAlias, out IDataEditor editor);
                        if (created)
                        {
                            DataType tenantPreferencesDataType = new DataType(editor, containerId)
                            {
                                Name = dataTypeName
                            };
                            dataTypeService.Save(tenantPreferencesDataType);

                            PropertyType tenantPreferencesPropType = new PropertyType(tenantPreferencesDataType, propertyAlias)
                            {
                                Name = "Tenant Preferences",
                                Description = "Tenant Preferences for Customers"
                            };
                            contentType.AddPropertyType(tenantPreferencesPropType, TENANT_TAB);

                            contentTypeService.Save(contentType);
                            ConnectorContext.AuditService.Add(AuditType.Save, -1, contentType.Id, "Document Type", $"Document Type '{DOCUMENT_TYPE_ALIAS}' has been updated");

                            var tenantCurrencies = dataTypeService.GetDataType("tenantCurrencies");
                            if (tenantCurrencies.SortOrder != 9)
                            {
                                tenantCurrencies.SortOrder = 9;
                                tenantPreferencesDataType.SortOrder = 10;
                                dataTypeService.Save(new List<IDataType> { tenantPreferencesDataType, tenantCurrencies });
                            }
                        }
                        else
                        {
                            ContentHelper.CopyPhysicalAssets(new TenantPreferencesEmbeddedResources()); // copy property editor files before trying to create the data type
                            // will need to restart the app, in order to force a refresh to attempt to create the data type again
                            throw new DataTypeNotCreatedException("In order to create the Data Type, a Reload is required");
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                logger.Error(typeof(_01_HomeDocumentTypePhase2), ex.Message);
                logger.Error(typeof(_01_HomeDocumentTypePhase2), ex.StackTrace);
            }
        }

        public void Initialize()
        {
            UpdateHomeDocumentType();
        }

        public void Terminate() { }

        internal class DataTypeNotCreatedException : System.Exception
        {
            public DataTypeNotCreatedException(string Message) : base(Message) { }
        }
    }
}
