using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models.PublishedContent;

namespace TotalCode.Core.Models.Pages
{
    public class TermsPageViewModel : BasePageViewModel
    {
        public TermsPageViewModel(IPublishedContent content) : base(content)
        {
        }

        public object Body { get; set; }
    }
}
