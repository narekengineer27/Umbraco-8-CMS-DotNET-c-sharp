namespace TotalCode.Core.Models.Pages
{
    using System.Linq;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Plugins.Connector.Models;
    using Umbraco.Plugins.Connector.Content;

    public class AccountPageViewModel : BasePageViewModel
    {
        public PayloadContent Customer { get; set; }
        public CustomerWallet CustomerWallet { get; set; }
        public IPublishedContent EditPage { get; set; }
        public string Url { get; set; }
        public AccountPageViewModel(IPublishedContent content) : base(content)
        {
            Url = content.Url;
            EditPage = content.Children.SingleOrDefault(x=>x.ContentType.Alias.Equals(_09_EditAccountDocumentType.DOCUMENT_TYPE_ALIAS));
        }
    }
}
