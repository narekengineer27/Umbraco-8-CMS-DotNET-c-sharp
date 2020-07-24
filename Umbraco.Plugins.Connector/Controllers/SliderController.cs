namespace Umbraco.Plugins.Connector.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.UI;
    using Umbraco.Plugins.Connector.Cache;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Models;

    public class SliderController : BaseController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<JsonResult> GetGamesSlider(string tenantUid)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(contentService, tenantUid);
            var key = ApiKeyCache.GetByTenantUid(tenantUid);
            var authorization = await new Authorization().GetAuthorizationAsync(key);

            var response = await apiService.GetGameDataAsync(tenantUid, origin);

            return Json(response, JsonRequestBehavior.DenyGet);
        }
    }
}
