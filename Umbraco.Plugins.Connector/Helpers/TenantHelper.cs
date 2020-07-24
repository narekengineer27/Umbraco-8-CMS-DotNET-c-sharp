namespace Umbraco.Plugins.Connector.Helpers
{
    using Newtonsoft.Json;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Models;
    using Umbraco.Plugins.Connector.Services;
    using Umbraco.Web.Models;

    public class Authorization
    {
        private readonly TotalCodeApiService apiService;
        public Authorization()
        {
            apiService = new TotalCodeApiService();
        }
        public async Task<ApiKeyLoginResponseContent> GetAuthorizationAsync(ApiKeys key)
        {
            DateTime now = DateTime.Now;
            ApiKeyLoginResponseContent response = (ApiKeyLoginResponseContent)HttpContext.Current.Session["authorization"];

            if (response == null || response.Expires <= now)
            {
                response = (ApiKeyLoginResponseContent)await apiService.ApiKeyLoginAsync(key);
                HttpContext.Current.Session["authorization"] = (ApiKeyLoginResponseContent)response;
            }
            return response;
        }
    }
    public static class TenantHelper
    {
        private static readonly TotalCodeApiService apiService;
        static TenantHelper()
        {
            apiService = new TotalCodeApiService();
        }

        public static string GetCurrentTenantUrl(IContentService contentService, string tenantUid)
        {
            var tenant = GetCurrentTenantHome(contentService, tenantUid);
            var domain = tenant.GetValue("domain").ToString();
            var subdomain = tenant.GetValue("subDomain").ToString();
            var protocol = bool.Parse(TenantGenerationOptions.SecureUrls) ? "https://" : "http://";
            return $"{protocol}{subdomain}.{domain}";
        }

        public static string GetCurrentTenantUrl(ContentModel model)
        {
            var tenant = model.Content.Parent;
            var domain = tenant.GetProperty("domain").GetValue().ToString();
            var subdomain = tenant.GetProperty("subDomain").GetValue().ToString();
            return $"http://{subdomain}.{domain}";
        }

        public static bool TenantExist(IContentService contentService, string tenantUid)
        { 
            return contentService.GetByLevel(1).FirstOrDefault((x => x.HasProperty("tenantUid") && x.GetValue("tenantUid").ToString().Equals(tenantUid))) != null;
        }

        public static IContent GetCurrentTenantHome(IContentService contentService, string tenantUid)
        {
            var tenantHome = contentService.GetByLevel(1).SingleOrDefault(x => x.HasProperty("tenantUid") && x.GetValue("tenantUid").ToString().Equals(tenantUid));
            return tenantHome;
        }

        public static IEnumerable<IContent> GetTenantNodes(IContentService contentService, string tenantUid)
        {
            var home = GetCurrentTenantHome(contentService, tenantUid);
            var children = contentService.GetByLevel(2).Where(x => x.ParentId == home.Id);
            return children;
        }
        public static ApiKeys GetCurrentTenantApiKey(IContentService contentService, string tenantUid)
        {
            var tenant = GetCurrentTenantHome(contentService, tenantUid);
            return new ApiKeys
            {
                AppId = tenant.GetValue<string>("appId"),
                ApiKey = tenant.GetValue<string>("apiKey")
            };
        }

        public static TenantPreferences GetCurrentTenantPreferences(IContentService contentService, string tenantUid)
        {
            var tenant = GetCurrentTenantHome(contentService, tenantUid);
            return JsonConvert.DeserializeObject<TenantPreferences>(tenant.GetValue("tenantPreferencesProperty").ToString());
        }
    }
}
