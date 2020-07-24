using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace Umbraco.Plugins.Connector.Controllers
{
    public class StylesheetBulkDownloadSurfaceController : SurfaceController
    {
        [HttpPost]
        public JsonResult GetThemeFolders()
        {
            var cssFilePath = Server.MapPath("~/") + "css\\";

            try
            {
                if (!System.IO.Directory.Exists(cssFilePath))
                    System.IO.Directory.CreateDirectory(cssFilePath);

                var folders = new List<string>();

                foreach (var dir in System.IO.Directory.GetDirectories(cssFilePath))
                {
                    folders.Add(System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(dir + "\\")));
                }

                return Json(new { status = "success", folders = folders }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = "error", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public FileContentResult DownloadFile(string folderNames)
        {
            try
            {
                if (string.IsNullOrEmpty(folderNames))
                    return null;

                var folders = JsonConvert.DeserializeObject<List<string>>(folderNames);

                using (var ms = new MemoryStream())
                {
                    using (var zipArchive = new ZipArchive(ms, ZipArchiveMode.Create, true))
                    {
                        var cssFilePath = Server.MapPath("~/") + "css\\";

                        foreach (var folderName in folders)
                        {
                            if (System.IO.Directory.Exists(cssFilePath + folderName))
                            {
                                var folderPath = cssFilePath + folderName + "\\";

                                zipArchive.CreateEntry(folderName + "/");

                                DirectoryInfo directorySelected = new DirectoryInfo(folderPath);

                                GetFileStream(directorySelected, zipArchive, cssFilePath);
                            }
                        }
                    }

                    return File(ms.ToArray(), "application/zip, application/octet-stream", $"Total-Code-Umbraco-Theme-{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.zip");
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void GetFileStream(DirectoryInfo directorySelected, ZipArchive zipArchive, string cssFilePath)
        {
            foreach (FileInfo file in directorySelected.GetFiles())
            {
                var entry = zipArchive.CreateEntry(file.FullName.Replace(cssFilePath, ""), CompressionLevel.Fastest);
                using (var entryStream = entry.Open())
                {
                    using (FileStream fs = file.OpenRead())
                    {
                        fs.CopyTo(entryStream);
                    }
                }
            }

            foreach (var dir in directorySelected.GetDirectories())
            {
                zipArchive.CreateEntry(dir.FullName.Replace(cssFilePath, "") + "/");
                GetFileStream(dir, zipArchive, cssFilePath);
            }
        }
    }
}
