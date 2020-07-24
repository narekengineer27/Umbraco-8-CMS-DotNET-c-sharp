using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Composing;
using Umbraco.Core.Logging;
using Umbraco.Plugins.Connector.Helpers;

namespace Umbraco.Plugins.Connector.Content
{
    public class _42_StylesheetBulkDownloadMigration : IComponent
    {
        private readonly ILogger _logger;

        public _42_StylesheetBulkDownloadMigration(ILogger logger)
        {
            _logger = logger;
        }

        public void Initialize()
        {
            try
            {
                ContentHelper.CopyPhysicalAssets(new StylesheetBulkDownloadEmbeddedResources());
            }
            catch (System.Exception ex)
            {
                _logger.Error(typeof(_42_StylesheetBulkDownloadMigration), ex.Message);
                _logger.Error(typeof(_42_StylesheetBulkDownloadMigration), ex.StackTrace);
            }
        }

        public void Terminate() { }
    }
}
