namespace Umbraco.Plugins.Connector.Controllers
{
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Cache;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Interfaces;

    public class TicketFileController : BaseController
    {
        private readonly ITicketFileService _ticketFileService;
        private readonly IContentService _contentService;

        public TicketFileController(ITicketFileService ticketFileService, IContentService contentService)
        {
            _ticketFileService = ticketFileService;
            _contentService = contentService;
        }

        [HttpPost]
        public async Task<JsonResult> Upload(HttpPostedFileBase file, string tenantUid)
        {
            if (file.ContentLength > 0)
            {
                var origin = TenantHelper.GetCurrentTenantUrl(_contentService, tenantUid);
                var token = Request.Cookies["token"].Value;

                var response = await _ticketFileService.Upload(tenantUid, token, origin, file);
                return Json(response);
            }

            return Json(new EmptyResult());
        }

        [HttpPost]
        public async Task<JsonResult> UploadAnonymous(HttpPostedFileBase file, string tenantUid)
        {
            if(file.ContentLength > 0)
            {
                var origin = TenantHelper.GetCurrentTenantUrl(_contentService, tenantUid);
                var key = ApiKeyCache.GetByTenantUid(tenantUid);
                var authorization = await new Authorization().GetAuthorizationAsync(key);

                var response = await _ticketFileService.UploadAnonymous(tenantUid, authorization.AccessToken, origin, file);
                return Json(response);
            }

            return Json(new EmptyResult());
        }
    }
}
