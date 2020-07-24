namespace Umbraco.Plugins.Connector.Services
{
    using RestSharp;
    using System;
    using System.Threading.Tasks;
    using Umbraco.Plugins.Connector.Models;
    // based off https://exceptionnotfound.net/extending-restsharp-to-handle-timeouts-in-asp-net-mvc/
    public class RestClientExtended : RestSharp.RestClient
    {
        public RestClientExtended(string baseUrl)
        {
            BaseUrl = new Uri(baseUrl);
        }

        public override IRestResponse<T> Execute<T>(IRestRequest request)
        {
            var response = base.Execute<T>(request);
            TimeoutCheck(request, response);
            Unauthorized(request, response);
            //InternalServerError(request, response);
            //BadRequest(request, response);
            return response;
        }

        public override async Task<IRestResponse> ExecuteTaskAsync(IRestRequest request)
        {
            var response = await base.ExecuteTaskAsync(request);
            TimeoutCheck(request, response);
            Unauthorized(request, response);
            //InternalServerError(request, response);
            //BadRequest(request, response);
            return response;
        }

        public override async Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request)
        {
            var response = await base.ExecuteTaskAsync<T>(request);
            TimeoutCheck(request, response);
            Unauthorized(request, response);
            //InternalServerError(request, response);
            //BadRequest(request, response);
            return response;
        }

        private void TimeoutCheck(IRestRequest request, IRestResponse response)
        {
            if (response.StatusCode == 0)
            {
                var message = "{'success':false,'message':'Fail','errors':{'errorCode':" + (int)ApiPayloadErrorCodes.ConnectionTimeout + ",'errorMessage':'" + ApiPayloadErrorCodes.ConnectionTimeout.ToString() + "'}}";
                response.ContentType = "application/json; charset=utf-8";
                response.ContentLength = message.Length;
                response.Content = message;
            }
        }
        private void Unauthorized(IRestRequest request, IRestResponse response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var message = "{'success':false,'message':'Fail','errors':{'errorCode':" + (int)ApiPayloadErrorCodes.UnAuthorized + ",'errorMessage':'" + ApiPayloadErrorCodes.UnAuthorized.ToString() + "'}}";
                response.ContentType = "application/json; charset=utf-8";
                response.ContentLength = message.Length;
                response.Content = message;
            }
        }
        private void InternalServerError(IRestRequest request, IRestResponse response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                var message = "{'success':false,'message':'Fail','errors':{'errorCode':" + (int)ApiPayloadErrorCodes.InternalServerError + ",'errorMessage':'" + ApiPayloadErrorCodes.InternalServerError.ToString() + "'}}";
                response.ContentType = "application/json; charset=utf-8";
                response.ContentLength = message.Length;
                response.Content = message;
            }
        }
        private void BadRequest(IRestRequest request, IRestResponse response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var message = "{'success':false,'message':'Fail','errors':{'errorCode':" + (int)ApiPayloadErrorCodes.BadRequest + ",'errorMessage':'" + ApiPayloadErrorCodes.BadRequest.ToString() + "'}}";
                response.ContentType = "application/json; charset=utf-8";
                response.ContentLength = message.Length;
                response.Content = message;
            }
        }
    }

}
