namespace Umbraco.Plugins.Connector.Models
{
    using System;
    public class CustomerSummary
    {
        public Balance Balance { get; set; }
        public string TenantPlatformMapGuid { get; set; }
        public string LanguageCode { get; set; }
        public string CurrencyCode { get; set; }
        public int OddsDisplay { get; set; }
        public string OddsDisplayName { get; set; }
        public string TimeZone { get; set; }
        public Guid? CustomerGuid { get; set; }
        public string Username { get; set; }
    }

    public class Balance
    {
        public string CurrentBalance { get; set; }
        public string Withdrawable { get; set; }
        public string Bonus { get; set; }
        public bool IsLiveBalance { get; set; }
        public string BalanceRetrievalFailureMessage { get; set; }
        public ResponseContentError Errors { get; set; }
    }
}
