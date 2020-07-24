using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Plugins.Connector.Models
{
    public class ReceiptModel
    {
        public string PaymentMethod { get; set; }
        public string PaymentSystemName { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        public string Amount { get; set; }
        public string TransactionNumber { get; set; }
        public string Gift { get; set; }
        public string Bonus { get; set; }
    }
}
