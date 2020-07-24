namespace Umbraco.Plugins.Connector.Services
{
    using Newtonsoft.Json;
    using RestSharp;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using Umbraco.Core.Logging;
    using Umbraco.Plugins.Connector.Interfaces;
    using Umbraco.Plugins.Connector.Models;

    public abstract class BaseService
    {
        private const int TIMEOUT = 60 * 1000; // miliseconds
        private readonly bool VERBOSE = false;
        #region Api Urls
        protected readonly string URL_CUSTOMER_MANAGEMENT_DOMAIN = ApiUrls.CustomerManagementUrl;
        protected readonly string URL_FINANCIAL_MANAGEMENT_DOMAIN = ApiUrls.FinancialManagementUrl;
        protected readonly string URL_USER_MANAGEMENT_DOMAIN = ApiUrls.UserManagementUrl;
        protected readonly string URL_GAME_MANAGEMENT_DOMAIN = ApiUrls.GameManagementUrl;
        protected readonly string URL_HELPDESK_MANAGEMENT_DOMAIN = ApiUrls.HelpdeskManagementUrl;
        protected readonly string URL_HELPDESK_MANAGEMENT_UPLOAD_DOMAIN = ApiUrls.HelpdeskManagementUploadUrl;
        protected readonly string URL_CURRENCY_MANAGEMENT_DOMAIN = ApiUrls.CurrencyManagementUrl;
        protected readonly string URL_NOTIFICATION_MANAGEMENT_DOMAIN = ApiUrls.NotificationManagementUrl;
        #endregion

        #region Get
        protected virtual IRestResponse SubmitGet(string url, string origin, IParameter parameter, string authorization = "", string tenantUid = "", string locale = "")
        {
            var client = SetupClient(url, origin, parameter, out RestRequest request, authorization, Method.GET, tenantUid, locale);
            return client.Execute(request);
        }

        protected virtual async Task<IRestResponse> SubmitGetAsync(string url, string origin, IParameter parameter, string authorization = "", string tenantUid = "", ILogger logger = null, string locale = "")
        {
            var client = SetupClient(url, origin, parameter, out RestRequest request, authorization, Method.GET, tenantUid, locale);

            if (logger != null)
            {
                var dict = new Dictionary<string, string>
                {
                    { "URL", url },
                    { "Method", "GET" }
                };

                foreach (var p in request.Parameters)
                {
                    if (!dict.ContainsKey(p.Name))
                    {
                        dict.Add($"{p.Type.ToString()} - {p.Name}", p.Value.ToString());
                    }
                }

                //logger.Info<BaseService>(JsonConvert.SerializeObject(dict));
            }

            return await client.ExecuteTaskAsync(request);
        }
        protected virtual async Task<IRestResponse> SubmitGetAsync(string url, string origin, string authorization, string tenantUid = "")
        {
            var client = SetupGetClient(url, origin, out RestRequest request, authorization, tenantUid);
            return await client.ExecuteTaskAsync(request);
        }
        protected virtual IRestResponse SubmitGet(string url, string origin, string authorization, string tenantUid = "")
        {
            var client = SetupGetClient(url, origin, out RestRequest request, authorization, tenantUid);
            return client.Execute(request);
        }
        #endregion

        #region Post
        protected virtual IRestResponse SubmitPost(string url, string origin, IParameter parameter, string authorization = "", string tenantUid = "", string locale = "", string returnUrl = "")
        {
            var client = SetupClient(url, origin, parameter, out RestRequest request, authorization, Method.POST, tenantUid, locale, returnUrl);
            return client.Execute(request);
        }

        protected virtual async Task<IRestResponse> SubmitPostAsync(string url, string origin, IParameter parameter, string authorization = "", string tenantUid = "", ILogger logger = null, string locale = "", string returnUrl = "")
        {
            var client = SetupClient(url, origin, parameter, out RestRequest request, authorization, Method.POST, tenantUid, locale, returnUrl);

            if (logger != null)
            {
                var dict = new Dictionary<string, string>
                {
                    { "URL", url },
                    { "Method", "GET" }
                };

                foreach (var p in request.Parameters)
                {
                    if (!dict.ContainsKey(p.Name))
                    {
                        dict.Add($"{p.Type.ToString()} - {p.Name}", p.Value.ToString());
                    }
                }

                //logger.Info<BaseService>(JsonConvert.SerializeObject(dict));
            }

            return await client.ExecuteTaskAsync(request);
        }

        protected async Task<IRestResponse> SubmitPostAsync(string url, string token, string origin, HttpPostedFileBase file, string tenantUid = "")
        {
            var client = new RestClientExtended(url)
            {
                Timeout = TIMEOUT
            };
            var request = new RestRequest(Method.POST);

            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("X-Forwarded-For", GetIpAddress());
            request.AddHeader("User-Agent", HttpContext.Current.Request.UserAgent);

            if (!string.IsNullOrEmpty(origin))
            {
                request.AddHeader("Origin", origin);
            }

            if (!string.IsNullOrEmpty(tenantUid))
            {
                request.AddHeader("Account-context", tenantUid);
            }

            if (!string.IsNullOrEmpty(token))
            {
                request.AddHeader("Authorization", $"Bearer {token}");
            }

            request.Files.Add(new FileParameter
            {
                Name = "file",
                Writer = (s) =>
                {
                    var stream = file.InputStream;
                    stream.CopyTo(s);
                    stream.Dispose();
                },
                FileName = file.FileName,
                ContentType = file.ContentType,
                ContentLength = file.ContentLength
            });

            request.AddParameter("multipart/form-data", file.FileName, ParameterType.RequestBody);

            return await client.ExecuteTaskAsync(request);
        }

        protected async Task<IRestResponse> SubmitPostAsync<T>(string url, string token, string origin, JsonRpcFormat<T> payload, string tenantUid = "")
        {
            var client = new RestClientExtended(url)
            {
                Timeout = TIMEOUT
            };
            var request = new RestRequest(Method.POST);
            var param = JsonConvert.SerializeObject(payload);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Origin", origin);
            request.AddHeader("X-Forwarded-For", GetIpAddress());

            if (!string.IsNullOrEmpty(tenantUid))
            {
                request.AddHeader("Account-context", tenantUid);
            }

            if (!string.IsNullOrEmpty(token))
            {
                request.AddHeader("Authorization", $"Bearer {token}");
            }

            request.AddParameter("body", param, ParameterType.RequestBody);

            return await client.ExecuteTaskAsync(request);
        }
        #endregion

        #region Response
        protected IResponseContent AssertResponseContent<T>(IRestResponse response) where T : IResponseContent
        {
            if (VERBOSE)
            {
                var logger = ConnectorContext.Logger;
                logger.Debug<BaseService>($"Api Response: {JsonConvert.SerializeObject(response)}");
            }

            if (response.ContentType.Equals("application/json; charset=utf-8"))
            {
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<T>(response.Content);
                }
                else
                {
                    return JsonConvert.DeserializeObject<T>(response.Content);
                }
            }
            else
            {
                var instance = Activator.CreateInstance<T>();
                instance.Message = response.Content;
                instance.Exception = new Exception(response.Content, response.ErrorMessage != null ? new Exception(response.ErrorMessage, response.ErrorException) : null);

                return instance;
            }
        }

        protected async Task<IResponseContent> AssertResponseContentAsync<T>(IRestResponse response) where T : IResponseContent
        {
            return await Task.FromResult(AssertResponseContent<T>(response));
        }

        protected async Task<GameDetails[]> GameDataAnonymousArrayResponseContentAsync(IRestResponse response)
        {
            var definition = new GameDetails[] { new GameDetails() };
            return await Task.FromResult(JsonConvert.DeserializeAnonymousType(response.Content, definition));
        }

        protected GameDetails[] GameDataAnonymousArrayResponseContent(IRestResponse response)
        {
            var definition = new GameDetails[] { new GameDetails() };
            return JsonConvert.DeserializeAnonymousType(response.Content, definition);
        }
        #endregion

        #region Setup Http Request
        private RestClientExtended SetupClient(string url, string origin, IParameter parameter, out RestRequest request, string authorization = "", RestSharp.Method method = Method.POST, string tenantUid = "", string locale = "", string returnUrl = "")
        {
           

            string param = string.Empty;

            if (parameter != null)
            {
                param = JsonConvert.SerializeObject(parameter); //serialize before, to get length
            }

            var client = new RestClientExtended(url)
            {
                Timeout = TIMEOUT
            };
            request = new RestRequest(method);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Host", new Uri(url).Host);
            request.AddHeader("Umbraco-CMS-Token", $"{Guid.NewGuid()},{Guid.NewGuid()}");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("User-Agent", HttpContext.Current.Request.UserAgent);
            request.AddHeader("X-Forwarded-For", GetIpAddress());

            if (method == Method.POST)
            {
                // if (string.IsNullOrEmpty(param)) throw new Exception("When Posting, parameters are required");

                request.AddHeader("content-length", param.Length.ToString());
                request.AddHeader("accept-encoding", "gzip, deflate");
                request.AddHeader("Content-Type", "application/json");
            }

            if (!string.IsNullOrEmpty(tenantUid))
            {
                request.AddHeader("Account-context", tenantUid);
            }

            if (!string.IsNullOrEmpty(returnUrl))
            {
                request.AddHeader("ReturnUrl", returnUrl);
                //request.AddHeader("ReturnUrl", ContentHelper.EncodeUrl(returnUrl));                
            }

            if (!string.IsNullOrEmpty(locale))
            {
                request.AddHeader("Locale", locale);
            }

            if (method == Method.POST && parameter != null)
            {
                request.AddParameter("body", param, ParameterType.RequestBody);
            }

            if (!string.IsNullOrEmpty(origin))
                request.AddHeader("Origin", origin);

            if (!string.IsNullOrEmpty(authorization))
                request.AddHeader("Authorization", $"Bearer {authorization}");

            return client;
        }
        private RestClientExtended SetupGetClient(string url, string origin, out RestRequest request, string authorization, string tenantUid = "")
        {
            return SetupClient(url, origin, null, out request, authorization, Method.GET, tenantUid);
        }
        #endregion

        #region JWT
        protected JwtCustomerDataResponseContent DecodeJwt(string jwtToken)
        {
            var token = new JwtSecurityToken(jwtToken);
            var response = JsonConvert.DeserializeObject<JwtCustomerDataResponseContent>(token.Claims.First(c => c.Type == "CustomerData").Value);
            return response;
        }

        protected async Task<JwtCustomerDataResponseContent> DecodeJwtAsync(string jwtToken)
        {
            return await Task.FromResult(DecodeJwt(jwtToken));
        }

        protected string GetIpAddress()
        {
            try
            {
                if (HttpContext.Current.Request.Headers["CF-CONNECTING-IP"] != null)
                    return HttpContext.Current.Request.Headers["CF-CONNECTING-IP"].ToString();
                else if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                    return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                else if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)
                    return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                else
                    return HttpContext.Current.Request.UserHostAddress;
            }
            catch
            {
                return "";
            }
        }
        #endregion
    }
}
