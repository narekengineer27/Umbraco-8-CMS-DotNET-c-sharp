namespace Umbraco.Plugins.Connector.Content
{
    using System;
    using System.Linq;
    using Umbraco.Core.Composing;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Helpers;

    public class _12_EditAccountDocumentType : IComponent
    {
        public static string
            PARENT_NODE_DOCUMENT_TYPE_ALIAS = "totalCodeAccountPage",
            DOCUMENT_TYPE_ALIAS = "TotalCodeEditAccountPage";

        private readonly IContentTypeService contentTypeService;
        private readonly IDataTypeService dataTypeService;
        private readonly ILogger logger;

        public _12_EditAccountDocumentType(IContentTypeService contentTypeService, IDataTypeService dataTypeService, ILogger logger)
        {
            this.contentTypeService = contentTypeService;
            this.dataTypeService = dataTypeService;
            this.logger = logger;
        }

        private void ConfigureDocumentType()
        {
            try
            {
                var contentType = contentTypeService.Get(DOCUMENT_TYPE_ALIAS);
                var parentDocType = contentTypeService.Get(PARENT_NODE_DOCUMENT_TYPE_ALIAS);
                if (parentDocType.AllowedContentTypes.SingleOrDefault(x => x.Alias.Equals(DOCUMENT_TYPE_ALIAS)) == null)
                {
                   // set as allowed content type in account home
                    ContentHelper.AddAllowedDocumentType(contentTypeService, PARENT_NODE_DOCUMENT_TYPE_ALIAS, DOCUMENT_TYPE_ALIAS);

                    ConnectorContext.AuditService.Add(AuditType.Move, -1, contentType.Id, "Document Type", $"Document Type '{DOCUMENT_TYPE_ALIAS}' has been updated");
                }
            }
            catch (Exception ex)
            {
                logger.Error(typeof(_12_EditAccountDocumentType), ex.Message);
                logger.Error(typeof(_12_EditAccountDocumentType), ex.StackTrace);
            }
        }

        public void Initialize()
        {
            ConfigureDocumentType();
        }

        public void Terminate() { }
    }
}
