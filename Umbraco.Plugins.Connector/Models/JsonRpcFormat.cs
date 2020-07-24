using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Plugins.Connector.Models
{
    public class JsonRpcFormat<T>
    {
        [JsonProperty(PropertyName = "jsonrpc")]
        public string JsonRpc => "2.0";

        [JsonProperty(PropertyName = "id")]
        public int Id => 1;

        [JsonProperty(PropertyName = "method")]
        public string Method { get; set; }

        [JsonProperty(PropertyName = "params")]
        public T Params { get; set; }
    }

    public class JsonRpcFormatParams<T>
    {
        [JsonProperty(PropertyName = "model")]
        public T Model { get; set; }
    }

    public class JsonRpcFormatParamsForGetDetails
    {
        [JsonProperty(PropertyName = "ticketId")]
        public int TicketId { get; set; }
    }
}



