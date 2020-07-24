using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TotalCode.Core.Utilities;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace TotalCode.Core.Models.NestedContents
{
    public class FooterLinkGroupViewModel
    {
        public FooterLinkGroupViewModel(IPublishedElement content)
        {
            Title = content.Value<string>("title");

            var linkTitle = "";
            var linkUrl = "";

            Links = new List<Links>();
            content.Value<IEnumerable<IPublishedElement>>("footerLinks").ToList().ForEach(
                item =>
                    {
                        if (item.HasValue("internalLink"))
                        {
                            var itemContent = item.Value<List<IPublishedContent>>("internalLink").First();
                            linkTitle = itemContent.Name;
                            linkUrl = itemContent.Url;
                        }
                        else if (item.HasValue("externalLinkText") && item.HasValue("externalLinkUrl"))
                        {
                            linkTitle = item.Value<string>("externalLinkText");
                            linkUrl = item.Value<string>("externalLinkUrl");
                        }

                        if (!string.IsNullOrEmpty(linkTitle) && !string.IsNullOrEmpty(linkUrl))
                            Links.Add(new Links()
                            {
                                Title = linkTitle,
                                URL = linkUrl
                            });
                    }
                );
        }

        public string Title { get; set; }
        public List<Links> Links { get; set; }
    }

    public class Links {
        public string Title{ get; set; }
        public string URL { get; set; }
    }
}
