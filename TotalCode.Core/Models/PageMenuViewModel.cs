namespace TotalCode.Core.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Web;
    public class PageMenuViewModel
    {
        public PageMenuViewModel(IPublishedContent content)
        {
            Id = content.Id;
            Title = content.Name;
            Body = content.Value<string>("bodyText");
            GameType = content.HasValue("gameType") ? content.Value<string>("gameType") : null;
            Url = content.Url;
            Alias = content.ContentType.Alias;
            Children = content.Children.Select(x => new PageMenuViewModel(x));
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Url { get; set; }
        public string Alias { get; set; }
        public string GameType { get; set; }
        public IEnumerable<PageMenuViewModel> Children { get; set; }
    }
}
