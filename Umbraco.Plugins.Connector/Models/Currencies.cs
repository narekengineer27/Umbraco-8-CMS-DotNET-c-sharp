namespace Umbraco.Plugins.Connector.Models
{
    using System.Collections.Generic;
    public class Currencies
    {
        public IEnumerable<Currency> Codes { get; set; }
    }
    public class Currency
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return $"{Code} {Name}";
        }
    }
    public class CurrencyCodes
    {
        public string[] Codes { get; set; } 
    }
}
