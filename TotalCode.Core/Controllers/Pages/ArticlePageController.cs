using TotalCode.Core.Models.Pages;
using TotalCode.Core.Pages.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Web;
using TotalCode.Core.Models;
using Umbraco.Web.Models;

namespace TotalCode.Core.Controllers.Pages
{
    public class TotalCodeArticlePageController : BasePageController
    {
        public ActionResult Index()
        {
            var model = GetModel<ArticlePageViewModel>(CurrentPage);

            model.Body = CurrentPage.Value("bodyText");
            model.ArticleCategory = new ArticleCategoryViewModel(CurrentPage.Parent);

            return CurrentTemplate(model);
        }
    }
}
