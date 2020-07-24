namespace Umbraco.Plugins.Connector.Interfaces
{
    public interface IFieldMandatory : IField
    {
        new bool IsMandatory { get; }
        new bool IsUsed { get; }
    }
}
