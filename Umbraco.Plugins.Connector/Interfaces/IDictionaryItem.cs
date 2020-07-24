namespace Umbraco.Plugins.Connector.Interfaces
{
    using System.Collections.Generic;
    public interface IDictionaryItem
    {
        string ParentKey { get; }
        string Key { get; }
        string Value { get; }
        string LanguageCode { get; }
        Dictionary<string, string> Translations { get; }
    }

    public interface IDictionaryItemImportExport
    {
        string ParentKey { get; set;  }
        string Key { get; set;  }
        string Value { get; set;  }
        string LanguageCode { get; set;  }
    }
}
