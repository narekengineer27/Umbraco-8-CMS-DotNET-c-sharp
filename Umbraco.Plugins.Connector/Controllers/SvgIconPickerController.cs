using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using Umbraco.Core.IO;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace Umbraco.Plugins.Connector.Controllers.SvgIconPicker
{
    [PluginController("Plugins")]
    public class SvgIconPickerController : UmbracoAuthorizedApiController
    {
        [HttpGet]
        public object GetIcons()
        {
            string str = IOHelper.MapPath("/img/svg/");
            DirectoryInfo directoryInfo = new DirectoryInfo(IOHelper.MapPath(str));
            return (object)new
            {
                path = IOHelper.MapPath(str).ToString(),
                icons = ((IEnumerable<FileInfo>)directoryInfo.GetFiles("*.svg")).Select<FileInfo, string>((Func<FileInfo, string>)(file => Path.GetFileNameWithoutExtension(file.FullName)))
            };
        }

        [HttpGet]
        public object GetSvg(string icon = null)
        {
            string str = IOHelper.MapPath("/img/svg/");
            DirectoryInfo directoryInfo = new DirectoryInfo(IOHelper.MapPath(str));
            string str2 = System.IO.File.ReadAllText(((IEnumerable<FileInfo>)directoryInfo.GetFiles("*.svg")).FirstOrDefault<FileInfo>((Func<FileInfo, bool>)(x => Path.GetFileNameWithoutExtension(x.FullName) == icon)).FullName);

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "image/svg+xml";
            HttpContext.Current.Response.Write(str2);
            HttpContext.Current.Response.End();
            return (object)null;
        }
    }
}