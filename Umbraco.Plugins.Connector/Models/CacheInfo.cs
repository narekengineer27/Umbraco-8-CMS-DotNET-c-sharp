using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Plugins.Connector.Models
{
    public class CacheInfo
    {
        public CacheInfo()
        {
            Tags = new Dictionary<string, string>();
        }

        public string CacheName { get; set; }

        public string TenantUid { get; set; }

        public Dictionary<string, string> Tags { get; set; }
    }
}
