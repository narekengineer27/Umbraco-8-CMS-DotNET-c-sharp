namespace Umbraco.Plugins.Connector.Content
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Composing;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Dictionaries;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Services;

    public class _09_EditAccountDocumentType : IComponent
    {
        public static string
            CONTAINER = "Total Code Container",
            PAGE_NAME = "Edit",
            PARENT_NODE_DOCUMENT_TYPE_ALIAS = "totalCodeAccountPage",
            PARENT_DOCUMENT_TYPE_ALIAS = "totalCodeBasePage",
            DOCUMENT_TYPE_ALIAS = "TotalCodeEditAccountPage",
            DOCUMENT_TYPE_NAME = "Total Code Edit Account Details",
            DOCUMENT_TYPE_DESCRIPTION = "Edit My Account Information Page",
            ICON = "icon-edit color-yellow",
            TEMPLATE_ALIAS = "totalCodeAccountEditPageTemplate",
            TEMPLATE_NAME = "Total Code Tenant Edit Account Details";

        private readonly IContentTypeService contentTypeService;
        private readonly IDataTypeService dataTypeService;
        private readonly IFileService fileService;
        private readonly ILocalizationService localizationService;
        private readonly IDomainService domainService;
        private readonly ILogger logger;

        private readonly bool createDictionaryItems = false;


        public _09_EditAccountDocumentType(ILocalizationService localizationService, IDomainService domainService, IContentTypeService contentTypeService, IDataTypeService dataTypeService, IFileService fileService, ILogger logger)
        {
            this.contentTypeService = contentTypeService;
            this.dataTypeService = dataTypeService;
            this.fileService = fileService;
            this.localizationService = localizationService;
            this.domainService = domainService;
            this.logger = logger;
            this.createDictionaryItems = false;
        }

        private void CreateDocumentType()
        {
            try
            {
                var container = contentTypeService.GetContainers(CONTAINER, 1).FirstOrDefault();
                var containerId = container.Id;
                var contentType = contentTypeService.Get(DOCUMENT_TYPE_ALIAS);
                var parentDocType = contentTypeService.Get(PARENT_NODE_DOCUMENT_TYPE_ALIAS);
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
                        var layoutAlias = "totalCodeLayout";
                        ITemplate masterTemplate = fileService.GetTemplate(layoutAlias);
                        //then create the template
                        Template newTemplate = new Template(TEMPLATE_NAME, TEMPLATE_ALIAS);
                        newTemplate.SetMasterTemplate(masterTemplate);
                        fileService.SaveTemplate(newTemplate);
                    }

                    // Set templates for document type
                    var template = fileService.GetTemplate(TEMPLATE_ALIAS);
                    docType.AllowedTemplates = new List<ITemplate> { template };
                    docType.SetDefaultTemplate(template);

                    contentTypeService.Save(docType);

                    // set as allowed content type in account home
                    ContentHelper.AddAllowedDocumentType(contentTypeService, PARENT_NODE_DOCUMENT_TYPE_ALIAS, DOCUMENT_TYPE_ALIAS);

                    ConnectorContext.AuditService.Add(AuditType.New, -1, docType.Id, "Document Type", $"Document Type '{DOCUMENT_TYPE_NAME}' has been created");

                    ContentHelper.CopyPhysicalAssets(new EditAccountDetailsEmbeddedResources());

                    if (createDictionaryItems)
                    {
                        // Check if parent Key exists, and skip if true
                        var language = new LanguageDictionaryService(localizationService, domainService, logger);
                        if (!language.CheckExists(typeof(Account_ParentKey)))
                        {
                            // Add Dictionary Items
                            var dictionaryItems = new List<Type>
                            {
                                typeof(Account_ParentKey),
                                typeof(Others_Save),
                                typeof(Account_AccountPageTitle),
                                typeof(Account_AccountEditPageTitle)
                            };
                            language.CreateDictionaryItems(dictionaryItems); // Create Dictionary Items

                            ConnectorContext.AuditService.Add(AuditType.Save, -1, -1, "Dictionary Items", $"Dictionaries Created");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(typeof(_09_EditAccountDocumentType), ex.Message);
                logger.Error(typeof(_09_EditAccountDocumentType), ex.StackTrace);
            }
        }

        public void Initialize()
        {
            CreateDocumentType();
        }

        public void Terminate() { }
    }
}
