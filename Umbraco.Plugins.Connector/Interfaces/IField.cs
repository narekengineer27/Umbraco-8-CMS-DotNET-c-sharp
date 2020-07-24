namespace Umbraco.Plugins.Connector.Interfaces
{
    public interface IField
    {
        object[] AllowedValues { get; set; }
        bool IsMandatory { get; set; }
        bool IsUsed { get; set; }
        object MaxValue { get; set; }
        object MinValue { get; set; }
        string Validation { get; set; }

        string ObjectName { get; }
    }
}
