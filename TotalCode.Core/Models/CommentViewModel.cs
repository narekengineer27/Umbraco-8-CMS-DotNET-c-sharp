using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace TotalCode.Core.Models
{
    public class CommentViewModel
    {
        public CommentViewModel(IPublishedContent content)
        {
            Title = content.Name;
            Body = content.Value<string>("comment");
            CreateDate = content.CreateDate;
            FormattedDate = content.CreateDate.ToString("MMM dd 'at' hh:mm tt");
        }

        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreateDate { get; set; }
        public string FormattedDate { get; set; }

    }
    
}
