namespace Umbraco.Plugins.Connector.Content
{
    using System;
    using Umbraco.Core.Models;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Services;
    using Umbraco.Core.Composing;
    using System.Collections.Generic;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Services;
    using Umbraco.Plugins.Connector.Dictionaries;
    using System.Linq;

    public class _26_InPlayGamePageReconfiguration : IComponent
    {
        private readonly ILocalizationService localizationService;
        private readonly IDomainService domainService;
        private readonly IFileService fileService;
        private readonly ILogger logger;
        private readonly IContentTypeService contentTypeService;

        public static string
                        DOCUMENT_TYPE_ALIAS = "totalCodeGenericPage",
                        TEMPLATE_ALIAS = "totalCodeSportsFeedPageTemplate";

        public _26_InPlayGamePageReconfiguration(ILocalizationService localizationService, IDomainService domainService, IFileService fileService, ILogger logger, IContentTypeService contentTypeService)
        {
            this.localizationService = localizationService;
            this.domainService = domainService;
            this.logger = logger;
            this.contentTypeService = contentTypeService;
            this.fileService = fileService;
        }

        private void Reconfigure()
        {
            try
            {
                var genericDocType = contentTypeService.Get(DOCUMENT_TYPE_ALIAS);
                var template = (Template)fileService.GetTemplate(TEMPLATE_ALIAS);
                var alreadyAdded = genericDocType.AllowedTemplates.SingleOrDefault(x => x.Alias == TEMPLATE_ALIAS) != null;
                if (!alreadyAdded)
                {
                    genericDocType.AddTemplate(contentTypeService, template);
                    genericDocType.SetDefaultTemplate(template);
                    contentTypeService.Save(genericDocType);
                }

                ConnectorContext.AuditService.Add(AuditType.Save, -1, genericDocType.Id, "Document Type", $"Document Type '{DOCUMENT_TYPE_ALIAS}' has been updated");
            }

            catch (Exception ex)
            {
                logger.Error(typeof(_26_InPlayGamePageReconfiguration), ex.Message);
                logger.Error(typeof(_26_InPlayGamePageReconfiguration), ex.StackTrace);
            }
        }

        public void Initialize()
        {
            Reconfigure();
        }

        public void Terminate() { }
    }
}
