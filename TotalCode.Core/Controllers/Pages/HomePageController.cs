namespace TotalCode.Core.Controllers.Pages
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using TotalCode.Core.Models.Pages;
    using TotalCode.Core.Pages.Controllers;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector;
    using Umbraco.Plugins.Connector.Cache;
    using Umbraco.Plugins.Connector.Services;

    public class TotalCodeHomePageController : BasePageController
    {
        private readonly TotalCodeApiService apiService;
        private readonly IContentService contentService;
        public TotalCodeHomePageController(IContentService contentService)
        {
            this.contentService = contentService;
            this.apiService = new TotalCodeApiService();
            using (var scope = ConnectorContext.ScopeProvider.CreateScope(autoComplete: true))
            {
                //ApiKeyCache.UpdateCache(scope.Database);
            }
        }
        public async Task<ActionResult> Index()
        {
            var model = GetModel<GamePageViewModel>(CurrentPage);

            model.IsHomePage = true;
            var gamesHelper = new GamesHelper(contentService, apiService);
            model = await gamesHelper.ModelAsync<GamePageViewModel>(model, CurrentPage, Request);
            return CurrentTemplate(model);
        }
    }
}
