namespace Umbraco.Plugins.Connector
{
    using Umbraco.Core.Logging;
    using Umbraco.Core.Scoping;
    using Umbraco.Core.Services;
    using Umbraco.Web;

    public static class ApiUrls
    {
        public static string CustomerManagementUrl { get; set; }
        public static string FinancialManagementUrl { get; set; }
        public static string GameManagementUrl { get; set; }
        public static string UserManagementUrl { get; set; }
        public static string HelpdeskManagementUrl { get; set; }
        public static string HelpdeskManagementUploadUrl { get; set; }
        public static string CurrencyManagementUrl { get; set; }
        public static string NotificationManagementUrl { get; set; }
    }

    public static class ConnectorContext
    {
        public static IAuditService AuditService { get; set; }
        public static IContentService ContentService { get; set; }
        public static IContentTypeService ContentTypeService { get; set; }
        public static IUmbracoContextFactory ContextFactory { get; set; }
        public static IDataTypeService DataTypeService { get; set; }
        public static IDomainService DomainService { get; set; }
        public static IFileService FileService { get; set; }
        public static ILocalizationService LocalizationService { get; set; }
        public static ILocalizedTextService LocalizedTextService { get; set; }
        public static ILogger Logger { get; set; }
        public static string MasterApiKey { get; set; }
        public static string MasterAppId { get; set; }
        public static IMediaService MediaService { get; set; }
        public static IMediaTypeService MediaTypeService { get; set; }
        public static IMemberService MemberService { get; set; }
        public static IPublicAccessService PublicAccessService { get; set; }
        public static IScopeProvider ScopeProvider { get; set; }
        public static ITagService TagService { get; set; }
        public static UmbracoContext UmbracoContext { get; set; }
        public static IUserService UserService { get; set; }
        public static IContentTypeBaseServiceProvider ContentTypeBaseService { get; set; }
    }

    public static class IframeUrls
    {
        public static string CasinoIframeUrl { get; set; }
        public static string InPlayIframeUrl { get; set; }
        public static string LiveCasinoIframeUrl { get; set; }
        public static string LotteryIframeUrl { get; set; }
        public static string PokerframeUrl { get; set; }
        public static string SportIframeUrl { get; set; }
        public static string VegasIframeUrl { get; set; }
        public static string BoardIframeUrl { get; set; }
    }
    public static class TenantGenerationOptions
    {
        public static string SaveAndPublish { get; set; }
        public static string SecureUrls { get; set; }
        public static string SetupLocalUrls { get; set; }
    }

    public static class CacheSetting
    {
        public static double TimeoutInSeconds { get; set; }
    }
}
