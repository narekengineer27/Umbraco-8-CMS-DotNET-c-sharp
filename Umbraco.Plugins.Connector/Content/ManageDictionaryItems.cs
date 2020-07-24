

namespace Umbraco.Plugins.Connector.Content
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Composing;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Core.PropertyEditors;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Dictionaries;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Services;
    using Umbraco.Web.PropertyEditors;
    public class _36_ManageDictionaryItems : IComponent
    {

        private readonly ILocalizationService localizationService;
        private readonly IDomainService domainService;
        private readonly IFileService fileService;
        private readonly ILogger logger;
        private readonly IContentTypeService contentTypeService;
        private readonly IDataTypeService dataTypeService;

        public _36_ManageDictionaryItems(ILocalizationService localizationService, IDomainService domainService, IFileService fileService, ILogger logger, IContentTypeService contentTypeService, IDataTypeService dataTypeService)
        {
            this.localizationService = localizationService;
            this.domainService = domainService;
            this.logger = logger;
            this.contentTypeService = contentTypeService;
            this.fileService = fileService;
            this.dataTypeService = dataTypeService;
        }


        public void Initialize()
        {
            var language = new LanguageDictionaryService(ConnectorContext.LocalizationService, ConnectorContext.DomainService, ConnectorContext.Logger);
            var dictionaryItems = new List<Type>();

            // Check if parent Key exists, and skip if true
            if (!language.CheckExists(typeof(AccountPage_ParentKey)))
                dictionaryItems.Add(typeof(AccountPage_ParentKey));

            if (!language.CheckExists(typeof(AccountPage_ChangeUsername)))
                dictionaryItems.Add(typeof(AccountPage_ChangeUsername));

            if (!language.CheckExists(typeof(AccountPage_CurrentUsername)))
                dictionaryItems.Add(typeof(AccountPage_CurrentUsername));

            if (!language.CheckExists(typeof(AccountPage_NewUsername)))
                dictionaryItems.Add(typeof(AccountPage_NewUsername));

            if (!language.CheckExists(typeof(Account_UsernameChangedSucccess)))
                dictionaryItems.Add(typeof(Account_UsernameChangedSucccess));

            if (!language.CheckExists(typeof(Account_UsernameChangedSucccessNotice)))
                dictionaryItems.Add(typeof(Account_UsernameChangedSucccessNotice));

            if (!language.CheckExists(typeof(Account_ChangeUsernameFailure)))
                dictionaryItems.Add(typeof(Account_ChangeUsernameFailure));

            if (!language.CheckExists(typeof(ServerErrors_ParentKey)))
                dictionaryItems.Add(typeof(ServerErrors_ParentKey));

            if (!language.CheckExists(typeof(ServerErrors_MatchingUsername)))
                dictionaryItems.Add(typeof(ServerErrors_MatchingUsername));

            if (!language.CheckExists(typeof(ServerErrors_UndefinedCustomer)))
                dictionaryItems.Add(typeof(ServerErrors_UndefinedCustomer));

            if (!language.CheckExists(typeof(ServerErrors_InvalidUsername)))
                dictionaryItems.Add(typeof(ServerErrors_InvalidUsername));

            if (dictionaryItems.Count > 0)
            {
                language.CreateDictionaryItems(dictionaryItems); // Create Dictionary Items

                ConnectorContext.AuditService.Add(AuditType.Save, -1, -1, "Dictionary Item", $"Common Dictionary Items have been created/updated");
            }
        }

        public void Terminate()
        {
            
        }
    }
}
