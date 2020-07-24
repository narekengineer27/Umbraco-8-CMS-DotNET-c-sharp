namespace TotalCode.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Web;
    public class LanguageViewModel
    {
        public LanguageViewModel() { }
        public LanguageViewModel(IPublishedContent page, IDomain domain, bool isHome, bool SecureUrl, string defaultLanguage)
        {
            //Url = domain.DomainName;
            // TwoLetterISOLanguageName = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            string secureUrl = SecureUrl ? "https://" : "http://";
            bool isLocal = !domain.DomainName.Contains("http");

            if (isLocal)
            {
                Url = isHome ? domain.DomainName : $"{domain.DomainName}/{GetUrlSegmentsIncludingParents(page, domain.LanguageIsoCode)}";
                Url = Url.Replace("//", "/");
            }
            else
            {
                var isDefault = domain.LanguageIsoCode == defaultLanguage ? string.Empty : $"/{domain.LanguageIsoCode}";
                var partialUrl = $"{new Uri(domain.DomainName).Host}{isDefault}/{GetUrlSegmentsIncludingParents(page, domain.LanguageIsoCode)}";
                partialUrl = partialUrl.Replace("//", "/");
                Url = isHome ? domain.DomainName : $"{secureUrl}{partialUrl}";
            }

            
            if (page.HasValue("languageName", domain.LanguageIsoCode))
            {
                EnglishName = page.Value<string>("languageName", domain.LanguageIsoCode);
                Title = page.Value<string>("languageName", domain.LanguageIsoCode);
            }
            else
            {
                var cultureInfo = new CultureInfo(domain.LanguageIsoCode);
                EnglishName = cultureInfo.DisplayName;
                Title = cultureInfo.NativeName;
                TwoLetterISOLanguageName = cultureInfo.TwoLetterISOLanguageName;
                ISOLanguangeName = domain.LanguageIsoCode;
            }
        }

        public string GetUrlSegmentsIncludingParents(IPublishedContent page, string languageIsoCode)
        {
            return string.Join("/", page.AncestorsOrSelf()
                .OrderBy(p => p.Level)
                .Where(p => p.Level > 1)
                .Select(p => p.GetUrlSegment(languageIsoCode)).Where(w => !string.IsNullOrEmpty(w))); ;
        }

        public LanguageViewModel(IPublishedContent homepage, IDomain domain)
        {
            Url = domain.DomainName;
            // TwoLetterISOLanguageName = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            if (homepage.HasValue("languageName", domain.LanguageIsoCode))
            {
                Title = homepage.Value<string>("languageName", domain.LanguageIsoCode);
            }
            else
            {
                var cultureInfo = new CultureInfo(domain.LanguageIsoCode);
                EnglishName = cultureInfo.DisplayName;
                Title = cultureInfo.NativeName;
                TwoLetterISOLanguageName = cultureInfo.TwoLetterISOLanguageName;
                ISOLanguangeName = domain.LanguageIsoCode;
            }
        }

        public LanguageViewModel(IPublishedContent page, string culture)
        {
            if (page.HasValue("languageName", culture))
            {
                Title = page.Value<string>("languageName", culture);
            }
            else
            {
                var cultureInfo = new CultureInfo(culture);
                EnglishName = cultureInfo.DisplayName;
                Title = cultureInfo.NativeName;
                TwoLetterISOLanguageName = cultureInfo.TwoLetterISOLanguageName;
                ISOLanguangeName = culture;
            }

            Url = page.GetUrl(culture);
        }

        public LanguageViewModel(
            IPublishedContent currentPage,
            KeyValuePair<string, PublishedCultureInfo> culture)
        {
            if (currentPage.HasValue("languageName", culture.Key))
            {
                Title = currentPage.Value<string>("languageName", culture.Key);
            }
            else
            {
                var cultureInfo = new CultureInfo(culture.Key);
                EnglishName = cultureInfo.DisplayName;
                Title = cultureInfo.NativeName;
                TwoLetterISOLanguageName = cultureInfo.TwoLetterISOLanguageName;
                ISOLanguangeName = culture.Key;
            }

            Url = currentPage.GetUrl(culture.Key);
        }

        public string Title { get; set; }
        public string Url { get; set; }
        public string TwoLetterISOLanguageName { get; set; }
        public string ISOLanguangeName { get; set; }
        public string EnglishName { get; set; }
    }
}
