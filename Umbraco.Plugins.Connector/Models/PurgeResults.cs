namespace Umbraco.Plugins.Connector.Models
{
    public class PurgeResults
    {
        public bool ContentFoundAndDeleted { get; set; }
        public bool GroupFoundAndDeleted { get; set; }
        public bool MediaFolderFoundAndDeleted { get; set; }
        public bool UserFoundAndDeleted { get; set; }
    }
}
