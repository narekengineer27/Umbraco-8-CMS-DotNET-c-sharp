namespace Umbraco.Plugins.Connector.Models
{
    public class JsonRpcResponseData
    {
        public const string OK = "Ok", ERROR = "Error";
        public string Message { get; set; }
        public string TenantUid { get; set; }
        public string Status { get; set; }
        public object Data { get; set; }
    }
}
