using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Core.Models.PublishedContent;

namespace TotalCode.Core.Models.Pages
{
    public class MarketingPageViewModel : BasePageViewModel
    {
        public MarketingPageViewModel(IPublishedContent content) : base(content)
        {
        }
    }
}
