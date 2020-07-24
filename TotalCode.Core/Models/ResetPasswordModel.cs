namespace Umbraco.Plugins.Connector.Models
{
    using TotalCode.Core.Models.Pages;
    using Umbraco.Core.Models.PublishedContent;
    public class ResetPasswordModel : BasePageViewModel
    {
        public IPublishedContent Root { get; private set; }

        public ResetPasswordModel(IPublishedContent content) : base(content)
        {
            Root = content.Parent;
        }
    }
}
