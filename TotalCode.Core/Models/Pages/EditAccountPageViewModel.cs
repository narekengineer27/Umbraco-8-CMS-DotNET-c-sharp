namespace TotalCode.Core.Models.Pages
{
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Plugins.Connector.Models;

    public class EditAccountPageViewModel : BasePageViewModel
    {
        public PayloadContent Customer { get; set; }
        public AccountBalanceContent CustomerWallet { get; set; }
        public string Url { get; set; }
        public string MyAccountUrl { get; set; }
        public EditAccountPageViewModel(IPublishedContent content) : base(content)
        {
            Url = content.Url;
            MyAccountUrl = content.Parent.Url;
        }
    }
}
