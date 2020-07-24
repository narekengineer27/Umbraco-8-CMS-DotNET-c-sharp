namespace Umbraco.Plugins.Connector.Cache
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Persistence;
    using Umbraco.Plugins.Connector.ConnectorServices;
    using Umbraco.Plugins.Connector.Models;

    public static class ApiKeyCache
    {
        private static DateTime lastUpdate;
        public static List<ApiKeys> Keys { get; internal set; } = new List<ApiKeys>();

        public static void UpdateCache(IUmbracoDatabase database)
        {
            if (lastUpdate < DateTime.Now && lastUpdate <= DateTime.Now.AddMinutes(-5))
            {
                Keys = new ApiKeysService(database).GetAll().Result.ToList();
                lastUpdate = DateTime.Now;
            }
        }

        public static void ForceUpdateCache(IUmbracoDatabase database)
        {
            Keys = new ApiKeysService(database).GetAll().Result.ToList();
            lastUpdate = DateTime.Now;
        }

        public static ApiKeys GetByTenantUid(string tenantUid)
        {
            var key = new ApiKeys();
            foreach (var k in Keys)
            {
                if (k.TenantId == tenantUid || k.TenantId.ToLower() == tenantUid)
                {
                    key = k;
                }
            }
            return key;
        }
    }
}
