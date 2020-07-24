namespace Umbraco.Plugins.Connector.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Filters;
    using System.Web.Http.Results;
    using Umbraco.Plugins.Connector.Cache;
    using Umbraco.Plugins.Connector.Helpers;

    /// <summary>
    /// Api Key Filter
    /// </summary>
    /// <see cref="http://bitoftech.net/2014/12/15/secure-asp-net-web-api-using-api-key-authentication-hmac-authentication/"/>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class ApiKeyAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        private static readonly Dictionary<string, string> allowedApps = new Dictionary<string, string>();
        private readonly string authenticationScheme = "ApiKey";

        public ApiKeyAuthenticationAttribute()
        {
            var keys = ApiKeyCache.Keys.ToList();
            if (keys.Count > 0)
            {
                foreach (var key in keys)
                {
                    if (!allowedApps.ContainsKey(key.AppId))
                        allowedApps.Add(key.AppId, key.ApiKey);
                }
            }

            //if (allowedApps.Count == 0)
            //{
            //    allowedApps.Add(ConfigurationManager.AppSettings["TotalCode.Admin.AppId"], ConfigurationManager.AppSettings["TotalCode.Admin.ApiKey"]);
            //}

            // add Total Code Admin App Id and Api Key
            if (!allowedApps.ContainsKey(ConfigurationManager.AppSettings["TotalCode.Admin.AppId"]))
                allowedApps.Add(ConfigurationManager.AppSettings["TotalCode.Admin.AppId"], ConfigurationManager.AppSettings["TotalCode.Admin.ApiKey"]);
        }

        public bool AllowMultiple => false;

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var req = context.Request;

            if (req.Headers.Authorization != null && authenticationScheme.Equals(req.Headers.Authorization.Scheme, StringComparison.OrdinalIgnoreCase))
            {
                var rawAuthzHeader = req.Headers.Authorization.Parameter;

                var autherizationHeaderArray = GetAutherizationHeaderValues(rawAuthzHeader);

                if (autherizationHeaderArray != null)
                {
                    var appId = autherizationHeaderArray[0];
                    var apiKey = autherizationHeaderArray[1];

                    var isValid = IsValidRequest(req, appId, apiKey);

                    if (isValid)
                    {
                        context.Principal = new GenericPrincipal(new GenericIdentity(appId), null);
                    }
                    else
                    {
                        context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
                    }
                }
                else
                {
                    context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
                }
            }
            else
            {
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
            }
            await Task.FromResult(false).ConfigureAwait(false);
        }

        private bool IsValidRequest(HttpRequestMessage req, string appId, string phrase)
        {
            string requestUri = HttpUtility.UrlEncode(req.RequestUri.AbsoluteUri.ToLower());
            string requestHttpMethod = req.Method.Method;

            if (!requestHttpMethod.Equals("POST")) { return false; }

            if (!allowedApps.ContainsKey(appId))
            {
                return false;
            }

            var sharedKey = allowedApps[appId];
            return EncryptDecryptHelper.Sha256Matches(phrase, sharedKey);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            context.Result = new ResultWithChallenge(context.Result);
            return Task.FromResult(0);
        }

        private string[] GetAutherizationHeaderValues(string rawAuthzHeader)
        {
            var credArray = rawAuthzHeader.Split(':');

            if (credArray.Length == 2)
            {
                return credArray;
            }
            else
            {
                return null;
            }
        }
    }

    public class ResultWithChallenge : IHttpActionResult
    {
        private readonly string authenticationScheme = "ApiKey";
        private readonly IHttpActionResult next;

        public ResultWithChallenge(IHttpActionResult next)
        {
            this.next = next;
        }

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = await next.ExecuteAsync(cancellationToken).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue(authenticationScheme));
            }

            return response;
        }
    }
}
