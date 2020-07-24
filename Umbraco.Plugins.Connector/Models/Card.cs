using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Plugins.Connector.Models
{
    public class Card
    {
        public string TenantUid { get; set; }
        public string CardNumber { get; set; }
        public string CardNumberLast4Digits => CardNumber.Substring(CardNumber.Length - 4);
        public string CardNumberMasked => $"{CardNumber.Substring(0, 4)}-XXXX-XXXX-{CardNumber.Substring(CardNumber.Length - 4)}";
        public string Iban { get; set; }
        public string BankName { get; set; }
        public string ShortBankAccountNumber { get; set; }
        public string AccountHolderName { get; set; }
        public string Status { get; set; }
        public string CustomerGuid { get; set; }
    }

    public class CardPayload
    {
        public int Id { get; set; }
        public string CustomerGuid { get; set; }
        public string AccountHolderName { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string Iban { get; set; }
        public int Status { get; set; }
        public string BankName { get; set; }
        public int ShortBankAccountNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CustomInfo { get; set; }
    }
}
