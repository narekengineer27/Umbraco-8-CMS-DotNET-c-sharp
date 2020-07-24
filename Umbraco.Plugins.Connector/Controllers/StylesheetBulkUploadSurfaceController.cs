using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace Umbraco.Plugins.Connector.Controllers
{
    public class StylesheetBulkUploadSurfaceController : SurfaceController
    {

        [HttpPost]
        public JsonResult UploadFile()
        {
            var destinationPath = Request.Form["destinationFilePath"];
            var isFileExisting = false;
            try
            {
                if (!string.IsNullOrEmpty(destinationPath))
                {
                    var path = HttpUtility.UrlDecode(destinationPath).Replace("/", "\\");
                    var serverFilePath = Server.MapPath("~/") + "css\\" + path;

                    if (!System.IO.Directory.Exists(serverFilePath))
                        System.IO.Directory.CreateDirectory(serverFilePath);

                    if (Request.Files[0] != null)
                    {
                        HttpPostedFileBase file = Request.Files[0];
                        var fileName = file.FileName;
                        if (!System.IO.File.Exists(serverFilePath + fileName))
                        {
                            System.IO.Stream fileContent = file.InputStream;
                            file.SaveAs(serverFilePath + fileName);
                        }
                        else
                        {
                            isFileExisting = true;
                        }
                    }
                }

                return Json(new
                {
                    isFileExisting = isFileExisting,
                    path = GetPath(HttpUtility.UrlDecode(destinationPath))
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetPath(string destinationPath)
        {
            var list = new List<string>();
            var ctr = 0;
            foreach (var item in destinationPath.Split('/').Reverse())
            {
                ctr++;
                var temp = new List<string>();
                temp.Add(item);
                foreach (var test in destinationPath.Split('/').Reverse().Skip(ctr))
                {
                    temp.Add(test);
                }
                temp.Reverse();
                list.Add(HttpUtility.UrlEncode(string.Join("/", temp)));
            }
            list.Reverse();
            return string.Join(",", list);
        }
    }
}
