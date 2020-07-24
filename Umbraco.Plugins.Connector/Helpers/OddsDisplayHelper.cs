namespace Umbraco.Plugins.Connector.Helpers
{
    using System;
    using Umbraco.Plugins.Connector.Models;
    public static class OddsDisplayHelper
    {
        public static string Text(this OddsDisplay odds)
        {
            switch (odds)
            {
                case OddsDisplay.Fractional: return "Fractional";
                case OddsDisplay.Decimal: return "Decimal";
                case OddsDisplay.American: return "American";
                case OddsDisplay.AmericanFractional: return "American Fractional";
                case OddsDisplay.None:
                default: return "None";
            }
        }
        public static int Value(string text)
        {
            if (text.Equals("American Fractional"))
                return (int)OddsDisplay.AmericanFractional;
            else
                return (int)Enum.Parse(typeof(OddsDisplay), text);
        }
    }
}
