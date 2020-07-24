namespace TotalCode.Core.Pages.Controllers
{
    using System.Web.Mvc;
    using TotalCode.Core.Models.Pages;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Web;
    using Umbraco.Web.Mvc;

    public class ErrorPageController : RenderMvcController
    {
        public ActionResult Index(IPublishedContent content)
        {
            var errorPage = new ErrorPageViewModel(content)
            {
                PageContent = CurrentPage.Value<string>("pageContent"),
                TwoLetterISOLanguageName = Umbraco.CultureDictionary.Culture.TwoLetterISOLanguageName,
                PageTitle = CurrentPage.Value<string>("errorTitle")
            };
            return CurrentTemplate(errorPage);
        }
    }
}

