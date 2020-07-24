namespace TotalCode.Core.Controllers.Pages
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using TotalCode.Core.Models;
    using Umbraco.Core.Services;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using TotalCode.Core.Models.Pages;
    using TotalCode.Core.Pages.Controllers;
    using Umbraco.Plugins.Connector.Models;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Services;
    using Umbraco.Plugins.Connector.Interfaces;

    public class TotalCodeTicketsPageController : BasePageController
    {
        private readonly ITicketService _ticketService;
        private readonly IContentService _contentService;


        public TotalCodeTicketsPageController(ITicketService ticketService, IContentService contentService)
        {
            _ticketService = ticketService;
            _contentService = contentService;
        }

        public async Task<ActionResult> Index()
        {
            var model = GetModel<TicketsPageViewModel>(CurrentPage);
            var origin = TenantHelper.GetCurrentTenantUrl(_contentService, model.TenantUid);
            var tickets = (TicketSearchResponseContent)await _ticketService.GetTickets(model.TenantUid, model.Token, origin);

            model.Tickets = tickets.Result;
            model.HasActiveTicket = model.Tickets != null && model.Tickets.Any(ticket => ticket.ClosedDate == null);

            return CurrentTemplate(model);
        }
    }
}
