namespace Umbraco.Plugins.Connector.Content
{
    using System;
    using System.Collections.Generic;
    using Umbraco.Core.Composing;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Dictionaries;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Services;

    public class _05_Phase2Milestone7DictionaryMigration : IComponent
    {
        private readonly ILocalizationService localizationService;
        private readonly IDomainService domainService;
        private readonly ILogger logger;

        private readonly bool createDictionaryItems = false;


        public _05_Phase2Milestone7DictionaryMigration(ILocalizationService localizationService, IDomainService domainService, ILogger logger)
        {
            this.localizationService = localizationService;
            this.domainService = domainService;
            this.logger = logger;
            this.createDictionaryItems = false;
        }

        private void CreateDictionaryItems()
        {
            try
            {
                if (createDictionaryItems)
                {
                    var language = new LanguageDictionaryService(localizationService, domainService, logger);
                    // Check if parent Key exists, and skip if true
                    if (!language.CheckExists(typeof(Login_ParentKey)))
                    {
                        // Add Dictionary Items
                        var dictionaryItems = new List<Type>
                    {
                        typeof(Login_ParentKey),
                        typeof(Login_EmailUsernameOrPhone),
                        typeof(Login_EmailUsernameOrPhonePlaceholder),
                        typeof(Login_ForgotPassword),
                        typeof(Login_Password),
                        typeof(Login_RememberMe),
                        typeof(Login_Login),
                        typeof(Login_DontHaveAccount),
                        typeof(Login_LoginSuccess),
                        typeof(Login_LoginFailure)
                    };
                        language.CreateDictionaryItems(dictionaryItems); // Create Dictionary Items

                        ConnectorContext.AuditService.Add(AuditType.Save, -1, -1, "Dictionary Items", $"Dictionaries Created");

                    }
                }

                var firstEmbeddedResource = new LoginEmbeddedResources().Resources[0];
                if (!ContentHelper.AssetAlreadyExists(firstEmbeddedResource.FileName, firstEmbeddedResource.OutputDirectory))
                {
                    ContentHelper.CopyPhysicalAssets(new LoginEmbeddedResources());
                }
            }
            catch (Exception ex)
            {
                logger.Error(typeof(_05_Phase2Milestone7DictionaryMigration), ex.Message);
                logger.Error(typeof(_05_Phase2Milestone7DictionaryMigration), ex.StackTrace);
            }
        }

        public void Initialize()
        {
            CreateDictionaryItems();
        }

        public void Terminate() { }
    }
}
