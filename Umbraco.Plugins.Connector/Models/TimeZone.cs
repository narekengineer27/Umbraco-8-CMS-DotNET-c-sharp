namespace Umbraco.Plugins.Connector.Models
{
    public class TimeZone
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string SimpleDisplayName { get; set; }
        public double UtcOffsetMinutes { get; set; }
        public string UtcOffsetDisplay { get; set; }
        public string Code { get; set; }
        public bool IsDaylightSavings { get; set; }
    }
}
