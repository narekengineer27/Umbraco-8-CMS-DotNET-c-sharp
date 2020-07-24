namespace Umbraco.Plugins.Connector.Models
{
    using System;
    public class ValidationErrorItem
    {
        public string Location { get; set; }
        public int Row { get; set; }
        public Exception Exception { get; set; }
    }
}
