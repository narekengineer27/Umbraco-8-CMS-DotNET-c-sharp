using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace TotalCode.Core.Models
{

    public class TicketViewModel
    {
        public TicketViewModel(IPublishedContent content)
        {
            IsActive = content.Value<bool>("closed") == false;
            Title = content.Name;
            Summary = content.Value<string>("bodyText").Truncate(130);
            Body = content.Value<string>("bodyText");
            Url = content.Url;
            Date = content.CreateDate;
            FormattedDate = content.CreateDate.ToString("MMM dd 'at' hh:mm tt");
        }

        public bool IsActive { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Body { get; set; }
        public string Url { get; set; }
        public DateTime Date { get; set; }
        public string FormattedDate { get; set; }
    }

}
