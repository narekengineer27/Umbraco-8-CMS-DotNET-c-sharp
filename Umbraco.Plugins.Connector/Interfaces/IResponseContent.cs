namespace Umbraco.Plugins.Connector.Interfaces
{
    using System;
    using Umbraco.Plugins.Connector.Models;
    public interface IResponseContent
    {
        string Message { get; set; }
        Exception Exception { get; set; }
        IResponseContent RelatedResponse { get; set; }
    }
    public interface IResponseContentErrors
    {
        ResponseContentError[] Errors { get; set; }
    }
    public interface IResponseContentError
    {
        int ErrorCode { get; set; }
        string ErrorMessage { get; set; }
    }
}
