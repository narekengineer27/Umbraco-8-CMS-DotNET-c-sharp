namespace Umbraco.Plugins.Connector.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Cache;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Interfaces;
    using Umbraco.Plugins.Connector.Models;
    public class TicketController : BaseController
    {
        private readonly ITicketService _ticketService;
        private readonly IContentService _contentService;

        public TicketController(IContentService contentService, ITicketService ticketService)
        {
            _contentService = contentService;
            _ticketService = ticketService;
        }

        [HttpPost]
        public async Task<JsonResult> CreateTicket(CreateTicketModel ticket, string tenantUid)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(_contentService, tenantUid);
            var token = Request.Cookies["token"].Value;

            var response = await _ticketService.CreateTicket(tenantUid, token, origin, ticket);
            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> CreateTicketAnonymous(CreateTicketAnonymousModel ticket, string tenantUid)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(_contentService, tenantUid);
            var key = ApiKeyCache.GetByTenantUid(tenantUid);
            var authorization = await new Authorization().GetAuthorizationAsync(key);

            var response = await _ticketService.CreateTicketAnonymous(tenantUid, authorization.AccessToken, origin, ticket);
            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> CloseTicket(CloseTicketModel ticket, string tenantUid)
        {
            var token = Request.Cookies["token"].Value;
            var origin = TenantHelper.GetCurrentTenantUrl(_contentService, tenantUid);
            var response = await _ticketService.CloseTicket(tenantUid, token, origin, ticket);
            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> CreateMessage(CreateMessageModel message, string tenantUid)
        {
            var token = Request.Cookies["token"].Value;
            var origin = TenantHelper.GetCurrentTenantUrl(_contentService, tenantUid);

            var response = await _ticketService.CreateMessage(tenantUid, token, origin, message.TicketId, message.EmailAddress, message.MessageText, message.Attachment);
            return Json(response);
        }


        [HttpPost]
        public async Task<ActionResult> GetMessagesForTicket(string tenantUid, int id, string language, string messageText, bool isActive)
        {
            var origin = TenantHelper.GetCurrentTenantUrl(_contentService, tenantUid);
            var token = Request.Cookies["token"].Value;
            var ticket = ((TicketResponseContent)await _ticketService.GetTicket(tenantUid, token, origin, id)).Result;

            ViewData["currentLanguage"] = language;
            ViewData["NewMessageText"] = messageText;
            ViewData["IsActive"] = isActive;

            return PartialView("_TotalCodeTicketMessageList", ticket.TicketResponseMessages);
        }
    }
}