namespace TotalCode.Core.Pages.Controllers
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using TotalCode.Core.Models;
    using TotalCode.Core.Models.NestedContents;
    using TotalCode.Core.Models.Pages;
    using TotalCode.Core.Utilities;
    using Umbraco.Core.IO;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Plugins.Connector;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Models;
    using Umbraco.Plugins.Connector.Services;
    using Umbraco.Web;
    using Umbraco.Web.Models;
    using Umbraco.Web.Mvc;

    public class BasePageController : RenderMvcController
    {
        public T GetModel<T>(IPublishedContent current)
            where T : BasePageViewModel
        {
            var model = Activator.CreateInstance(typeof(T), current);
            var baseModel = model as BasePageViewModel;

            var homePage = current.Root();

            var depositPage = homePage.Children.SingleOrDefault(x => x.ContentType.Alias == "totalCodeDepositPage");
            baseModel.DepositPageUrl = depositPage?.Url;
            baseModel.DepositPageUrlAbsolute = depositPage?.UrlAbsolute();

            baseModel.RootGuid = homePage.Key;
            baseModel.RootUrl = homePage.Url;

            var service = Services.DomainService;

            var currentDomainInUse = Request.Url.Host;

            baseModel.DefaultLanguage = homePage.HasProperty("defaultLanguage") ? homePage.Value<string>("defaultLanguage") : DefaultAllowedValues.DefaultLanguage;


            var secureUrl = bool.Parse(TenantGenerationOptions.SecureUrls) ? "https" : "http";

            var allDomains = service.GetAssignedDomains(homePage.Id, false);
            var domainsWithExistingLanguages = allDomains.Where(x => Services.LocalizationService.GetLanguageByIsoCode(x.LanguageIsoCode) != null && (x.DomainName.ToLower().Contains($"{secureUrl}://{currentDomainInUse}") || !x.DomainName.Contains("http")));
            baseModel.Languages = domainsWithExistingLanguages.Select(domain => new LanguageViewModel(baseModel.Content, domain, baseModel.Content.Id == homePage.Id, bool.Parse(TenantGenerationOptions.SecureUrls), baseModel.DefaultLanguage));
            baseModel.TenantUid = homePage.Value<string>("tenantUid");

            //var currentCulture = Umbraco.CultureDictionary.Culture.Name;
            //var currentCulture = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            var currentCulture = Umbraco.CultureDictionary.Culture.TwoLetterISOLanguageName;

            if (domainsWithExistingLanguages.Any())
            {
                var currentDomain = domainsWithExistingLanguages.FirstOrDefault(domain => domain.LanguageIsoCode == currentCulture);
                currentDomain = currentDomain ?? allDomains.First();

                baseModel.CurrentLanguage = new LanguageViewModel(baseModel.Content, currentDomain);
            }
            else
            {
                baseModel.CurrentLanguage = new LanguageViewModel(baseModel.Content, currentCulture);
            }

            string origin = Request.Url.ToString();
#if DEBUG
            origin = $"{baseModel.CurrentLanguage.Url}";
#endif
            baseModel.Origin = origin;
            CheckSession(origin, baseModel.TenantUid);

            baseModel.Domain = homePage.Value<string>("domain").ToLower();
            baseModel.Subdomain = homePage.Value<string>("subDomain").ToLower();
            baseModel.ServiceUrl = $"sportsbook-api-{baseModel.Subdomain}.{baseModel.Domain}";


            var preferences = homePage.Value<string>("tenantPreferencesProperty");

            baseModel.FaviconIco = homePage.HasValue("tenantFavicon16x16") ? homePage.Value<string>("tenantFavicon16x16") : string.Empty;
            baseModel.Favicon32x32 = homePage.HasValue("tenantFavicon32x32") ? homePage.Value<string>("tenantFavicon32x32") : string.Empty;
            baseModel.Favicon72x72 = homePage.HasValue("tenantFavicon72x72") ? homePage.Value<string>("tenantFavicon72x72") : string.Empty;
            baseModel.Favicon144x144 = homePage.HasValue("tenantFavicon144x144") ? homePage.Value<string>("tenantFavicon144x144") : string.Empty;
            baseModel.Favicon192x192 = homePage.HasValue("tenantFavicon192x192") ? homePage.Value<string>("tenantFavicon192x192") : string.Empty;
            baseModel.Favicon256x256 = homePage.HasValue("tenantFavicon256x256") ? homePage.Value<string>("tenantFavicon256x256") : string.Empty;

            baseModel.Preferences = JsonConvert.DeserializeObject<TenantPreferences>(preferences);

            baseModel.TermsAndConditionsText = homePage.HasValue("termsAndConditionsMessage") ? homePage.Value<string>("termsAndConditionsMessage") : Umbraco.GetDictionaryValue("[Register]TermsAndConditions", "terms and conditions");

            baseModel.TermsAndConditionsPageUrl = homePage.DescendantOfType("totalCodeTermsPage") != null ? homePage.DescendantOfType("totalCodeTermsPage").Url : "#";

            baseModel.CookiesPolicyPageUrl = homePage.DescendantOfType("cookiesPolicy") != null ? homePage.DescendantOfType("cookiesPolicy").Url : "#";

            baseModel.PrivacyPolicyPageUrl = homePage.DescendantOfType("privacyPolicy") != null ? homePage.DescendantOfType("privacyPolicy").Url : "#";

            baseModel.Currencies = homePage.HasProperty("tenantCurrencies") ? homePage.Value<string>("tenantCurrencies").Split(',').Select(x => DefaultAllowedValues.GetCurrencyInfo(x)).ToList() : DefaultAllowedValues.DefaultCurrencies.ToList();

            baseModel.Titles = baseModel.Preferences.Title.AllowedValues == null || baseModel.Preferences.Title.AllowedValues.Length == 0 ? DefaultAllowedValues.DefaultTitles : baseModel.Preferences.Title.AllowedValues.Select(x => x.ToString()).ToArray();

            baseModel.Countries = baseModel.Preferences.Country.AllowedValues == null || baseModel.Preferences.Country.AllowedValues.Length == 0 ? DefaultAllowedValues.DefaultCountries : DefaultAllowedValues.GetTenantCountries(baseModel.Preferences.Country.AllowedValues.Where(x => x != null).Select(x => x.ToString()).ToArray());

            baseModel.PhoneCountryCodes = baseModel.Preferences.Mobile.AllowedValues == null || baseModel.Preferences.Mobile.AllowedValues.Length == 0 || baseModel.Preferences.Country.AllowedValues.Length == 0 ? DefaultAllowedValues.GetPhoneCountryCodeByCountries(baseModel.Preferences.Country.AllowedValues) : baseModel.Preferences.Mobile.AllowedValues.Select(x => x.ToString()).ToArray();

            baseModel.Genders = baseModel.Preferences.Gender.AllowedValues == null || baseModel.Preferences.Gender.AllowedValues.Length == 0 ? DefaultAllowedValues.DefaultGendersCode : baseModel.Preferences.Gender.AllowedValues.Select(x => x.ToString()).ToArray();

            baseModel.Years = baseModel.Preferences.DateOfBirth.AllowedValues == null || baseModel.Preferences.DateOfBirth.AllowedValues.Length == 0 ? DefaultAllowedValues.GenerateDefaultYears() : baseModel.Preferences.DateOfBirth.AllowedValues.Select(x => x.ToString()).ToArray();

            baseModel.Odds = baseModel.Preferences.Preferences.OddsDisplay.AllowedValues == null || baseModel.Preferences.Preferences.OddsDisplay.AllowedValues.Length == 0 ? DefaultAllowedValues.DefaultOddsDisplay : baseModel.Preferences.Preferences.OddsDisplay.AllowedValues.Select(x => x.ToString()).ToArray();

            baseModel.TimeZones = baseModel.Preferences.Preferences.TimeZone.AllowedValues != null && baseModel.Preferences.Preferences.TimeZone.AllowedValues.Length > 0 ? DefaultAllowedValues.GetTenantTimeZones(baseModel.Preferences.Preferences.TimeZone.AllowedValues.Where(x => x != null).Select(x => x.ToString()).ToArray()) : DefaultAllowedValues.DefaultTimeZones;

            baseModel.Title = current.HasValue("pageTitle") ? current.Value<string>("pageTitle") : current.Name;

            #region SEO (Meta Tags)
            baseModel.MetaAuthor = homePage.HasValue("metaAuthor") ? homePage.Value<string>("metaAuthor") : string.Empty;
            baseModel.MetaCopyright = homePage.HasValue("metaCopyright") ? homePage.Value<string>("metaCopyright") : string.Empty;
            baseModel.MetaDescription = homePage.HasValue("metaDescription") ? homePage.Value<string>("metaDescription") : string.Empty;
            baseModel.MetaKeywords = homePage.HasValue("metaKeywords") ? homePage.Value<string>("metaKeywords") : string.Empty;
            baseModel.MetaRobots = homePage.HasValue("metaRobots") ? homePage.Value<string>("metaRobots") : "index,follow";
            #endregion

            var logo = homePage.Value<IPublishedContent>("logo");
            baseModel.Logo = logo;

            baseModel.LogoContent = string.Empty;
            if (logo != null && Path.GetExtension(logo.Url).Equals(".svg"))
            {
                var logoAbsolutePath = IOHelper.MapPath(logo.Url);
                if (System.IO.File.Exists(logoAbsolutePath))
                {
                    baseModel.LogoContent = System.IO.File.ReadAllText(logoAbsolutePath);
                    baseModel.LogoIsSvg = true;
                }
            }
            else if (logo != null)
            {
                baseModel.LogoContent = logo.Url;
            }

            baseModel.MainMenu = CurrentPage.HasProperty("mainMenu") && CurrentPage.HasValue("mainMenu") ? baseModel.MainMenu = CurrentPage.GetValueAsViewModels<PageMenuViewModel>("mainMenu") : homePage.GetValueAsViewModels<PageMenuViewModel>("mainMenu", baseModel.DefaultLanguage);
            baseModel.TopMenu = homePage.GetValueAsViewModels<PageMenuViewModel>("topMenu", baseModel.DefaultLanguage);

            baseModel.MobileMenu = current.Children.Select(child => new PageMenuViewModel(child));
            baseModel.AccountMenu = homePage.GetValueAsViewModels<PageMenuViewModel>("accountMenu", baseModel.DefaultLanguage);

            baseModel.SpinnerImage = homePage.Value<IPublishedContent>("spinnerImage");

            baseModel.TopMenuExternalLinks = homePage.HasValue("externalUrlsTopMenu") ? homePage.Value<IEnumerable<Link>>("externalUrlsTopMenu") : Enumerable.Empty<Link>();
            baseModel.MainMenuExternalLinks = homePage.HasValue("externalUrlsMainMenu") ? homePage.Value<IEnumerable<Link>>("externalUrlsMainMenu") : Enumerable.Empty<Link>();
            baseModel.AccountMenuExternalLinks = homePage.HasValue("externalUrlsAccountMenu") ? homePage.Value<IEnumerable<Link>>("externalUrlsAccountMenu") : Enumerable.Empty<Link>();
            baseModel.FooterExternalLinks = homePage.HasValue("externalUrlsFooter") ? homePage.Value<IEnumerable<Link>>("externalUrlsFooter") : Enumerable.Empty<Link>();

            baseModel.FooterLogo = homePage.Value<IPublishedContent>("footerLogo");
            baseModel.FooterText = homePage.HasValue("footerText") ? homePage.Value<string>("footerText").Replace("[year]", DateTime.Now.Year.ToString()) : string.Empty;

            baseModel.GroupLinksA = homePage.GetValueAsViewModel<FooterLinkGroupViewModel>("groupLinksA");
            baseModel.GroupLinksB = homePage.GetValueAsViewModel<FooterLinkGroupViewModel>("groupLinksB");
            baseModel.GroupLinksC = homePage.GetValueAsViewModel<FooterLinkGroupViewModel>("groupLinksC");

            baseModel.Extras = homePage.HasValue("extras") ? homePage.Value<IEnumerable<Link>>("extras") : new List<Link>().AsEnumerable();

            baseModel.FacebookLink = homePage.Value<string>("facebookLink");
            baseModel.InstagramLink = homePage.Value<string>("instagramLink");
            baseModel.TwitterLink = homePage.Value<string>("twitterLink");
            baseModel.TelegramUsername = homePage.Value<string>("telegramUsername");
            baseModel.TelegramHelpdesk = homePage.Value<string>("helpdeskTelegramAccount");
            baseModel.WhatsAppNumber = homePage.Value<string>("whatsAppNumber");
            baseModel.CompanyLogos = homePage.GetValueAsViewModels<CompanyLogoViewModel, IPublishedElement>("companyLogos");

            baseModel.WebsiteName = homePage.Value<string>("websiteName");
            baseModel.BrandName = homePage.Value<string>("brandName");
            baseModel.PageId = homePage.Id;
            baseModel.CurrentPageId = current.Id;

            baseModel.AccountLandingPageUrl = homePage.HasValue("accountLandingPage") ? homePage.Value<IEnumerable<IPublishedContent>>("accountLandingPage").FirstOrDefault().Url : homePage.Url;
            baseModel.BackUrl = current.Parent?.Url;

            var contactUsPage = homePage.Children(x => x.ContentType.Alias == "totalCodeTicketsPage").FirstOrDefault()?.Children(x => x.ContentType.Alias == "totalCodeTicketPage").FirstOrDefault();
            baseModel.ContactUsUrl = contactUsPage?.Parent.Url;
            baseModel.ViewTicketUrl = contactUsPage?.Url;

            baseModel.HelpUrl = homePage.FirstChildOfType("totalCodeCategoriesPage")?.Url;
            baseModel.CategoriesUrl = homePage.FirstChildOfType("totalCodeCategoriesPage")?.Url;
            baseModel.SettingsUrl = homePage.FirstChildOfType("totalCodeSettingsPage")?.Url;

            var withdrawPage = homePage.Children(x => x.ContentType.Alias == "totalCodeWithdrawPage").FirstOrDefault();
            baseModel.WithdrawUrl = withdrawPage?.Url;

            baseModel.IsLoggedIn = LoginSession.LoggedIn;
            baseModel.IsMobileBrowser = LoginSession.IsMobileBrowser.Value;

            if(current.ContentType.Alias == "totalCodeCategoryPage" || current.ContentType.Alias == "totalCodeArticlePage")
            {
                baseModel.BreadCrumbs = current.Ancestors().Where(x => x.Level > 1).OrderBy(o => o.Level);
            }

            if (baseModel.IsLoggedIn)
            {
                baseModel.LoggedInUsername = LoginSession.Username;
                baseModel.CustomerSummary = LoginSession.CustomerSummary;
                baseModel.CurrencySymbol = CurrencyCodeMapper.GetSymbol(baseModel.CustomerSummary.CurrencyCode);
                baseModel.CurrencyFormat = DefaultAllowedValues.CurrencyFormat(baseModel.CustomerSummary.CurrencyCode);
                baseModel.CurrencyHasDecimals = DefaultAllowedValues.HasDecimals(baseModel.CustomerSummary.CurrencyCode);
                baseModel.Token = LoginSession.Token;
            }

            baseModel.PageContent = current.HasProperty("pageContent") && current.HasValue("pageContent") ? current.Value<string>("pageContent", baseModel.CurrentLanguage.TwoLetterISOLanguageName) : string.Empty;

            baseModel.Theme = homePage.HasProperty("theme") && homePage.HasValue("theme") ? homePage.Value<string>("theme") : string.Empty;
            //Balance Api Url
            baseModel.BalanceApiUrl = ApiUrls.FinancialManagementUrl;

            baseModel.notificationBgColor = homePage.Value<string>("notificationBgColor");
            //baseModel.notificationPosition = homePage.Value<string>("notificationPosition");

            switch (homePage.Value<string>("notificationPosition"))
            {
                case "Top Right":
                    baseModel.notificationPosition = "toast-top-right";
                    break;
                case "Bottom Right":
                    baseModel.notificationPosition = "toast-bottom-right";
                    break;
                case "Bottom Left":
                    baseModel.notificationPosition = "toast-bottom-left";
                    break;
                case "Top Left":
                    baseModel.notificationPosition = "toast-top-left";
                    break;
                case "Top Center":
                    baseModel.notificationPosition = "toast-top-center";
                    break;
                case "Bottom Center":
                    baseModel.notificationPosition = "toast-bottom-center";
                    break;
                default:
                    baseModel.notificationPosition = "toast-bottom-right";
                    break;

            }

            var isNumber = int.TryParse(homePage.Value<string>("notificationWidth"), out int notificationWidth);

            if (isNumber)
                baseModel.notificationWidth = notificationWidth.ToString();

            TempData["FormSpinnerImage"] = homePage.HasValue("spinnerImage", currentCulture) ? (!string.IsNullOrEmpty(homePage.Value<IPublishedContent>("spinnerImage", currentCulture).GetCropUrl("Form Spinner")) ? homePage.Value<IPublishedContent>("spinnerImage", currentCulture).GetCropUrl("Form Spinner") : homePage.Value<IPublishedContent>("spinnerImage", currentCulture).GetCropUrl(32, 32)) : string.Empty;

            HttpContext.Response.Cookies["IsAuthenticatedPage"].Expires = DateTime.Now.AddDays(-1);

            return (T)model;
        }

        void CheckSession(string origin, string tenantUid)
        {
            HttpCookie usernameCookie = Request.Cookies["username"];
            HttpCookie tokenCookie = Request.Cookies["token"];
            HttpCookie lastLoginCookie = Request.Cookies["lastLogin"];

            if (usernameCookie != null)
            {
                LoginSession.LoggedIn = true;
                LoginSession.Username = usernameCookie.Value;
                string sa = @"""" + lastLoginCookie?.Value + @"""";
                LoginSession.LastLogin = lastLoginCookie != null ? JsonConvert.DeserializeObject<DateTime>(sa) : DateTime.UtcNow;

                if (tokenCookie != null)
                {
                    LoginSession.Token = tokenCookie.Value;
                    LoginSession.Username = LoginSession.DecodedJwtToken.Username;
                }

                //LoginSession.Token = Request.Cookies["token"] != null ? Request.Cookies["token"].Value : string.Empty;
            }
            else
            {
                LoginSession.Logout();
            }

            var api = new TotalCodeApiService();

            if (LoginSession.LastLogin > DateTime.MinValue && LoginSession.LastLogin < DateTime.UtcNow)
            {
                LoginSession.LoggedIn = true;
            }
            else if (LoginSession.LastLogin == DateTime.UtcNow)
            {
                var refresh = (LoginResponseContent)api.RefreshToken(tenantUid, origin, LoginSession.Token);
                LoginSession.LoggedIn = true;
                LoginSession.Token = refresh.Token;
                LoginSession.LastLogin = refresh.LastLogin.Value;
            }
            else if (LoginSession.LastLogin > DateTime.UtcNow)
            {
                LoginSession.Logout();
            }

            if (LoginSession.LoggedIn)
            {
                //if (LoginSession.CustomerSummary == null)
                //{
                LoginSession.CustomerSummary = api.GetCustomerSummary(tenantUid, origin, LoginSession.DecodedJwtToken);
                if (!LoginSession.CustomerSummary.Balance.IsLiveBalance)
                {
                    LoginSession.Logout();
                }
                //}
            }

            if (!LoginSession.IsMobileBrowser.HasValue)
                LoginSession.IsMobileBrowser = ContentHelper.BrowserIsMobile();
        }
    }
}

