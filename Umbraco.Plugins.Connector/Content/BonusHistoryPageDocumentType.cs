using System;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Core.Logging;
using Umbraco.Core.Services;
using Umbraco.Core.Composing;
using Umbraco.Plugins.Connector.Helpers;
using System.Collections.Generic;
using Umbraco.Plugins.Connector.Services;
using Umbraco.Plugins.Connector.Dictionaries;

namespace Umbraco.Plugins.Connector.Content
{
    public class _35_BonusHistoryPageDocumentType : IComponent
    {
        public static string
          CONTAINER = "Container",
          PARENT_DOCUMENT_TYPE_ALIAS = "totalCodeBasePage",
          DOCUMENT_TYPE_ALIAS = "totalCodeBonusHistoryPage",
          DOCUMENT_TYPE_NAME = "Bonus History Page",
          DOCUMENT_TYPE_DESCRIPTION = "Total Code Bonus History Page",
          ICON = "icon-medal color-blue",
          TEMPLATE_ALIAS = "totalCodeBonusHistoryPageTemplate",
          TEMPLATE_NAME = "Total Code Bonus History Page Template";

        private readonly IContentTypeService contentTypeService;
        private readonly IDataTypeService dataTypeService;
        private readonly IFileService fileService;
        private readonly ILogger logger;

        public _35_BonusHistoryPageDocumentType(IContentTypeService contentTypeService,
                                            IDataTypeService dataTypeService,
                                            IFileService fileService,
                                            ILogger logger)
        {
            this.contentTypeService = contentTypeService;
            this.dataTypeService = dataTypeService;
            this.fileService = fileService;
            this.logger = logger;
        }

        private void CreateBonusHistoryDocumentType()
        {
            try
            {
                var container = contentTypeService.GetContainers(CONTAINER, 1).FirstOrDefault();
                var containerId = container.Id;
                var contentType = contentTypeService.Get(DOCUMENT_TYPE_ALIAS);
                var parentDocType = contentTypeService.Get(PARENT_DOCUMENT_TYPE_ALIAS);
                if (contentType == null)
                {
                    ContentType docType = (ContentType)contentType ?? new ContentType(containerId)
                    {
                        Name = DOCUMENT_TYPE_NAME,
                        Alias = DOCUMENT_TYPE_ALIAS,
                        AllowedAsRoot = false,
                        Description = DOCUMENT_TYPE_DESCRIPTION,
                        Icon = ICON,
                        SortOrder = 0,
                        Variations = ContentVariation.Culture,
                        ParentId = parentDocType.Id
                    };

                    // Create the Template if it doesn't exist
                    if (fileService.GetTemplate(TEMPLATE_ALIAS) == null)
                    {
                        //then create the template
                        Template newTemplate = new Template(TEMPLATE_NAME, TEMPLATE_ALIAS);
                        fileService.SaveTemplate(newTemplate);
                    }

                    // Set templates for document type
                    var template = fileService.GetTemplate(TEMPLATE_ALIAS);
                    docType.AllowedTemplates = new List<ITemplate> { template };
                    docType.SetDefaultTemplate(template);

                    contentTypeService.Save(docType);

                    // set as allowed content type in home
                    ContentHelper.AddAllowedDocumentType(contentTypeService, Phase2MergedHomeDocumentType.DOCUMENT_TYPE_ALIAS, DOCUMENT_TYPE_ALIAS);

                    ConnectorContext.AuditService.Add(AuditType.New, -1, docType.Id, "Document Type", $"Document Type '{DOCUMENT_TYPE_NAME}' has been created");

                    ContentHelper.CopyPhysicalAssets(new BonusHistoryPageEmbeddedResources());
                }
            }
            catch (System.Exception ex)
            {
                logger.Error(typeof(_35_BonusHistoryPageDocumentType), ex.Message);
                logger.Error(typeof(_35_BonusHistoryPageDocumentType), ex.StackTrace);
            }
        }
        private void CreateDictionaryLanguage()
        {
            try
            {
                var language = new LanguageDictionaryService(ConnectorContext.LocalizationService, ConnectorContext.DomainService, ConnectorContext.Logger);
                var dictionaryItems = new List<Type>();

                // Check if parent Key exists, and skip if true
                if (!language.CheckExists(typeof(BonusTransaction_ParentKey)))
                    dictionaryItems.Add(typeof(BonusTransaction_ParentKey));

                if (!language.CheckExists(typeof(BonusTransaction_Name)))
                    dictionaryItems.Add(typeof(BonusTransaction_Name));

                if (!language.CheckExists(typeof(BonusTransaction_Type)))
                    dictionaryItems.Add(typeof(BonusTransaction_Type));

                if (!language.CheckExists(typeof(BonusTransaction_Status)))
                    dictionaryItems.Add(typeof(BonusTransaction_Status));

                if (!language.CheckExists(typeof(BonusTransaction_RedemptionStatus)))
                    dictionaryItems.Add(typeof(BonusTransaction_RedemptionStatus));

                if (!language.CheckExists(typeof(BonusTransaction_BonusApplied)))
                    dictionaryItems.Add(typeof(BonusTransaction_BonusApplied));

                if (!language.CheckExists(typeof(BonusTransaction_DepositBonus)))
                    dictionaryItems.Add(typeof(BonusTransaction_DepositBonus));

                if (!language.CheckExists(typeof(BonusTransaction_Active)))
                    dictionaryItems.Add(typeof(BonusTransaction_Active));

                if (!language.CheckExists(typeof(BonusTransaction_Closed)))
                    dictionaryItems.Add(typeof(BonusTransaction_Closed));

                if (!language.CheckExists(typeof(BonusTransaction_Completed)))
                    dictionaryItems.Add(typeof(BonusTransaction_Completed));

                if (!language.CheckExists(typeof(BonusTransaction_Incomplete)))
                    dictionaryItems.Add(typeof(BonusTransaction_Incomplete));

                if (!language.CheckExists(typeof(BonusTransaction_RedemptionName)))
                    dictionaryItems.Add(typeof(BonusTransaction_RedemptionName));

                if (!language.CheckExists(typeof(BonusTransaction_RedemptionQualifierDifference)))
                    dictionaryItems.Add(typeof(BonusTransaction_RedemptionQualifierDifference));

                if (!language.CheckExists(typeof(BonusTransaction_RedemptionQualifierProgress)))
                    dictionaryItems.Add(typeof(BonusTransaction_RedemptionQualifierProgress));

                if (!language.CheckExists(typeof(BonusTransaction_RedemptionQualifierRequirement)))
                    dictionaryItems.Add(typeof(BonusTransaction_RedemptionQualifierRequirement));

                if (!language.CheckExists(typeof(BonusTransaction_RedemptionStatus)))
                    dictionaryItems.Add(typeof(BonusTransaction_RedemptionStatus));

                if (!language.CheckExists(typeof(BonusTransaction_TotalBetAmount)))
                    dictionaryItems.Add(typeof(BonusTransaction_TotalBetAmount));

                if (!language.CheckExists(typeof(BonusTransaction_TotalCasinoAmount)))
                    dictionaryItems.Add(typeof(BonusTransaction_TotalCasinoAmount));

                if (!language.CheckExists(typeof(BonusTransaction_TotalDepositAmount)))
                    dictionaryItems.Add(typeof(BonusTransaction_TotalDepositAmount));

                if (!language.CheckExists(typeof(BonusTransaction_TotalLiveCasinoAmount)))
                    dictionaryItems.Add(typeof(BonusTransaction_TotalLiveCasinoAmount));

                if (!language.CheckExists(typeof(BonusTransaction_TotalLotteryAmount)))
                    dictionaryItems.Add(typeof(BonusTransaction_TotalLotteryAmount));

                if (!language.CheckExists(typeof(BonusTransaction_TotalPokerAmount)))
                    dictionaryItems.Add(typeof(BonusTransaction_TotalPokerAmount));

                if (!language.CheckExists(typeof(BonusTransaction_TotalSportsbookAmount)))
                    dictionaryItems.Add(typeof(BonusTransaction_TotalSportsbookAmount));

                language.CreateDictionaryItems(dictionaryItems); // Create Dictionary Items
            }
            catch (System.Exception ex)
            {
                logger.Error(typeof(_35_BonusHistoryPageDocumentType), ex.Message);
                logger.Error(typeof(_35_BonusHistoryPageDocumentType), ex.StackTrace);
            }
        }
        public void Initialize()
        {
            CreateBonusHistoryDocumentType();
            CreateDictionaryLanguage();
        }

        public void Terminate() { }
    }
}
