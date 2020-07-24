namespace Umbraco.Plugins.Connector.Content
{
    using Umbraco.Core.Composing;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Core.PropertyEditors;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Helpers;

    public class _13_HomeDocumentTypePaymentSettings : IComponent
    {
        public static string
            DOCUMENT_TYPE_ALIAS = "totalCodeHomePage",
            TENANT_TAB = "Tenant Info";

        private readonly IContentTypeService contentTypeService;
        private readonly IDataTypeService dataTypeService;
        private readonly IFileService fileService;
        private readonly ILogger logger;

        public _13_HomeDocumentTypePaymentSettings(IContentTypeService contentTypeService, IDataTypeService dataTypeService, IFileService fileService, ILogger logger)
        {
            this.contentTypeService = contentTypeService;
            this.dataTypeService = dataTypeService;
            this.fileService = fileService;
            this.logger = logger;
        }

        private void UpdateHomeDocumentType()
        {
            const string editorAlias = "paymentSettings";
            const string propertyAlias = "paymentSettingsProperty";
            const string dataTypeName = "Tenant Payment Methods";
            try
            {
                var contentType = contentTypeService.Get(DOCUMENT_TYPE_ALIAS);
                if (contentType != null)
                {
                    var exists = dataTypeService.GetDataType(dataTypeName) != null;
                    if (!exists)
                    {
                        var created = Web.Composing.Current.PropertyEditors.TryGet(editorAlias, out IDataEditor editor);
                        if (created)
                        {
                            DataType tenantPreferencesDataType = new DataType(editor)
                            {
                                Name = dataTypeName
                            };
                            dataTypeService.Save(tenantPreferencesDataType);

                            PropertyType tenantPreferencesPropType = new PropertyType(tenantPreferencesDataType, propertyAlias)
                            {
                                Name = dataTypeName,
                                Description = "Payment Options for Customers"
                            };
                            contentType.AddPropertyType(tenantPreferencesPropType, TENANT_TAB);

                            contentTypeService.Save(contentType);
                            ConnectorContext.AuditService.Add(AuditType.Save, -1, contentType.Id, "Document Type", $"Document Type '{DOCUMENT_TYPE_ALIAS}' has been updated");

                        }
                        else
                        {
                            ContentHelper.CopyPhysicalAssets(new PaymentSettingsEmbeddedResources()); // copy property editor files before trying to create the data type
                            // will need to restart the app, in order to force a refresh to attempt to create the data type again
                            throw new DataTypeNotCreatedException("In order to create the Data Type, a Reload is required");
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                logger.Error(typeof(_13_HomeDocumentTypePaymentSettings), ex.Message);
                logger.Error(typeof(_13_HomeDocumentTypePaymentSettings), ex.StackTrace);
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
