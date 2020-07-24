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
    public class _08_SportsPageIntegration : IComponent
    {
        private readonly ILocalizationService localizationService;
        private readonly IDomainService domainService;
        private readonly IFileService fileService;
        private readonly ILogger logger;
        private readonly IContentTypeService contentTypeService;

        public static string
                        DOCUMENT_TYPE_ALIAS = "totalCodeGenericPage",
                        PARENT_TEMPLATE_ALIAS = "totalCodeLayout",
                        TEMPLATE_ALIAS = "totalCodeSportsFeedPageTemplate",
                        TEMPLATE_NAME = "Total Code Sports Feed Template";

        private readonly bool createDictionaryItems = false;


        public _08_SportsPageIntegration(ILocalizationService localizationService, IDomainService domainService, IFileService fileService, ILogger logger, IContentTypeService contentTypeService)
        {
            this.localizationService = localizationService;
            this.domainService = domainService;
            this.logger = logger;
            this.contentTypeService = contentTypeService;
            this.fileService = fileService;
            this.createDictionaryItems = false;
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

                    ContentHelper.CopyPhysicalAssets(new ReconfigureSportsPageEmbeddedResources());

                    ConnectorContext.AuditService.Add(AuditType.Save, -1, newTemplate.Id, "Template", $"Teplate '{TEMPLATE_NAME}' has been created and assigned");
                }

                if (createDictionaryItems)
                {
                    var language = new LanguageDictionaryService(localizationService, domainService, logger);
                    // Check if parent Key exists, and skip if true
                    if (!language.CheckExists(typeof(Pages_ParentKey)))
                    {
                        // Add Dictionary Items
                        var dictionaryItems = new List<Type>
                    {
                        typeof(Pages_ParentKey),
                        typeof(Pages_SportsPage),
                        typeof(Pages_SportEvents),
                        typeof(Pages_SportEventsEventName),
                        typeof(Pages_SportEventsEventScheduleTime),
                        typeof(Pages_SportEventsEventStatusDescription),
                        typeof(Pages_SportEventsEventTournament),
                        typeof(Pages_SportEventsEventCategory),
                        typeof(Pages_SportEventsEventCategorySport),
                        typeof(Pages_SportEventsNoEvents)
                    };
                        language.CreateDictionaryItems(dictionaryItems); // Create Dictionary Items
                    }
                }
            }

            catch (Exception ex)
            {
                logger.Error(typeof(_08_SportsPageIntegration), ex.Message);
                logger.Error(typeof(_08_SportsPageIntegration), ex.StackTrace);
            }
        }

        public void Initialize()
        {
            Reconfigure();
        }

        public void Terminate() { }
    }
}
