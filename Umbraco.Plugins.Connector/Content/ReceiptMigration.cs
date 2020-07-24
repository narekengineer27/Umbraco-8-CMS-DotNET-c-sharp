using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Composing;
using Umbraco.Core.Logging;
using Umbraco.Plugins.Connector.Dictionaries;
using Umbraco.Plugins.Connector.Helpers;
using Umbraco.Plugins.Connector.Services;

namespace Umbraco.Plugins.Connector.Content
{
    public class _43_ReceiptMigration : IComponent
    {
        private readonly ILogger _logger;

        public _43_ReceiptMigration(ILogger logger)
        {
            _logger = logger;
        }

        public void Initialize()
        {
            try
            {
                var language = new LanguageDictionaryService(ConnectorContext.LocalizationService, ConnectorContext.DomainService, ConnectorContext.Logger);
                var dictionaryItems = new List<Type>();

                if (!language.CheckExists(typeof(Receipt_Gift_ParentKey)))
                    dictionaryItems.Add(typeof(Receipt_Gift_ParentKey));

                if (!language.CheckExists(typeof(Receipt_Gift)))
                    dictionaryItems.Add(typeof(Receipt_Gift));

                if (!language.CheckExists(typeof(Receipt_Bonus)))
                    dictionaryItems.Add(typeof(Receipt_Bonus));

                language.CreateDictionaryItems(dictionaryItems);
            }
            catch (System.Exception ex)
            {
                _logger.Error(typeof(_43_ReceiptMigration), ex.Message);
                _logger.Error(typeof(_43_ReceiptMigration), ex.StackTrace);
            }
        }

        public void Terminate() { }
    }
}
