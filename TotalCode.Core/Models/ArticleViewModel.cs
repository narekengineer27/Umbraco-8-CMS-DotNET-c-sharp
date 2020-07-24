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
    public class ArticleViewModel
    {

        public ArticleViewModel(IPublishedContent content)
        {
            Title = content.Name;
            Body = content.Value("bodyText");
            Summary = content.Value<string>("bodyText").StripHtml().Truncate(100);
            Url = content.Url;
        }

        public string Title { get; set; }
        public string Summary { get; set; }
        public object Body { get; set; }
        public string Url { get; set; }
    }
}
