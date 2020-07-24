namespace Umbraco.Plugins.Connector.Interfaces
{
    using System.Collections.Generic;
    using Umbraco.Plugins.Connector.Models;

    public interface IEmbeddedResource
    {
        List<EmbeddedResource> Resources { get; }
    }
}
