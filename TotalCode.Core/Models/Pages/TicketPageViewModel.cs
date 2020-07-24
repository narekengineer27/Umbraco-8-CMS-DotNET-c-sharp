using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Plugins.Connector.Models;

namespace TotalCode.Core.Models.Pages
{
    public class TicketPageViewModel : BasePageViewModel
    {
        public TicketPageViewModel(IPublishedContent content) : base(content)
        {
            
        }

        public Ticket Ticket { get; set; }
        public bool IsActive { get; set; }
        public bool OpenReply { get; set; }
    }
}
