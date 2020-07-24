using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Models;

namespace TotalCode.Core.Models.Pages
{
    public class CategoriesPageViewModel : BasePageViewModel
    {
        public CategoriesPageViewModel(IPublishedContent content) : base(content)
        {
        }
        public string Body { get; set; }
        public bool IsSearchMode { get; set; }
        public IEnumerable<ArticleCategoryViewModel> Categories { get; set; }
        public IEnumerable<PageViewModel> SearchResults { get; set; }
    }
}
