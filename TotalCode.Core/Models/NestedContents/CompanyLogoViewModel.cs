using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.IO;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using System.IO;

namespace TotalCode.Core.Models.NestedContents
{
    public class CompanyLogoViewModel
    {
        public CompanyLogoViewModel(IPublishedElement element)
        {
            Title = element.Value<string>("title");
            var img = element.Value<IPublishedContent>("image");
            Image = img;
            Svg = "";
            if (img != null)
            {
                IsSvg = Path.GetExtension(img.Url) == ".svg";
                var imgAbsolutePath = IOHelper.MapPath(img.Url);
                if (File.Exists(imgAbsolutePath))
                {
                    Svg = File.ReadAllText(imgAbsolutePath);
                }
            }

            Link = element.Value<string>("link");
        }

        public string Title { get; set; }
        public IPublishedContent Image { get; set; }
        public string Svg { get; set; }
        public bool IsSvg { get; set; }
        public string Link { get; set; }
    }
}
