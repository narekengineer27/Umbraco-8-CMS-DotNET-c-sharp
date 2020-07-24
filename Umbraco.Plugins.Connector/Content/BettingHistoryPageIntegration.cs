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
    public class _11_BettingHistoryPageIntegration : IComponent
    {
        private readonly ILocalizationService localizationService;
        private readonly IDomainService domainService;
        private readonly IFileService fileService;
        private readonly ILogger logger;
        private readonly IContentTypeService contentTypeService;

        public static string
                        DOCUMENT_TYPE_ALIAS = "totalCodeGenericPage",
                        PARENT_TEMPLATE_ALIAS = "totalCodeLayout",
                        TEMPLATE_ALIAS = "totalCodeBettingHistoryPageTemplate",
                        TEMPLATE_NAME = "Total Code Betting History Template";

        public _11_BettingHistoryPageIntegration(ILocalizationService localizationService, IDomainService domainService, IFileService fileService, ILogger logger, IContentTypeService contentTypeService)
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
                // Create the Template if it doesn't exist
                if (fileService.GetTemplate(TEMPLATE_ALIAS) == null)
                {
                    //then create the template
                    Template newTemplate = new Template(TEMPLATE_NAME, TEMPLATE_ALIAS);
                    ITemplate masterTemplate = fileService.GetTemplate(PARENT_TEMPLATE_ALIAS);
                    newTemplate.SetMasterTemplate(masterTemplate);
                    fileService.SaveTemplate(newTemplate);

                    // Set template for document type
                    var genericDocType = contentTypeService.Get(DOCUMENT_TYPE_ALIAS);
                    genericDocType.AddTemplate(contentTypeService, newTemplate);

                    ContentHelper.CopyPhysicalAssets(new ReconfigureBettingHistoryPageEmbeddedResources());

                    ConnectorContext.AuditService.Add(AuditType.Save, -1, newTemplate.Id, "Template", $"Teplate '{TEMPLATE_NAME}' has been created and assigned");
                }

                //var language = new LanguageDictionaryService(localizationService, domainService, logger);
                //// Check if parent Key exists, and skip if true
                //if (!language.CheckExists(typeof(Pages_ParentKey)))
                //{
                //    // Add Dictionary Items
                //    var dictionaryItems = new List<Type>
                //    {
                //        typeof(Pages_ParentKey),
                //        typeof(Pages_CasinoPage)
                //    };
                //    language.CreateDictionaryItems(dictionaryItems); // Create Dictionary Items
                //}
            }

            catch (Exception ex)
            {
                logger.Error(typeof(_11_BettingHistoryPageIntegration), ex.Message);
                logger.Error(typeof(_11_BettingHistoryPageIntegration), ex.StackTrace);
            }
        }

        public void Initialize()
        {
            Reconfigure();
        }

        public void Terminate() { }
    }
}
