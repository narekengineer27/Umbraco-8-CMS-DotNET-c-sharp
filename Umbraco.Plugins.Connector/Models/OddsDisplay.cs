namespace Umbraco.Plugins.Connector.Models
{
    using System;
    [Flags]
    public enum OddsDisplay
    {
        Fractional = 1,
        Decimal = 2,
        American = 3,
        AmericanFractional = 4,
        None = 5
    }
}
