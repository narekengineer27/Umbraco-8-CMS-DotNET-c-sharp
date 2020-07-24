namespace Umbraco.Plugins.Connector.Models
{
    using NodaTime;
    using NodaTime.TimeZones;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;
    using TimeZoneNames;
    using Umbraco.Plugins.Connector.Helpers;
    public static class DefaultAllowedValues
    {
        #region NON ISO CURRENCY NAMES
        private const string BITCOIN = "Bitcoin";
        private const string TOMAN = "Toman";
        private const string RIYAL = "Riyal";
        private const string USDOLLAR = "US Dollar";

        private const string BITCOIN_CODE = "BTC";
        private const string TOMAN_CODE = "IRT";
        private const string RIYAL_CODE = "IRR";
        private const string USDOLLAR_CODE = "USD";

        private const string BITCOIN_SYMBOL = "฿";
        private const string TOMAN_SYMBOL = "تومان";
        private const string RIYAL_SYMBOL = "﷼";
        private const string USDOLLAR_SYMBOL = "$";
        #endregion

        #region Default Value Public Methods
        public static string[] GenerateDefaultYears(int startYear = 1900)
        {
            var yearsToSubtract = DateTime.Now.Year - startYear;
            List<string> years = new List<string>();
            for (int i = 1; i <= yearsToSubtract; i++)
            {
                years.Add(DateTime.Now.AddYears(-i).Year.ToString());
            }
            return years.ToArray();
        }

        public static List<TimeZone> GetAllDefaultTimeZones(string language = "en-US")
        {
            return GetAllTimeZones(language);
        }

        public static string[] GetAllDefaultTimeZonesArray(string language = "en-US")
        {
            return GetAllTimeZones(language).Select(x => x.Code).ToArray();
        }

        public static Currency GetCurrencyInfo(string code)
        {
            var currencies = GetAllCurrenciesCodeInfo();
            return currencies.SingleOrDefault(x => x.Code.Equals(code));
        }

        public static Dictionary<string, string> AddNonISOCurrencySymbols(this Dictionary<string, string> symbols)
        {
            symbols.Add(BITCOIN_CODE, BITCOIN_SYMBOL);
            symbols.Add(TOMAN_CODE, TOMAN_SYMBOL);
            return symbols;
        }

        public static string CurrencyFormat(string code)
        {
            // TODO: Add currency format to Dictionaries
            switch (code)
            {
                case BITCOIN_CODE:
                    return "#0.00000";
                case RIYAL_CODE:
                    return "###,###,###,###";
                case TOMAN_CODE:
                    return "###,###,###,###";
                case USDOLLAR_CODE:
                    return "#,##0.00";
                default:
                    return "#.##0,00";
            }
        }

        public static bool HasDecimals(string code)
        {
            switch (code)
            {
                case RIYAL_CODE:
                    return false;
                case TOMAN_CODE:
                    return false;
                default:
                    return true;
            }
        }

        public static string DecimalAccuracy(decimal money, string code)
        {
            int intPart = (int)money;
            switch (code)
            {
                case BITCOIN_CODE:
                    return money.ToString("N5");
                case RIYAL_CODE:
                    return intPart.ToString("N0");
                case TOMAN_CODE:
                    return intPart.ToString("N0");
                default:
                    return intPart.ToString("N2");
            }
        }
        public static Language GetLanguageByCode(string code)
        {
            //var countries = GetAllLanguages();
            //var language = countries.FirstOrDefault(x => x.Code.Equals(code));
            //return language;
            var language = GetLanguage(code);
            if (language.Name == "Persian") { language.Name = "Farsi"; }
            return language;
        }

        public static string GetGenderDisplayName(string code)
        {
            switch (code)
            {
                case "M": return "[Genders]Male";
                case "F": return "[Genders]Female";
                default: return "[Genders]Unknown";
            }
        }

        public static string GetGenderCode(string displayName)
        {
            switch (displayName)
            {
                case "[Genders]Male": return "M";
                case "[Genders]Female": return "F";
                case "[Genders]Unknown":
                default: return string.Empty;
            }
        }

        public static string[] GetPhoneCountryCodeByCountries(object[] countries)
        {
            string[] countriesString = new string[countries.Length];
            for (int i = 0; i < countries.Length; i++)
            {
                countriesString[i] = countries[i].ToString();
            }
            return GetDefaultPhoneCountryCodes(countriesString);
        }

        public static List<SimpleCountry> GetTenantCountries(string[] countries)
        {
            var countryList = new List<SimpleCountry>();
            var allCountries = ISO3166.GetCollection();
            try
            {
                foreach (var country in countries)
                {
                    var c = allCountries.SingleOrDefault(x => x.Alpha3.Equals(country));
                    if (c != null)
                        countryList.Add(new SimpleCountry
                        {
                            Name = c.Name,
                            Code = c.Alpha3
                        });
                }
            }
            catch
            {
                // if exception is thrown it means that the culture is neutral or does not have a region 
            }
            return countryList;
        }

        public static List<TimeZone> GetTenantTimeZones(string[] zones, string language = "en-US")
        {
            var timeZones = GetAllTimeZones(language);
            var tenantZones = new List<TimeZone>();
            foreach (var zone in zones)
            {
                tenantZones.AddRange(GetTimeZonesByCode(zone));
            }
            return tenantZones;
        }

        public static IEnumerable<TimeZone> GetTimeZonesByCode(string zone, string language = "en-US")
        {
            var zones = GetAllTimeZones(language);
            return zones.Where(x => x.Code.Equals(zone) && !x.IsDaylightSavings).Select(x => x);
        }

        public static TimeZone GetTimeZoneByCode(string zone, string language = "en-US")
        {
            var zones = GetAllTimeZones(language);
            //return zones.SingleOrDefault(x => x.Code.Equals(zone) && !x.IsDaylightSavings);
            return zones.FirstOrDefault(x => x.Code.Equals(zone));
        }

        public static string GetOddsDisplayName(int code)
        {
            switch (code)
            {
                case 1: return "Fractional";
                case 2: return "Decimal";
                case 3: return "American";
                case 4: return "American Fractional";
                case 5: return "None";
                default: return "Unknown";
            }
        }

        #endregion

        #region Private Methods
        private static Currency[] GetAllCurrenciesCodeInfo()
        {
            var countries = ISO3166.GetCollection();
            var codes = new List<Currency>();
            foreach (var country in countries)
            {
                try
                {
                    var region = new RegionInfo(country.Alpha2);
                    if (codes.SingleOrDefault(x => x.Code.Equals(region.ISOCurrencySymbol)) == null)
                    {
                        codes.Add(new Currency
                        {
                            Code = region.ISOCurrencySymbol,
                            //Name = $"{region.CurrencySymbol} {region.CurrencyNativeName}"
                            Name = $"{region.CurrencySymbol} {region.CurrencyEnglishName}"
                        });
                    }
                }
                catch
                {
                    // if exception is thrown it means that the culture is neutral or does not have a region 
                }
            }
            // Add non ISO currencies
            codes.AddNonISOCurrencies();
            return codes.ToArray();
        }

        private static string[] GetAllCurrencyCodes()
        {
            var countries = ISO3166.GetCollection();
            var codes = new List<string>();
            foreach (var country in countries)
            {
                try
                {
                    codes.Add(new RegionInfo(country.Alpha2).ISOCurrencySymbol);
                }
                catch
                {
                    // if exception is thrown it means that the culture is neutral or does not have a region 
                }
            }

            return codes.ToArray();
        }

        private static List<Currency> AddNonISOCurrencies(this List<Currency> codes)
        {
            // Manually add non ISO currencies
            codes.Add(new Currency { Name = $"{BITCOIN_SYMBOL} {BITCOIN}", Code = BITCOIN_CODE }); // bitcoin
            codes.Add(new Currency { Name = $"{TOMAN_SYMBOL} {TOMAN}", Code = TOMAN_CODE }); // TOMAN = 0.01 of IRR
            return codes;
        }

        private static string[] GetAllLanguageCodes()
        {
            var codes = new List<string>();
            foreach (var country in (Country[])Enum.GetValues(typeof(Country)))
            {
                codes.Add(country.GetCountryLanguageIsoCode());
            }
            return codes.ToArray();
        }

        private static List<Language> GetAllLanguages()
        {
            var codes = new List<Language>();
            foreach (var country in (Country[])Enum.GetValues(typeof(Country)))
            {
                codes.Add(new Language
                {
                    Name = country.GetCountryLanguageName(),
                    Code = country.GetCountryLanguageIsoCode()
                });
            }
            return codes;
        }

        private static CultureInfo[] GetAllCultures()
        {
            return CultureInfo.GetCultures(CultureTypes.AllCultures);
        }

        private static Language GetLanguage(string code)
        {
            var cultureInfo = new CultureInfo(code);
            return new Language
            {
                Name = cultureInfo.DisplayName,
                Code = code
            };
        }

        private static List<TimeZone> GetAllTimeZones(string language = "en-US")
        {
            var allZones = new List<TimeZone>();
            const string pattern = @"UTC[±]{0,1}";
            foreach (var timeZone in TimeZoneHelper.AllTimeZones)
            {
                var zone = Regex.Replace(timeZone.UtcOffsetDisplay, pattern, "").ToString().Split(':');
                var hour = int.Parse(zone[0]);
                var minute = zone.Length > 1 ? int.Parse(zone[1]) : 0;
                var gmt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                var thisTimeZone = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Math.Abs(hour), minute, 0);
                var span = hour > 0 ? thisTimeZone.Subtract(gmt) : gmt.Subtract(thisTimeZone);
                var totalMinutes = span.TotalMinutes;

                allZones.Add(new TimeZone
                {
                    Code = timeZone.Abbreviation,
                    DisplayName = timeZone.Name,
                    Name = timeZone.Name,
                    SimpleDisplayName = $"{timeZone.Abbreviation}({timeZone.UtcOffsetDisplay})",
                    UtcOffsetMinutes = totalMinutes,
                    UtcOffsetDisplay = timeZone.UtcOffsetDisplay
                });
            }
            return allZones;
            //var timeZones = GetAllTimeZonesInfo();
            //var allZones = new List<TimeZone>();
            //foreach (var timezone in timeZones)
            //{
            //    var intervals = GetTimeZoneIntervals(timezone.Id);

            //    TimeZoneValues abbreviation = TZNames.GetAbbreviationsForTimeZone(timezone.Id, language);
            //    string name = string.Empty, code = string.Empty, utcDisplay = string.Empty, plusPrefix = string.Empty;
            //    bool isDst = false;

            //    foreach (var interval in intervals)
            //    {
            //        bool codeIsNumber = int.TryParse(interval.Name, out int isNumber);
            //        code = codeIsNumber ? abbreviation.Standard : interval.Name;
            //        plusPrefix = timezone.BaseUtcOffset.Hours > 0 ? "+" : "";
            //        var baseUtcOffsetTxt = timezone.BaseUtcOffset.ToString();
            //        utcDisplay = $"UTC{plusPrefix}{baseUtcOffsetTxt.Substring(0, baseUtcOffsetTxt.Length - 3)}";

            //        if (interval.Name.Equals(abbreviation.Standard))
            //        {
            //            name = timezone.StandardName;
            //            code = abbreviation.Standard;
            //            isDst = false;
            //        }
            //        else if (interval.Name.Equals(abbreviation.Daylight))
            //        {
            //            name = timezone.DaylightName;
            //            code = abbreviation.Daylight;
            //            isDst = true;
            //        }
            //        else if (interval.Name.Equals(abbreviation.Generic))
            //        {
            //            name = timezone.DisplayName;
            //            code = abbreviation.Generic;
            //            isDst = false;
            //        }
            //        else
            //        {
            //            name = timezone.DisplayName;
            //            code = interval.Name;
            //            isDst = false;
            //        }
            //        const string pattern = @"\(UTC[-+][0-9]{1,2}:[0-9]{2}\)\s";
            //        name = Regex.Replace(name, pattern, "");
            //        var codeArray = code.Split(' ');
            //        TimeZoneData timeZoneData = null;
            //        if (codeArray.Length > 1)
            //        {
            //            timeZoneData = TimeZoneHelper.AllTimeZones.SingleOrDefault(x => x.Name.Equals(timezone.Id));
            //            if (timeZoneData == null)
            //            {
            //                var attempt = TimeZoneHelper.AllTimeZones.Where(x => x.Name.Contains(codeArray[0]));
            //                timeZoneData = attempt.Count() > 0 ? attempt.ToList()[0] : null;
            //            }
            //            if (timeZoneData != null)
            //            {
            //                code = timeZoneData.Abbreviation;
            //                utcDisplay = timeZoneData.UtcOffsetDisplay;
            //            }
            //        }

            //        var newTimeZone = new TimeZone
            //        {
            //            Code = code,
            //            DisplayName = name + $" ({code})" + (timezone.BaseUtcOffset.Hours != 0 ? $" [{utcDisplay}]" : ""),
            //            Name = name,
            //            SimpleDisplayName = code + (timezone.BaseUtcOffset.Hours != 0 ? $" [{utcDisplay}]" : ""),
            //            UtcOffset = timezone.BaseUtcOffset.TotalMinutes,
            //            UtcOffsetDisplay = utcDisplay,
            //            IsDaylightSavings = isDst
            //        };

            //        if (allZones.SingleOrDefault(x => x.Name.Equals(newTimeZone.Name)) == null)
            //            allZones.Add(newTimeZone);

            //        if (code.Equals("UTC"))
            //        {
            //            timeZoneData = TimeZoneHelper.AllTimeZones.SingleOrDefault(x => x.Abbreviation.Equals("GMT"));
            //            if (timeZoneData != null)
            //            {
            //                if (allZones.SingleOrDefault(x => x.Name.Equals(timeZoneData.Name)) == null)
            //                    allZones.Add(new TimeZone
            //                    {
            //                        Code = timeZoneData.Abbreviation,
            //                        DisplayName = timeZoneData.Name + $" ({timeZoneData.Abbreviation})" + (timezone.BaseUtcOffset.Hours != 0 ? $" [{utcDisplay}]" : ""),
            //                        Name = timeZoneData.Name,
            //                        SimpleDisplayName = timeZoneData.Abbreviation + (timezone.BaseUtcOffset.Hours != 0 ? $" [{utcDisplay}]" : ""),
            //                        UtcOffset = timezone.BaseUtcOffset.TotalMinutes,
            //                        UtcOffsetDisplay = utcDisplay,
            //                        IsDaylightSavings = isDst
            //                    });
            //            }
            //        }
            //    }
            //}
            //return allZones;
        }

        private static List<TimeZoneValues> GetAllTimeZonesAbbreviations(string language = "en-US")
        {
            var timeZone = GetAllTimeZonesInfo();
            var list = new List<TimeZoneValues>();
            foreach (var zone in timeZone)
            {
                list.Add(TZNames.GetAbbreviationsForTimeZone(zone.Id, language));
            }
            return list.Distinct().OrderBy(x => x.Standard).ToList();
        }

        private static string[] GetAllTimeZonesAbbreviationsArray(string language = "en-US")
        {
            var timeZone = GetAllTimeZonesInfo();
            var list = new List<string>();
            foreach (var zone in timeZone)
            {
                list.Add(TZNames.GetAbbreviationsForTimeZone(zone.Id, language).Standard);
            }
            return list.Distinct().OrderBy(x => x).ToArray();
        }

        private static ReadOnlyCollection<TimeZoneInfo> GetAllTimeZonesInfo()
        {
            return TimeZoneInfo.GetSystemTimeZones();
        }

        private static List<TimeZoneValues> GetAllTimeZonesValues(string language = "en-US")
        {
            var timeZone = GetAllTimeZonesInfo();
            var list = new List<TimeZoneValues>();
            foreach (var zone in timeZone)
            {
                list.Add(TZNames.GetNamesForTimeZone(zone.Id, language));
            }
            return list;
        }

        private static string[] GetDefaultPhoneCountryCodes(string[] countries = null)
        {
            var list = ISO3166.GetCollection();
            List<string> unorderedCodes = new List<string>();

            List<ISO3166Country> filteredList = new List<ISO3166Country>();

            if (countries != null)
            {
                foreach (var c in countries)
                {
                    var countryInList = list.Where(x => x.Alpha3.Equals(c));
                    if (countryInList != null)
                        filteredList.AddRange(countryInList);
                }
            }
            else
            {
                filteredList = list.ToList();
            }

            foreach (var item in filteredList)
            {
                if (item.DialCodes != null)
                {
                    foreach (var c in item.DialCodes)
                    {
                        if (!unorderedCodes.Exists(x => x.Equals(c)))
                            unorderedCodes.Add(c);
                    }
                }
            }
            var orderedCodes = unorderedCodes.OrderBy(x => x);
            var codes = new List<string>();
            foreach (var code in orderedCodes)
            {
                codes.Add($"+{code}");
            }
            return codes.ToArray();
        }

        private static List<SimpleCountry> GetSimpleCountries()
        {
            var list = ISO3166.GetCollection();
            return list.Select(x => new SimpleCountry { Name = x.Name, Code = x.Alpha3 }).Distinct().OrderBy(x => x).ToList();
        }

        private static string[] GetThreeDigitCountryCodes()
        {
            var list = ISO3166.GetCollection();
            return list.Select(x => x.Alpha3).Distinct().OrderBy(x => x).ToArray();
        }

        private static IEnumerable<ZoneInterval> GetTimeZoneIntervals(string timezoneId, DateTime? dateTime = null)
        {
            var dateTimeZone = DateTimeZoneProviders.Tzdb.GetZoneOrNull(timezoneId);

            if (dateTimeZone == null)
            {
                var tz = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);
                var source = TzdbDateTimeZoneSource.Default;
                // If there's no such mapping, result will be null.
                source.WindowsMapping.PrimaryMapping.TryGetValue(tz.Id, out string result);

                if (String.IsNullOrEmpty(result)) return new List<ZoneInterval>();

                dateTimeZone = DateTimeZoneProviders.Tzdb[result];

                var yearStart = Instant.FromDateTimeUtc(DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Utc));
                var yearEnd = Instant.FromDateTimeUtc(DateTime.SpecifyKind(DateTime.MinValue.AddYears(1), DateTimeKind.Utc));
                var zoneIntervals = dateTimeZone.GetZoneIntervals(yearStart, yearEnd);

                return zoneIntervals;
            }

            var dateTimeUtc = new DateTime((dateTime ?? DateTime.UtcNow).Ticks, DateTimeKind.Utc);
            var zoneInterval = dateTimeZone.GetZoneInterval(Instant.FromDateTimeUtc(dateTimeUtc));

            return new List<ZoneInterval> { zoneInterval };
        }

        private static IEnumerable<ZoneInterval> GetTimeZoneIntervals(string timeZoneId)
        {
            return DateTimeZoneProviders.Bcl.GetZoneOrNull(timeZoneId)
                .GetZoneIntervals(
                new Interval(Instant.FromDateTimeUtc(DateTime.UtcNow.AddYears(-1)), Instant.FromDateTimeUtc(DateTime.UtcNow.AddYears(1))),
                ZoneEqualityComparer.Options.MatchAllTransitions)
                .ToList();
        }

        private static string[] TimeZonesToArray(this List<TimeZoneValues> list, string language = "en-US")
        {
            List<string> timeZones = new List<string>();
            foreach (var zone in list)
            {
                timeZones.Add(zone.Standard);
            }
            return timeZones.ToArray();
        }
        #endregion

        #region Default Values
        public static List<SimpleCountry> DefaultCountries => GetSimpleCountries();
        public static Currency[] DefaultCurrencies => GetAllCurrenciesCodeInfo();
        public static Currency[] DefaultCurrencyInfo => GetAllCurrenciesCodeInfo();
        public static string[] DefaultGenders => new string[] { "[Genders]Male", "[Genders]Female" };
        public static string[] DefaultGendersCode => new string[] { "M", "F" };
        public static string DefaultLanguage => "en";
        public static List<Language> DefaultLanguageCodes => GetAllLanguages();
        public static string[] DefaultOddsDisplay => new string[] { "Decimal", "Fractional", "American", "American Fractional", "None" };
        public static string[] DefaultPhoneCountryCodes => GetDefaultPhoneCountryCodes();
        public static List<TimeZone> DefaultTimeZones => GetAllDefaultTimeZones();
        public static string[] DefaultTitles => new string[] { "Mr", "Mrs", "Ms", "Miss" };
        public static string[] DefaultYears => GenerateDefaultYears();
        #endregion
    }
}
