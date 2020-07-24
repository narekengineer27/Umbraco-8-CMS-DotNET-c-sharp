using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Plugins.Connector.Models;

namespace TotalCode.Core.Models.Pages
{
    public class TicketsPageViewModel : BasePageViewModel
    {
        public TicketsPageViewModel(IPublishedContent content) : base(content)
        {

        }

        public IEnumerable<TicketResult> Tickets { get; set; }
        public bool HasActiveTicket { get; set; }

    }
}
