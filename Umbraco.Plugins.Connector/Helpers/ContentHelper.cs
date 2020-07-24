namespace Umbraco.Plugins.Connector.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Web;
    using Umbraco.Core.Models;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Interfaces;
    using Umbraco.Plugins.Connector.Models;
    public static class ContentHelper
    {
        public static void CopyPhysicalAssets(IEmbeddedResource embeddedResource)
        {
            var list = GetListOfEmbeddedResources(embeddedResource, ResourceType.All);
            var assembly = Assembly.GetExecutingAssembly();

            if (list?.Count > 0)
            {
                //Copy physical files to destination
                assembly.ExtractEmbeddedResource(list);
            }
        }

        private static List<EmbeddedResource> GetListOfEmbeddedResources(IEmbeddedResource embeddedResource, ResourceType resourceType)
        {
            List<EmbeddedResource> list = new List<EmbeddedResource>();

            foreach (var resource in embeddedResource.Resources)
            {
                if (resource.ResourceType == resourceType || resourceType == ResourceType.All)
                    list.Add(resource);
            }
            return list;
        }

        public static bool OnOffToBool(string state)
        {
            if (state.Equals("on")) return true;
            else if (state.Equals("off")) return false;
            else return false;
        }

        public static string Sanitize(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            value = value.Trim().ToLower();
            value = value.Replace("-", string.Empty);
            value = value.Replace(" ", string.Empty);
            value = value.Replace("?", string.Empty);
            value = value.Replace("!", string.Empty);
            value = value.Replace("@", string.Empty);
            value = value.Replace("#", string.Empty);
            value = value.Replace("$", string.Empty);
            value = value.Replace("%", string.Empty);
            value = value.Replace("&", string.Empty);
            value = value.Replace("*", string.Empty);
            value = value.Replace("(", string.Empty);
            value = value.Replace(")", string.Empty);
            value = value.Replace(".", string.Empty);
            value = value.Replace(",", string.Empty);
            value = value.Replace(";", string.Empty);
            value = value.Replace(":", string.Empty);
            value = value.Replace("'", string.Empty);
            value = value.Replace("~", string.Empty);
            var rgx = new Regex("[^a-zA-Z0-9 -]");
            return rgx.Replace(value, "");
        }

        public static void AddAllowedDocumentType(IContentTypeService service, string parentAlias, string childAlias, int order = -1)
        {
            var parent = service.Get(parentAlias);
            var child = service.Get(childAlias);
            var allowed = parent.AllowedContentTypes.ToList();
            allowed.Add(new Core.Models.ContentTypeSort(child.Id, order == -1 ? allowed.Count() : order));
            parent.AllowedContentTypes = allowed;
            service.Save(parent);
        }

        public static void AddTemplate(this IContentType type, IContentTypeService service, Template template, int order = -1)
        {
            var templates = type.AllowedTemplates.ToList();
            templates.Add(template);
            type.AllowedTemplates = templates;
            service.Save(type);
        }

        public static void SetTemplate(IContentService service, IFileService fileService, int nodeId, string templateAlias)
        {
            var node = service.GetById(nodeId);
            var template = fileService.GetTemplate(templateAlias);
            node.TemplateId = template.Id;
            service.Save(node);
        }

        /// <summary>
        /// Kindly borrowed from https://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings
        /// </summary>
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static bool AssetAlreadyExists(string fileName, string outputDirectory)
        {
            var appRoot = HttpContext.Current.Server.MapPath("~/");
            return ResourceHelper.CheckExists(appRoot, fileName, outputDirectory);
        }

        // update : October 2019
        static Regex MobileCheck = new Regex(@"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        static Regex MobileVersionCheck = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase | RegexOptions.Multiline);

        public static bool BrowserIsMobile()
        {
            Debug.Assert(HttpContext.Current != null);

            if (HttpContext.Current.Request != null && HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"] != null)
            {
                var u = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"].ToString();

                if (u.Length < 4)
                    return false;

                if (MobileCheck.IsMatch(u) || MobileVersionCheck.IsMatch(u.Substring(0, 4)))
                    return true;
            }

            return false;
        }
        public static string EncodeUrl(this string url)
        {
            return HttpUtility.UrlEncode(url);
        }
        public static string DecodeUrl(this string url)
        {
            return HttpUtility.UrlDecode(url);
        }
    }
}
