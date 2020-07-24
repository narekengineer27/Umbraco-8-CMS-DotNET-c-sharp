namespace Umbraco.Plugins.Connector.Controllers
{
    using Newtonsoft.Json;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Cache;
    using Umbraco.Plugins.Connector.Models;
    using Umbraco.Plugins.Connector.Services;
    using Umbraco.Web.Mvc;
    public abstract class BaseController : SurfaceController
    {
        protected readonly IContentService contentService;
        protected readonly TotalCodeApiService apiService;

        public BaseController()
        {
            apiService = new TotalCodeApiService();
            contentService = ConnectorContext.ContentService;
            using (var scope = ConnectorContext.ScopeProvider.CreateScope(autoComplete: true))
            {
                ApiKeyCache.UpdateCache(scope.Database);
            }
        }
    }
}
