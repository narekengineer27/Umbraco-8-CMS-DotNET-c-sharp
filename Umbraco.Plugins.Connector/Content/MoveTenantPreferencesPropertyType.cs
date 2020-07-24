namespace Umbraco.Plugins.Connector.Content
{
    using System.Linq;
    using Umbraco.Core.Composing;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Core.Services;

    public class _16_MoveTenantPreferencesPropertyType : IComponent
    {
        public static string
            CONTAINER = "Total Code Data Types";

        private readonly IDataTypeService dataTypeService;
        private readonly ILogger logger;

        public _16_MoveTenantPreferencesPropertyType(IDataTypeService dataTypeService, ILogger logger)
        {
            this.dataTypeService = dataTypeService;
            this.logger = logger;
        }

        private void UpdateHomeDocumentType()
        {
            const string
            propertyName = "Tenant Preferences",
            dataTypeName = "Total Code Tenant Properties",
            oldDataTypeName = "Total Tode Tenant Properties";

            try
            {
                var existingDataType = dataTypeService.GetDataType(oldDataTypeName);
                if (existingDataType != null)
                {
                    existingDataType.Name = dataTypeName;
                    dataTypeService.Save(existingDataType);

                    var container = dataTypeService.GetContainers(CONTAINER, 1).FirstOrDefault();
                    var containerId = -1;

                    if (container != null)
                        containerId = container.Id;

                    dataTypeService.Move(existingDataType, containerId);
                    ConnectorContext.AuditService.Add(AuditType.Move, -1, existingDataType.Id, "Data Type", $"Data Type '{propertyName}' has been updated and moved");
                }
            }
            catch (System.Exception ex)
            {
                logger.Error(typeof(_16_MoveTenantPreferencesPropertyType), ex.Message);
                logger.Error(typeof(_16_MoveTenantPreferencesPropertyType), ex.StackTrace);
            }
        }

        public void Initialize()
        {
            UpdateHomeDocumentType();
        }

        public void Terminate() { }
    }
}
