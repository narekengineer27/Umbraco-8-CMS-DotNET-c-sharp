namespace Umbraco.Plugins.Connector.Content
{
    using System;
    using System.Collections.Generic;
    using Umbraco.Core.Composing;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Plugins.Connector.Dictionaries;
    using Umbraco.Plugins.Connector.Services;

    public class _32_GenderDictionaries : IComponent
    {
        private readonly ILogger logger;
        private readonly bool createDictionaryItems = true;

        public _32_GenderDictionaries(ILogger logger)
        {
            this.logger = logger;
        }

        private void InsertDictionaries()
        {
            try
            {
                if (createDictionaryItems)
                {
                    var language = new LanguageDictionaryService(ConnectorContext.LocalizationService, ConnectorContext.DomainService, ConnectorContext.Logger);
                    var dictionaryItems = new List<Type>();

                    // Check if parent Key exists, and skip if true
                    if (!language.CheckExists(typeof(Genders_ParentKey)))
                        dictionaryItems.Add(typeof(Genders_ParentKey));

                    if (!language.CheckExists(typeof(Genders_Male)))
                        dictionaryItems.Add(typeof(Genders_Male));

                    if (!language.CheckExists(typeof(Genders_Female)))
                        dictionaryItems.Add(typeof(Genders_Female));

                    if (!language.CheckExists(typeof(Genders_Unknown)))
                        dictionaryItems.Add(typeof(Genders_Unknown));


                    language.CreateDictionaryItems(dictionaryItems); // Create Dictionary Items

                    ConnectorContext.AuditService.Add(AuditType.Save, -1, -1, "Dictionary Item", $"Gender Dictionary Items have been created/updated");

                }

            }
            catch (Exception ex)
            {
                logger.Error(typeof(_32_GenderDictionaries), ex.Message);
                logger.Error(typeof(_32_GenderDictionaries), ex.StackTrace);
            }
        }

        public void Initialize()
        {
            InsertDictionaries();
        }

        public void Terminate() { }
    }
}
