namespace TotalCode.Core.Controllers.Pages
{
    using Examine;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using TotalCode.Core.Models;
    using TotalCode.Core.Models.Pages;
    using TotalCode.Core.Pages.Controllers;
    using Umbraco.Examine;

    public class TotalCodeCategoriesPageController : BasePageController
    {

        public ActionResult Index(string q)
        {
            var model = GetModel<CategoriesPageViewModel>(CurrentPage);

            model.IsSearchMode = !string.IsNullOrEmpty(q);

            if (model.IsSearchMode)
            {
                if (ExamineManager.Instance.TryGetIndex("ExternalIndex", out IIndex index))
                {
                    ISearcher searcher = index.GetSearcher();
                    var fields = new List<string> { "nodeName", "content", "pageContent", "bodyText" };

                    ISearchResults results = searcher.CreateQuery("content").GroupedOr(fields, q).Execute();
                    model.SearchResults = results.Select(result => new PageViewModel(result, model.TenantUid, Umbraco)).Where(x => !string.IsNullOrEmpty(x.Title));
                    //model.SearchResults = results.Select(result => new PageViewModel(result, Umbraco));
                }
            }
            else
            {
                model.Categories = CurrentPage.Children.Select(content => new ArticleCategoryViewModel(content));
            }

            return CurrentTemplate(model);
        }
    }
}
