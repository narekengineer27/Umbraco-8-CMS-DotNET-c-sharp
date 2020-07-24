namespace TotalCode.Core.Controllers.Pages
{
    using System.Linq;
    using System.Web.Mvc;
    using TotalCode.Core.Models;
    using TotalCode.Core.Models.Pages;
    using TotalCode.Core.Pages.Controllers;
    using Umbraco.Web;
    public class TotalCodeCategoryPageController : BasePageController
    {
        public ActionResult Index()
        {
            var model = GetModel<CategoryPageViewModel>(CurrentPage);
            
            model.Articles = CurrentPage.Children.Select(content => new ArticleViewModel(content));
            model.Body = CurrentPage.Value<string>("pageContent");

            return CurrentTemplate(model);
        }
    }
}
