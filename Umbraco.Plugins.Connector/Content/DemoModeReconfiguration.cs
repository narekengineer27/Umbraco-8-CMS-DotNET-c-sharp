namespace Umbraco.Plugins.Connector.Content
{
    using System;
    using Umbraco.Core.Composing;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Core.Services;

    public class _21_DemoModeReconfiguration : IComponent
    {
        public static string
            DATA_TYPE_CONTAINER = "Total Code Data Types",
            CONTAINER = "Total Code Container",
            DOCUMENT_TYPE_ALIAS = "totalCodeGenericPage",
            DEMO = "Game Mode Dialog";

        private readonly IContentTypeService contentTypeService;
        private readonly IDataTypeService dataTypeService;
        private readonly ILogger logger;

        public _21_DemoModeReconfiguration(IContentTypeService contentTypeService, IDataTypeService dataTypeService, ILogger logger)
        {
            this.contentTypeService = contentTypeService;
            this.dataTypeService = dataTypeService;
            this.logger = logger;
        }

        private void CreateDocumentType()
        {
            try
            {

                // generic page
                var genericType = contentTypeService.Get(DOCUMENT_TYPE_ALIAS);
                if (genericType != null)
                {
                    var changed = false;
                    try
                    {
                        if (genericType.PropertyTypeExists("playButtonText"))
                        {
                            genericType.RemovePropertyType("playButtonText");
                            changed = true;
                        }

                        if (genericType.PropertyTypeExists("demoButtonText"))
                        {
                            genericType.RemovePropertyType("demoButtonText");
                            changed = true;
                        }

                        if (genericType.PropertyTypeExists("demoPageImages"))
                        {
                            genericType.RemovePropertyType("demoPageImages");
                            changed = true;
                        }

                        if (genericType.PropertyGroups["Demo Mode"] != null)
                        {
                            genericType.PropertyGroups.Remove("Demo Mode");
                            changed = true;
                        }

                        if (changed)
                            contentTypeService.Save(genericType);
                    }
                    catch { }

                    ConnectorContext.AuditService.Add(AuditType.Save, -1, genericType.Id, "Document Type", $"Document Type '{DOCUMENT_TYPE_ALIAS}' has been updated");

                }
            }
            catch (Exception ex)
            {
                logger.Error(typeof(_21_DemoModeReconfiguration), ex.Message);
                logger.Error(typeof(_21_DemoModeReconfiguration), ex.StackTrace);
            }
        }

        public void Initialize()
        {
            CreateDocumentType();
        }

        public void Terminate() { }
    }
}
