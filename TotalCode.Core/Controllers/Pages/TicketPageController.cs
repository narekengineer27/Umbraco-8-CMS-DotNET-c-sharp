namespace TotalCode.Core.Controllers.Pages
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using TotalCode.Core.Models.Pages;
    using TotalCode.Core.Pages.Controllers;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Interfaces;
    using Umbraco.Plugins.Connector.Models;

    public class TotalCodeTicketPageController : BasePageController
    {
        private readonly ITicketService _ticketService;
        private readonly IContentService _contentService;

        public TotalCodeTicketPageController(ITicketService ticketService, IContentService contentService)
        {
            _ticketService = ticketService;
            _contentService = contentService;
        }

        public async Task<ActionResult> Index(int id, bool openReply = false)
        {
            var model = GetModel<TicketPageViewModel>(CurrentPage);
            var origin = TenantHelper.GetCurrentTenantUrl(_contentService, model.TenantUid);

            model.Ticket = ((TicketResponseContent)await _ticketService.GetTicket(model.TenantUid, model.Token, origin, id)).Result;
            model.IsActive = model.Ticket?.ClosedDate == null;
            model.OpenReply = openReply;

            return CurrentTemplate(model);
        }
    }
}
