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
    public class _37_StylesheetBulkUploadMigration : IComponent
    {
        private readonly ILogger _logger;

        public _37_StylesheetBulkUploadMigration(ILogger logger)
        {
            _logger = logger;
        }

        public void Initialize()
        {
            try
            {
                ContentHelper.CopyPhysicalAssets(new StylesheetBulkUploadEmbeddedResources());
            }
            catch (System.Exception ex)
            {
                _logger.Error(typeof(_37_StylesheetBulkUploadMigration), ex.Message);
                _logger.Error(typeof(_37_StylesheetBulkUploadMigration), ex.StackTrace);
            }
        }

        public void Terminate() { }
    }
}
