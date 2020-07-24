using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace TotalCode.Core.Models.Pages
{
    public class GenericInfoPageViewModel : BasePageViewModel
    {
        public string GenericInfoPageTitle { get; set; }
        public string GenericInfoPageContent { get; set; }
        public string TwoLetterISOLanguageName { get; set; }
        public GenericInfoPageViewModel(IPublishedContent content) : base(content)
        {

        }
    }
}