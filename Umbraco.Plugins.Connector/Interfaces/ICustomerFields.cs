namespace Umbraco.Plugins.Connector.Interfaces
{
    public interface ICustomerFields
    {
        string AddressLine1 { get; set; }
        string AddressLine2 { get; set; }
        string AddressLine3 { get; set; }
        string Country { get; set; }
        string CountryCode { get; set; }
        string County { get; set; }
        string CurrencyCode { get; set; }
        string DOB { get; set; }
        string FirstName { get; set; }
        string Gender { get; set; }
        string LanguageCode { get; set; }
        string LastName { get; set; }
        string PostCode { get; set; }
        string TimeZoneCode { get; set; }
        string Title { get; set; }
        string Town { get; set; }
        string OddsDisplay { get; set; }
        string NotificationComPref { get; set; }
    }
}
