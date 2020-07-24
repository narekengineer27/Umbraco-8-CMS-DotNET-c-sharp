using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Plugins.Connector.Models;
using Umbraco.Web;

namespace TotalCode.Core.Models
{
    public class TransactionViewModel
    {
        public string[] PredefinedAmounts { get; set; }
        public string Title { get; set; }
        public string CurrencySymbol { get; set; }
        public bool ShowReceipt { get; set; }
        public ReceiptModel Receipt { get; set; }
        public UmbracoHelper Umbraco { get; set; }
    }
}
