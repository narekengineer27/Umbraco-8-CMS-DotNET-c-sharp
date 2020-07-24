namespace Umbraco.Plugins.Connector.Models
{
    using System.Collections.Generic;
    public class Languages
    {
        public string Default { get; set; }
        public IEnumerable<string> Alternate { get; set; }
    }

    public class Language
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
