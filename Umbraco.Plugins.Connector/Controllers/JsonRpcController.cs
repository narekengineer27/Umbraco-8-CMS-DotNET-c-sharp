namespace Umbraco.Plugins.Connector.Controllers
{
    using AustinHarris.JsonRpc;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using Umbraco.Plugins.Connector.Cache;
    using Umbraco.Plugins.Connector.Exceptions;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Interfaces;
    using Umbraco.Plugins.Connector.Models;
    using Umbraco.Plugins.Connector.Services;

    public class JsonRpcController : JsonRpcService, IApiConnectorJsonRpc
    {
        private static readonly Dictionary<string, string> allowedApps = new Dictionary<string, string>();

        [JsonRpcMethod]
        public JsonRpcResponseData AssignUserGroups(string[] groups, string username, ApiAuthorization authorization)
        {
            SetupAuth();
            IsValidRequest(authorization.AppId, authorization.ApiKey);
            var apiController = new ControllerService();
            try
            {
                var assignedGroups = apiController.AssignUserGroups(groups, username);
                return new JsonRpcResponseData
                {
                    Message = "Groups assigned to user",
                    Status = JsonRpcResponseData.OK,
                    Data = new
                    {
                        Username = username,
                        assignedGroups
                    }
                };
            }
            catch (System.Exception ex)
            {
                throw HandleException(ex);
            }
        }

        [JsonRpcMethod]
        public JsonRpcResponseData ChangePassword(TenantUser payload, ApiAuthorization authorization)
        {
            SetupAuth();
            IsValidRequest(authorization.AppId, authorization.ApiKey);
            var apiController = new ControllerService();
            try
            {
                var username = apiController.ChangePassword(payload);
                return new JsonRpcResponseData
                {
                    Message = $"Password for {username} changed",
                    Status = JsonRpcResponseData.OK,
                    Data = new
                    {
                        Username = username,
                        payload.Password
                    }
                };
            }
            catch (System.Exception ex)
            {
                throw HandleException(ex);
            }
        }

        [JsonRpcMethod]
        public JsonRpcResponseData CreateGroup(TenantGroup payload, ApiAuthorization authorization)
        {
            SetupAuth();
            IsValidRequest(authorization.AppId, authorization.ApiKey);
            var apiController = new ControllerService();
            try
            {
                var groupId = apiController.CreateGroup(payload);
                return new JsonRpcResponseData
                {
                    Message = $"Group {payload.Name} Created",
                    Status = JsonRpcResponseData.OK,
                    TenantUid = payload.TenantUid.ToString(),
                    Data = new
                    {
                        groupId
                    }
                };
            }
            catch (System.Exception ex)
            {
                throw HandleException(ex);
            }
        }

        [JsonRpcMethod]
        public JsonRpcResponseData CreateTenant(Tenant tenant, ApiAuthorization authorization, string tenantToBeCopied = "")
        {
            SetupAuth();
            IsValidRequest(authorization.AppId, authorization.ApiKey);
            var apiController = new ControllerService();
            try
            {
                var extended = apiController.CreateTenant(tenant, tenantToBeCopied);

                return new JsonRpcResponseData
                {
                    Message = "Tenant Created",
                    Status = JsonRpcResponseData.OK,
                    TenantUid = extended.Tenant.TenantUId.ToString(),
                    Data = new
                    {
                        extended
                    }
                };
            }
            catch (System.Exception ex)
            {
                throw HandleException(ex);
            }
        }

        [JsonRpcMethod]
        public JsonRpcResponseData CreateUser(TenantUser payload, ApiAuthorization authorization)
        {
            SetupAuth();
            IsValidRequest(authorization.AppId, authorization.ApiKey);
            var apiController = new ControllerService();
            try
            {
                var assignedUmbracoUserId = apiController.CreateUser(payload);
                return new JsonRpcResponseData
                {
                    Message = "User created",
                    Status = JsonRpcResponseData.OK,
                    TenantUid = payload.TenantUId.ToString(),
                    Data = assignedUmbracoUserId
                };
            }
            catch (System.Exception ex)
            {
                throw HandleException(ex);
            }
        }

        [JsonRpcMethod]
        public JsonRpcResponseData DisableTenant(SimpleTenant payload, ApiAuthorization authorization)
        {
            SetupAuth();
            IsValidRequest(authorization.AppId, authorization.ApiKey);
            var apiController = new ControllerService();
            try
            {
                var tenantUid = apiController.DisableTenant(payload);
                return new JsonRpcResponseData
                {
                    Message = "Tenant disabled",
                    Status = JsonRpcResponseData.OK,
                    TenantUid = tenantUid
                };
            }
            catch (System.Exception ex)
            {
                throw HandleException(ex);
            }
        }

        [JsonRpcMethod]
        public JsonRpcResponseData DisableUser(SimpleTenant payload, ApiAuthorization authorization)
        {
            SetupAuth();
            IsValidRequest(authorization.AppId, authorization.ApiKey);
            var apiController = new ControllerService();
            try
            {
                apiController.DisableUser(payload);
                return new JsonRpcResponseData
                {
                    Message = "User disabled",
                    Status = JsonRpcResponseData.OK,
                    Data = new { payload.Username }
                };
            }
            catch (System.Exception ex)
            {
                throw HandleException(ex);
            }
        }

        [JsonRpcMethod]
        public JsonRpcResponseData EditTenant(TenantData tenant, ApiAuthorization authorization, TenantUser user = null, TenantGroup group = null)
        {
            SetupAuth();
            IsValidRequest(authorization.AppId, authorization.ApiKey);
            var apiController = new ControllerService();
            try
            {
                var extended = apiController.EditTenant(tenant, user, group);
                return new JsonRpcResponseData
                {
                    Message = "Tenant edited",
                    Status = JsonRpcResponseData.OK,
                    TenantUid = extended.Tenant.TenantUId.ToString(),
                    Data = new
                    {
                        extended
                    }
                };
            }
            catch (System.Exception ex)
            {
                throw HandleException(ex);
            }
        }

        [JsonRpcMethod]
        public JsonRpcResponseData EnableTenant(SimpleTenant payload, ApiAuthorization authorization)
        {
            SetupAuth();
            IsValidRequest(authorization.AppId, authorization.ApiKey);
            var apiController = new ControllerService();
            try
            {
                var tenantUid = apiController.EnableTenant(payload);
                return new JsonRpcResponseData
                {
                    Message = "Tenant enabled",
                    Status = JsonRpcResponseData.OK,
                    TenantUid = tenantUid
                };
            }
            catch (System.Exception ex)
            {
                throw HandleException(ex);
            }
        }

        [JsonRpcMethod]
        public JsonRpcResponseData EnableUser(SimpleTenant payload, ApiAuthorization authorization)
        {
            SetupAuth();
            IsValidRequest(authorization.AppId, authorization.ApiKey);
            var apiController = new ControllerService();
            try
            {
                var newPassword = apiController.EnableUser(payload);
                return new JsonRpcResponseData
                {
                    Message = $"User {payload.Username} Enabled",
                    Status = JsonRpcResponseData.OK,
                    TenantUid = payload.TenantUId
                };
            }
            catch (System.Exception ex)
            {
                throw HandleException(ex);
            }
        }

        [JsonRpcMethod]
        public JsonRpcResponseData LoginTenant(SimpleTenant payload, ApiAuthorization authorization)
        {
            SetupAuth();
            IsValidRequest(authorization.AppId, authorization.ApiKey);
            var apiController = new ControllerService();
            try
            {
                apiController.LoginTenant(payload);
                return new JsonRpcResponseData
                {
                    Message = $"Tenant Logged in",
                    Status = JsonRpcResponseData.OK,
                    TenantUid = payload.TenantUId,
                    Data = new
                    {
                        payload.Username
                    }
                };
            }
            catch (System.Exception ex)
            {
                throw HandleException(ex);
            }
        }

        [JsonRpcMethod]
        public JsonRpcResponseData PurgeTenant(SimpleTenant payload, ApiAuthorization authorization)
        {
            SetupAuth();
            IsValidRequest(authorization.AppId, authorization.ApiKey);
            var apiController = new ControllerService();
            try
            {
                var purge = apiController.PurgeTenant(payload);
                return new JsonRpcResponseData
                {
                    Message = "Tenant Purged from CMS",
                    Status = JsonRpcResponseData.OK,
                    TenantUid = payload.TenantUId,
                    Data = purge
                };
            }
            catch (System.Exception ex)
            {
                throw HandleException(ex);
            }
        }

        [JsonRpcMethod]
        public JsonRpcResponseData ResetPassword(SimpleTenant payload, ApiAuthorization authorization)
        {
            SetupAuth();
            IsValidRequest(authorization.AppId, authorization.ApiKey);
            var apiController = new ControllerService();
            try
            {
                var newPassword = apiController.ResetPassword(payload);
                return new JsonRpcResponseData
                {
                    Message = $"Password for {payload.Username} Reset",
                    Status = JsonRpcResponseData.OK,
                    TenantUid = payload.TenantUId,
                    Data = new
                    {
                        newPassword
                    }
                };
            }
            catch (System.Exception ex)
            {
                throw HandleException(ex);
            }
        }

        [JsonRpcMethod]
        public JsonRpcResponseData AddTenantDomain(TenantDomain payload, ApiAuthorization authorization)
        {
            SetupAuth();
            IsValidRequest(authorization.AppId, authorization.ApiKey);
            var apiController = new ControllerService();

            try
            {
                var tenantDomains = apiController.AddTenantDomain(payload);
                return new JsonRpcResponseData
                {
                    Message = $"Domain {payload.Domain} for Tenant {payload.TenantUid} has been added",
                    Status = JsonRpcResponseData.OK,
                    TenantUid = payload.TenantUid,
                    Data = new
                    {
                        tenantDomains
                    }
                };
            }
            catch (System.Exception ex)
            {
                throw HandleException(ex);
            }
        }

        [JsonRpcMethod]
        public JsonRpcResponseData RemoveTenantDomain(TenantDomain payload, ApiAuthorization authorization)
        {
            SetupAuth();
            IsValidRequest(authorization.AppId, authorization.ApiKey);
            var apiController = new ControllerService();

            try
            {
                var tenantDomains = apiController.RemoveTenantDomain(payload);
                return new JsonRpcResponseData
                {
                    Message = $"Domain {payload.Domain} for Tenant {payload.TenantUid} has been removed",
                    Status = JsonRpcResponseData.OK,
                    TenantUid = payload.TenantUid,
                    Data = new
                    {
                        tenantDomains
                    }
                };
            }
            catch (System.Exception ex)
            {
                throw HandleException(ex);
            }
        }

        [JsonRpcMethod]
        public JsonRpcResponseData ReassignDomains(string tenantUid, ApiAuthorization authorization)
        {
            SetupAuth();
            IsValidRequest(authorization.AppId, authorization.ApiKey);
            var apiController = new ControllerService();

            try
            {
                var extended = apiController.ReassignCulturesAndHostnames(tenantUid);
                var domainList = new List<string>
                {
                    extended.Tenant.Domain
                };
                foreach (var domain in extended.Tenant.AlternateDomains)
                    domainList.Add(domain);

                var languageList = new List<string>
                {
                    extended.Tenant.Languages.Default
                };
                foreach (var lang in extended.Tenant.Languages.Alternate)
                    languageList.Add(lang);

                return new JsonRpcResponseData
                {
                    Message = $"Domains for Tenant {tenantUid} have been reassigned",
                    Status = JsonRpcResponseData.OK,
                    TenantUid = tenantUid,
                    Data = new
                    {
                        Domains = domainList.ToArray(),
                        Languages = languageList.ToArray()
                    }
                };
            }
            catch (System.Exception ex)
            {
                throw HandleException(ex);
            }
        }

        [JsonRpcMethod]
        public JsonRpcResponseData RefreshTenant(string tenantUid, string tenantName, ApiAuthorization authorization, string languageCode = "en")
        {
            SetupAuth();
            IsValidRequest(authorization.AppId, authorization.ApiKey);
            var apiController = new ControllerService();

            try
            {
                var isRefreshed = apiController.RefreshTenant(tenantUid, tenantName, languageCode);

                if (isRefreshed)
                {
                    return new JsonRpcResponseData
                    {
                        Message = $"Tenant ({tenantUid}) has been refreshed",
                        Status = JsonRpcResponseData.OK,
                        TenantUid = tenantUid
                    };
                }
                else
                {
                    return new JsonRpcResponseData
                    {
                        Message = $"Could not refresh tenant ({tenantUid}) - Critical Error",
                        Status = JsonRpcResponseData.ERROR,
                        TenantUid = tenantUid
                    };
                }
            }
            catch (System.Exception ex)
            {
                throw HandleException(ex);
            }
        }

        [JsonRpcMethod]
        public JsonRpcResponseData RefreshAllTenants(ApiAuthorization authorization, string language = "en")
        {
            SetupAuth();
            IsValidRequest(authorization.AppId, authorization.ApiKey);
            var apiController = new ControllerService();

            try
            {
                int refreshed = apiController.RefreshAllTenants(language);

                return new JsonRpcResponseData
                {
                    Message = $"All Tenants have been refreshed",
                    Status = JsonRpcResponseData.OK,
                    Data = new { Refreshed = refreshed }
                };
            }
            catch (System.Exception ex)
            {
                throw HandleException(ex);
            }
        }

        [JsonRpcMethod]
        private string Ping(string message)
        {
            return $"Ping received: {message}";
        }

        [JsonRpcMethod]
        public JsonRpcResponseData ClearAllApiCache(ApiAuthorization authorization)
        {
            SetupAuth();
            IsValidRequest(authorization.AppId, authorization.ApiKey);
            try
            {
                CacheHelper.ClearAllCache();
                return new JsonRpcResponseData
                {
                    Message = $"All Api Cache has been cleared",
                    Status = JsonRpcResponseData.OK,
                };
            }
            catch (Exception ex)
            {
                return new JsonRpcResponseData
                {
                    Message = ex.Message,
                    Status = JsonRpcResponseData.ERROR,
                };
            }
        }

        [JsonRpcMethod]
        public JsonRpcResponseData ClearApiCache(string tenantUid, string cacheName, ApiAuthorization authorization)
        {
            SetupAuth();
            IsValidRequest(authorization.AppId, authorization.ApiKey);
            try
            {
                var cacheItems = CacheHelper.GetAllCacheItems.Where(p => p.TenantUid == tenantUid);

                if (!string.IsNullOrEmpty(cacheName))
                {
                    cacheItems = cacheItems.Where(p => p.CacheName == cacheName);
                }

                foreach (var item in cacheItems)
                {
                    CacheHelper.ClearCache(item);
                }

                return new JsonRpcResponseData
                {
                    Message = $"Api Cache has been cleared",
                    TenantUid = tenantUid,
                    Status = JsonRpcResponseData.OK,
                };
            }
            catch (Exception ex)
            {
                return new JsonRpcResponseData
                {
                    Message = ex.Message,
                    Status = JsonRpcResponseData.ERROR,
                };
            }
        }

        [JsonRpcMethod]
        public JsonRpcResponseData GetAllApiCache(ApiAuthorization authorization)
        {
            SetupAuth();
            IsValidRequest(authorization.AppId, authorization.ApiKey);
            try
            {
                return new JsonRpcResponseData
                {
                    Message = $"Successfully fetched Api Cache",
                    Status = JsonRpcResponseData.OK,
                    Data = new { CacheHelper.GetAllCacheItems }
                };
            }
            catch (Exception ex)
            {
                return new JsonRpcResponseData
                {
                    Message = ex.Message,
                    Status = JsonRpcResponseData.ERROR,
                };
            }
        }

        [JsonRpcMethod]
        public JsonRpcResponseData GetApiCache(string tenantUid, string cacheName, ApiAuthorization authorization)
        {
            SetupAuth();
            IsValidRequest(authorization.AppId, authorization.ApiKey);
            try
            {
                var cacheItems = CacheHelper.GetAllCacheItems.Where(p => p.TenantUid == tenantUid);

                if (!string.IsNullOrEmpty(cacheName))
                {
                    cacheItems = cacheItems.Where(p => p.CacheName == cacheName);
                }

                return new JsonRpcResponseData
                {
                    Message = $"Successfully fetched Api Cache",
                    Status = JsonRpcResponseData.OK,
                    TenantUid = tenantUid,
                    Data = new { cacheItems }
                };
            }
            catch (Exception ex)
            {
                return new JsonRpcResponseData
                {
                    Message = ex.Message,
                    Status = JsonRpcResponseData.ERROR,
                };
            }
        }

        #region Authentication Helpers
        private void IsValidRequest(string appId, string apiKey)
        {
            var sharedKey = allowedApps[appId];
            if (!apiKey.Equals(sharedKey))
            {
                throw new JsonRpcException(ExceptionCode.NotAuthorized.GetHashCode(), "Not authenticated", new { appId, apiKey });
            }
        }

        private void SetupAuth()
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
            // add Total Code Admin App Id and Api Key
            if (!allowedApps.ContainsKey(ConfigurationManager.AppSettings["TotalCode.Admin.AppId"]))
                allowedApps.Add(ConfigurationManager.AppSettings["TotalCode.Admin.AppId"], ConfigurationManager.AppSettings["TotalCode.Admin.ApiKey"]);
        }
        #endregion

        #region Exception Handling
        private JsonRpcException HandleException(System.Exception ex)
        {
            if (ex.GetType().Equals(typeof(TenantException)))
            {
                var exception = (TenantException)ex;
                ConnectorContext.Logger.Error(typeof(JsonRpcController), ex.Message);
                ConnectorContext.Logger.Error(typeof(JsonRpcController), ex.StackTrace);
                return new JsonRpcException(exception.Code.GetHashCode(), exception.Message, new { exception.TenantUid, exception.Info });
            }
            else
            {
                ConnectorContext.Logger.Error(typeof(JsonRpcController), ex.Message);
                ConnectorContext.Logger.Error(typeof(JsonRpcController), ex.StackTrace);
                return new JsonRpcException(ExceptionCode.Unhandled.GetHashCode(), ex.Message, new { ex.InnerException, ex.StackTrace });
            }
        }
        #endregion
    }
}
