namespace TotalCode.Core.Models.Pages
{
    using System.Collections.Generic;
    using Umbraco.Core.Models.PublishedContent;
    public class CategoryPageViewModel : BasePageViewModel
    {
        public CategoryPageViewModel(IPublishedContent content) : base(content)
        {
        }

        public IEnumerable<ArticleViewModel> Articles { get; set; }
        public string Body { get; set; }
    }
}
