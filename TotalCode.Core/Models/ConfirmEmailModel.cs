namespace Umbraco.Plugins.Connector.Models
{
    using TotalCode.Core.Models.Pages;
    using Umbraco.Core.Models.PublishedContent;
    public class ConfirmEmailModel : BasePageViewModel
    {
        public IPublishedContent Root { get; private set; }
        public string MyAccountUrl { get; set; }

        public ConfirmEmailModel(IPublishedContent content) : base(content)
        {
            Root = content.Parent;
        }
    }
}
