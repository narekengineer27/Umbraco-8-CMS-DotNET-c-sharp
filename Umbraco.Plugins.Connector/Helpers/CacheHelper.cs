using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Umbraco.Core.Cache;
using Umbraco.Plugins.Connector.Models;

namespace Umbraco.Plugins.Connector.Helpers
{
    /// <summary>
    /// Lazy loaded Caching
    /// </summary>
    public static class CacheHelper
    {
        public static T GetOrSetCache<T>(CacheInfo cacheInfo, Func<T> func)
        {
            var cache = new AppCaches();
            var cacheName = GetCacheName(cacheInfo);
            return cache.RuntimeCache.GetCacheItem<T>(cacheName, func);
        }

        public static T GetOrSetCache<T>(CacheInfo cacheInfo, Func<T> func, TimeSpan timeSpan)
        {
            var cache = new AppCaches();
            var cacheName = GetCacheName(cacheInfo);
            return cache.RuntimeCache.GetCacheItem<T>(cacheName, func, timeSpan);
        }

        public static async Task<T> GetOrSetCacheAsync<T>(CacheInfo cacheInfo, Func<Task<T>> p)
        {
            var cache = new AppCaches();
            var cacheName = GetCacheName(cacheInfo);
            var cacheResult = cache.RuntimeCache.GetCacheItem<T>(cacheName);
            if (cacheResult == null)
            {
                var process = await p();
                return cache.RuntimeCache.GetCacheItem<T>(JsonConvert.SerializeObject(cacheInfo), () =>
                {
                    return process;
                });
            }
            else
            {
                return cacheResult;
            }
        }

        public static async Task<T> GetOrSetCacheAsync<T>(CacheInfo cacheInfo, Func<Task<T>> p, TimeSpan timeSpan)
        {
            var cache = new AppCaches();
            var cacheName = GetCacheName(cacheInfo);
            var cacheResult = cache.RuntimeCache.GetCacheItem<T>(cacheName);
            if (cacheResult == null)
            {
                var process = await p();
                return cache.RuntimeCache.GetCacheItem<T>(JsonConvert.SerializeObject(cacheInfo), () =>
                {
                    return process;
                }, timeSpan);
            }
            else
            {
                return cacheResult;
            }
        }

        public static T GetCache<T>(CacheInfo cacheInfo)
        {
            var cache = new AppCaches();
            var cacheName = GetCacheName(cacheInfo);
            return cache.RuntimeCache.GetCacheItem<T>(cacheName);
        }

        public static void ClearCache(CacheInfo cacheInfo)
        {
            var cache = new AppCaches();
            var cacheName = GetCacheName(cacheInfo);
            cache.RuntimeCache.Clear(cacheName);
        }

        public static void ClearAllCache()
        {
            var cache = new AppCaches();
            cache.RuntimeCache.Clear();
        }

        private static string GetCacheName(CacheInfo cacheInfo)
        {
            return JsonConvert.SerializeObject(cacheInfo);
        }

        public static List<CacheInfo> GetAllCacheItems
        {
            get
            {
                var caches = new List<CacheInfo>();
                foreach (System.Collections.DictionaryEntry item in HttpRuntime.Cache)
                {
                    var str = item.Key.ToString().Replace("umbrtmche-", "");

                    try
                    {
                        var info = JsonConvert.DeserializeObject<CacheInfo>(str);
                        caches.Add(info);
                    }
                    catch (Exception ex)
                    {
                        //Ignore non CacheItem type data
                    }

                }

                return caches;
            }
        }
    }


    public class CacheTagBuilder
    {
        private Dictionary<string, string> _keyValuePairs;

        public CacheTagBuilder()
        {
            _keyValuePairs = new Dictionary<string, string>();
        }

        public CacheTagBuilder Add<T>(Expression<Func<T>> memberExpression)
        {
            var expressionBody = (MemberExpression)memberExpression.Body;
            _keyValuePairs.Add(expressionBody.Member.Name, memberExpression.Compile().Invoke().ToString());
            return this;
        }

        public Dictionary<string, string> Compile()
        {
            return _keyValuePairs;
        }
    }
}
