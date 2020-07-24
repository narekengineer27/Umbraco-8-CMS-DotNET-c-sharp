namespace TotalCode.Core.Models.Pages
{
    using Umbraco.Core.Models.PublishedContent;
    public class GenericPageViewModel : BasePageViewModel
    {
        public GenericPageViewModel(IPublishedContent content) : base(content)
        {
        }
    }
}
