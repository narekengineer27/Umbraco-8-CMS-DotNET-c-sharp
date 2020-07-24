namespace Umbraco.Plugins.Connector.Helpers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web;
    using Umbraco.Plugins.Connector.Dictionaries;

    public static class FileSystemHelper
    {
        public static List<AuditDictionaryItem> GetAllRazorDictionaryReferences(IEnumerable<ExportDictionaryItem> dictionaryItems)
        {
            var list = new List<AuditDictionaryItem>();
            var pattern = "Umbraco\\.GetDictionaryValue\\(\\\"(\\[[a-zA-Z]*\\]?[a-zA-Z]+)\\\"";
            var path = HttpContext.Current.Server.MapPath("~/Views");
            var extension = "*.cshtml";
            var matches = GetAllFilesContaining(path, pattern, extension);
            foreach (var match in matches)
            {
                foreach (var item in match.Value)
                {
                    Regex regex = new Regex("\\[([a-zA-Z]*)\\]");
                    Match parent = regex.Match(item);
                    var parentKey = string.IsNullOrEmpty(parent.Groups[0].Value) ? string.Empty : parent.Groups[0].Value.Replace("[", string.Empty).Replace("]", string.Empty);

                    bool isRegistered = dictionaryItems.ToList().FirstOrDefault(x => x.Key == item) != null;
                    list.Add(new AuditDictionaryItem
                    {
                        File = match.Key,
                        Key = item,
                        Registered = isRegistered,
                        ParentKey = parentKey
                    });
                }
            }
            return list;
        }
        public static Dictionary<string, List<string>> GetAllFilesContaining(string path, string pattern, string extension = "*.*")
        {
            var files = GetAllFiles(path, extension);
            var matches = new Dictionary<string, List<string>>();
            foreach (var file in files)
            {
                var fileMatches = HasMatch(file, pattern);
                if (fileMatches.Count > 0)
                {
                    matches.Add(file, fileMatches);
                }
            }
            return matches;
        }
        public static IEnumerable<string> GetAllFiles(string path, string extension)
        {
            return Directory.EnumerateFiles(path, extension, SearchOption.AllDirectories);
        }

        private static List<string> HasMatch(string path, string matchText)
        {
            Regex regex = new Regex(matchText);
            var matches = new List<string>();
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Match match = regex.Match(line);
                    if (match.Success)
                    {
                        string v = match.Groups[1].Value;
                        matches.Add(v);
                    }
                }
            }
            return matches;
        }
    }
}
