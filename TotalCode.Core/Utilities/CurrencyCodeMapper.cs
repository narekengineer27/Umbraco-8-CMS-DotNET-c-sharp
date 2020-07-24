namespace TotalCode.Core.Utilities
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Umbraco.Plugins.Connector.Models;
    public static class CurrencyCodeMapper
    {
        private static readonly Dictionary<string, string> SymbolsByCode;

        public static string GetSymbol(string code)
        {
            try
            {

                return SymbolsByCode[code];
            }
            catch
            {
                return code;
            }
        }

        static CurrencyCodeMapper()
        {
            SymbolsByCode = new Dictionary<string, string>();

            var regions = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                          .Select(x => new RegionInfo(x.Name));

            foreach (var region in regions)
            {
                if (!SymbolsByCode.ContainsKey(region.ISOCurrencySymbol))
                {
                    SymbolsByCode.Add(region.ISOCurrencySymbol, region.CurrencySymbol);
                }
            }

            SymbolsByCode.AddNonISOCurrencySymbols();
        }
    }
}
