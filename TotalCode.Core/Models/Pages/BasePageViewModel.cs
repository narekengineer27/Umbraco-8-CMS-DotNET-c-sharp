namespace TotalCode.Core.Models.Pages
{
    using System;
    using System.Collections.Generic;
    using TotalCode.Core.Models.NestedContents;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Plugins.Connector.Models;
    using Umbraco.Web.Models;
    using TimeZone = Umbraco.Plugins.Connector.Models.TimeZone;

    public class BasePageViewModel : ContentModel
    {
        public BasePageViewModel(IPublishedContent content) : base(content)
        {
        }

        public string Title { get; set; }
        public IPublishedContent Logo { get; set; }
        public string LogoContent { get; set; }
        public bool LogoIsSvg { get; set; }
        public IEnumerable<PageMenuViewModel> TopMenu { get; set; }
        public IEnumerable<PageMenuViewModel> MainMenu { get; set; }
        public IEnumerable<PageMenuViewModel> MobileMenu { get; set; }
        public IEnumerable<Link> Extras { get; set; }
        public IPublishedContent SpinnerImage { get; set; }

        // External Links added Nov 5, 2019
        public IEnumerable<Link> TopMenuExternalLinks { get; set; }
        public IEnumerable<Link> MainMenuExternalLinks { get; set; }
        public IEnumerable<Link> AccountMenuExternalLinks { get; set; }
        public IEnumerable<Link> FooterExternalLinks { get; set; }

        public IPublishedContent FooterLogo { get; set; }
        public string FooterText { get; set; }
        public FooterLinkGroupViewModel GroupLinksA { get; set; }
        public FooterLinkGroupViewModel GroupLinksB { get; set; }
        public FooterLinkGroupViewModel GroupLinksC { get; set; }
        public string FacebookLink { get; set; }
        public string InstagramLink { get; set; }
        public string TwitterLink { get; set; }

        public string TelegramUsername { get; set; }
        public string TelegramHelpdesk { get; set; }
        public string WhatsAppNumber { get; set; }

        public IEnumerable<CompanyLogoViewModel> CompanyLogos { get; set; }

        public IEnumerable<PageMenuViewModel> AccountMenu { get; set; }

        public string WebsiteName { get; set; }

        public LanguageViewModel CurrentLanguage { get; set; }
        public IEnumerable<LanguageViewModel> Languages { get; set; }
        public string AccountLandingPageUrl { get; set; }
        public string ViewTicketUrl { get; set; }
        public string ContactUsUrl { get; set; }
        public string HelpUrl { get; set; }
        public string CategoriesUrl { get; set; }
        public string SettingsUrl { get; set; }
        public string WithdrawUrl { get; set; }
        public string BalanceApiUrl { get; set; }

        public bool IsLoggedIn { get; set; }

        public CustomerSummary CustomerSummary { get; set; }
        public string CurrencySymbol { get; set; }
        public string CurrencyFormat { get; set; }
        public bool CurrencyHasDecimals { get; set; }

        public string LoggedInUsername { get; set; }

        public string BackUrl { get; set; }
        public string TermsAndConditionsText { get; set; }
        public string TermsAndConditionsPageUrl { get; set; }
        public string CookiesPolicyPageUrl { get; set; }
        public string PrivacyPolicyPageUrl { get; set; }
        public int PageId { get; set; }
        public int CurrentPageId { get; set; }
        public Guid RootGuid { get; set; }
        public string DepositPageUrl { get; set; }
        public string DepositPageUrlAbsolute { get; set; }
        public string RootUrl { get; set; }
        public string PageContent { get; set; }
        public string Theme { get; set; }

        public IEnumerable<IPublishedContent> BreadCrumbs { get; set; }

        #region Tenant Information
        public string TenantUid { get; set; }
        public string Token { get; set; }
        public TenantPreferences Preferences { get; set; }
        public string DefaultLanguage { get; set; }
        public string[] Odds { get; set; }
        public List<TimeZone> TimeZones { get; set; }
        public List<Currency> Currencies { get; set; }
        public string[] Titles { get; set; }
        public List<SimpleCountry> Countries { get; set; }
        public string[] Genders { get; set; }
        public string[] PhoneCountryCodes { get; set; }
        public string[] Years { get; set; }
        public string BrandName { get; set; }
        public string Origin { get; set; }
        public string ServiceUrl { get; set; }
        public string Domain { get; set; }
        public string Subdomain { get; set; }


        public bool IsHomePage { get; set; }
        public bool IsMobileBrowser { get; set; }

        public string FaviconIco { get; set; }
        public string Favicon32x32 { get; set; }
        public string Favicon72x72 { get; set; }
        public string Favicon144x144 { get; set; }
        public string Favicon192x192 { get; set; }
        public string Favicon256x256 { get; set; }
        #endregion

        #region SEO (Meta Tags)

        public string MetaDescription { get; set; }
        public string MetaCopyright { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaAuthor { get; set; }
        public string MetaRobots { get; set; }
        #endregion


        #region Games
        public List<GameDetails> Games { get; set; }
        public List<SliderItem> Slider { get; set; }
        public List<MenuItem> MenuItems { get; set; }
        public int GameId { get; set; }
        public string GameUrl { get; set; }
        public string DemoUrl { get; set; }
        public string GameImage { get; set; }
        public bool HasDemoMode { get; set; }
        public string ReturnUrl { get; set; }
        public GameMode GameMode { get; set; }
        public bool OpenPopup { get; set; }
        public string PlayButtonText { get; set; }
        public string DemoButtonText { get; set; }
        public string GameMessage { get; set; }
        public string GameAgreeText { get; set; }
        public bool GameLoadSuccess { get; set; }
        public IEnumerable<IPublishedElement> PageImages { get; set; }
        public string Category { get; set; }
        public UrlType UrlType { get; set; }
        public bool ShowSubCategoryFilter { get; set; }
        public bool ShowProvidersFilter { get; set; }
        public bool ShowSeachFilter { get; set; }
        public bool SingleIconCentered { get; set; }
        public string IframeUrl { get; set; }
        public string PageTitle { get; set; }
        public string View { get; set; }
        #endregion
        #region Sport & In-Play
        public string SBColor { get; set; }
        #endregion

        #region Notification
        public string notificationPosition { get; set; }
        public string notificationBgColor { get; set; }
        public string notificationWidth { get; set; }
        #endregion
    }
}
