using Examine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Plugins.Connector.Helpers;
using Umbraco.Web;

namespace TotalCode.Core.Models
{
    public class PageViewModel
    {
        public PageViewModel(ISearchResult result, UmbracoHelper helper)
        {
            var content = helper.Content(result.Id);
            if (content != null)
            {
                Id = content.Id;
                Title = content.Name;
                Body = content.Value<string>("bodyText");
                Url = content.Url;
                PageContent = content.Value<string>("pageContent");
            }
        }
        public PageViewModel(ISearchResult result, string tenantUid, UmbracoHelper helper)
        {
            var tenantRoot = new NodeHelper().GetTenantRoot(tenantUid);
            var contentRoot = helper.Content(tenantRoot.Id);
            var content = contentRoot.Children().SingleOrDefault(x => x.Id == int.Parse(result.Id));

            if (content != null)
            {
                Id = content.Id;
                Title = content.Name;
                Body = content.Value<string>("bodyText");
                Url = content.Url;
                PageContent = content.Value<string>("pageContent");
            }
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Url { get; set; }
        public string PageContent { get; set; }
    }
}
