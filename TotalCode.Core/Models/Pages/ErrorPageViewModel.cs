namespace TotalCode.Core.Models.Pages
{
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Web.Models;

    public class ErrorPageViewModel : ContentModel
    {
        public ErrorPageViewModel(IPublishedContent content) : base(content)
        {
        }
        public string PageTitle { get; set; }
        public string PageContent { get; set; }
        public string TwoLetterISOLanguageName { get; set; }
    }
}
