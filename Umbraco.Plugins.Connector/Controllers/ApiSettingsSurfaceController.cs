namespace Umbraco.Plugins.Connector.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Caching;
    using System.Web;
    using System.Web.Mvc;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Models;
    using Umbraco.Web.Mvc;

    /// <summary>
    /// Summary description for ApiSettingsSurfaceController
    /// </summary>
    public class ApiSettingsSurfaceController : SurfaceController
    {
        private readonly ConfigurationService configurationHelper;

        public ApiSettingsSurfaceController(ILocalizationService localizationService, IDomainService domainService, ILogger logger)
        {
            this.configurationHelper = new ConfigurationService();
        }

        [HttpGet]
        public JsonResult ReadSettings()
        {
            var settings = configurationHelper.GetApiSettings();
            return base.Json(new { settings, status = "OK" }, JsonRequestBehavior.AllowGet);
        }

        //[System.Obsolete]
        //[HttpPost]
        //public JsonResult SaveSettings(string appId, string apiKey)
        //{
        //    configurationHelper.EditApiInfo(appId, apiKey);
        //    return Json(new { status = "OK" }, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult SaveSettings(ApiSettings settings)
        {
            configurationHelper.EditApiSettings(settings);
            return Json(new { status = "OK" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ClearApiCache()
        {
            CacheHelper.ClearAllCache();
            return Json(new { status = "OK" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ClearSelectedApiCache(CacheInfo cacheInfo)
        {
            if (!string.IsNullOrEmpty(cacheInfo.TenantUid) && !string.IsNullOrEmpty(cacheInfo.CacheName))
            {
                var caches = CacheHelper.GetAllCacheItems.Where(p => p.CacheName == cacheInfo.CacheName && p.TenantUid == cacheInfo.TenantUid);
                foreach (var item in caches)
                {
                    CacheHelper.ClearCache(item);
                }
            }
            else
            {
                var caches = CacheHelper.GetAllCacheItems.Where(p => p.TenantUid == cacheInfo.TenantUid);
                foreach (var item in caches)
                {
                    CacheHelper.ClearCache(item);
                }
            }
            return Json(new { status = "OK" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetTenantUidsOnCache()
        {
            var res = CacheHelper.GetAllCacheItems.Select(p => p.TenantUid).ToList().Distinct();
            return Json(new { tenantUids = res }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetCacheNamesOnCache(string tenantUid)
        {
            var res = CacheHelper.GetAllCacheItems.Where(p => p.TenantUid == tenantUid).Select(p => p.CacheName).Distinct().ToList();
            return Json(new { cacheNames = res }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetErrorPages()
        {
            var errorDirectory = Server.MapPath("~/errors");
            var files = new DirectoryInfo(errorDirectory).EnumerateFiles();
            List<string> fullPathFiles = new List<string>();
            foreach (var file in files)
            {
                fullPathFiles.Add($"~/errors/{file.Name}");
            }
            return Json(new { files = fullPathFiles }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetErrorPageContent(string path)
        {
            var data = System.IO.File.ReadAllText(Server.MapPath(path));
            var results = new { Status = "OK", Success = true, Data = data };
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public JsonResult SaveErrorPageContent(string path, string content)
        {
            System.IO.File.WriteAllText(Server.MapPath(path), content);
            var results = new { Status = "OK", Success = true };
            return Json(results, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult DeleteErrorPage(string path)
        {
            System.IO.File.Delete(Server.MapPath(path));
            var results = new { Status = "OK", Success = true };
            return Json(results, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult SaveNewErrorPageContent(string pageName)
        {
            var errorDirectory = Server.MapPath("~/errors");
            var path = $"{errorDirectory}/{pageName}.html";
            var htmlTemplate = 
@"<!doctype html>

<html lang='en'>
    <head>
        <meta charset='utf-8'>

        <title>New Html Error Page</title>
        <meta name='description' content='change-me'>
        <meta name='author' content='change-me'>

        <link rel='stylesheet' href='css/styles.css?v=1.0'>

    </head>

    <body>
        <script src='js/scripts.js'></script>
    </body>
</html>";
            object results = null;
            if (!System.IO.File.Exists(path))
            {
                System.IO.File.WriteAllText(path, htmlTemplate);
                results = new { Status = "OK", Success = true };
            }
            else
            {
                results = new { Status = "File Already Exists", Success = false };
            }
            return Json(results, JsonRequestBehavior.DenyGet);
        }
    }
}