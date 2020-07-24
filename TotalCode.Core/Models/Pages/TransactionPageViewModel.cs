using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Plugins.Connector.Models;

namespace TotalCode.Core.Models.Pages
{
    public class TransactionPageViewModel : BasePageViewModel
    {
        public TransactionPageViewModel(IPublishedContent content) : base(content)
        {
        }

        public string[] PredefinedAmounts { get; set; }
        public bool ShowReceipt { get; set; }
        public ReceiptModel Receipt { get; set; }
    }
}
