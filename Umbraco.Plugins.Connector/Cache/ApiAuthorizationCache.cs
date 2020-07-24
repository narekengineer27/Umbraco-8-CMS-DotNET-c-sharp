using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Plugins.Connector.Helpers;
using Umbraco.Plugins.Connector.Models;
using Umbraco.Plugins.Connector.Services;

namespace Umbraco.Plugins.Connector.Cache
{
    public static class ApiAuthorizationCache
    {
        private static readonly string AUTH_KEY = "authorization";

        public static ApiKeyLoginResponseContent GetOrSetAuthorization(string tenantUid)
        {
            var cacheInfo = new CacheInfo()
            {
                CacheName = AUTH_KEY,
                TenantUid = tenantUid
            };

            var result = CacheHelper.GetCache<ApiKeyLoginResponseContent>(cacheInfo);

            if(result == null || result.Expires <= DateTime.UtcNow)
            {
                CacheHelper.ClearCache(cacheInfo);
                result = CacheHelper.GetOrSetCache<ApiKeyLoginResponseContent>(cacheInfo,
                            () =>
                            {
                                var key = ApiKeyCache.GetByTenantUid(tenantUid);
                                var apiService = new TotalCodeApiService();
                                var res = (ApiKeyLoginResponseContent)apiService.ApiKeyLogin(key.ApiKey, key.AppId);
                                return res;
                            }, TimeSpan.FromSeconds(CacheSetting.TimeoutInSeconds));
            }

            return result;
        }
    }
}
