namespace Umbraco.Plugins.Connector.Controllers
{
    using AustinHarris.JsonRpc;
    using System.Web.Http;
    using Umbraco.Plugins.Connector.Filters;
    using Umbraco.Plugins.Connector.Interfaces;
    using Umbraco.Plugins.Connector.Models;
    using Umbraco.Plugins.Connector.Services;
    using Umbraco.Web.WebApi;

    [RoutePrefix("ApiConnector")]
    public class ExternalApiConnectorController : UmbracoApiController, IApiConnector
    {
        [Route("AddTenantDomain")]
        [ApiKeyAuthentication]
        public TenantDomains AddTenantDomain(TenantDomain payload)
        {
            return new ControllerService().AddTenantDomain(payload);
        }

        [Route("AssignUserGroups")]
        [ApiKeyAuthentication]
        public string AssignUserGroups(string[] groups, string username)
        {
            return new ControllerService().AssignUserGroups(groups, username);
        }

        [Route("ChangePassword")]
        [ApiKeyAuthentication]
        public string ChangePassword([FromBody] TenantUser payload)
        {
            return new ControllerService().ChangePassword(payload);
        }

        [Route("CreateGroup")]
        [ApiKeyAuthentication]
        public string CreateGroup([FromBody] TenantGroup payload)
        {
            return new ControllerService().CreateGroup(payload);
        }

        [Route("CreateTenant")]
        [JsonRpcMethod]
        [ApiKeyAuthentication]
        public ExtendedTenant CreateTenant([FromBody] Tenant tenant, string tenantToBeCopied = "")
        {
            return new ControllerService().CreateTenant(tenant, tenantToBeCopied);
        }

        [Route("CreateUser")]
        [ApiKeyAuthentication]
        public string CreateUser([FromBody] TenantUser payload)
        {
            return new ControllerService().CreateUser(payload);
        }

        [Route("DisableTenant")]
        [ApiKeyAuthentication]
        public string DisableTenant([FromBody] SimpleTenant payload)
        {
            return new ControllerService().DisableTenant(payload);
        }

        [Route("DisableUser")]
        [ApiKeyAuthentication]
        public string DisableUser([FromBody] SimpleTenant payload)
        {
            return new ControllerService().DisableUser(payload);
        }

        public ExtendedTenant EditTenant(TenantData tenant, TenantUser user = null, TenantGroup group = null)
        {
            throw new System.Exception("not used in non-Json-rpc procedure");
        }

        [Route("EditTenantWrap")]
        [ApiKeyAuthentication]
        public ExtendedTenant EditTenantWrap([FromBody] TenantEdit edit)
        {
            return new ControllerService().EditTenant(edit.Tenant, edit.User, edit.Group);
        }

        [Route("EnableTenant")]
        [ApiKeyAuthentication]
        public string EnableTenant([FromBody] SimpleTenant payload)
        {
            return new ControllerService().EnableTenant(payload);
        }

        [Route("EnableUser")]
        [ApiKeyAuthentication]
        public string EnableUser([FromBody] SimpleTenant payload)
        {
            return new ControllerService().EnableUser(payload);
        }

        [Route("LoginTenant")]
        [ApiKeyAuthentication]
        public void LoginTenant([FromBody] SimpleTenant payload)
        {
            var user = Services.UserService.GetByUsername(payload.Username);
            UmbracoContext.Security.PerformLogin(user.Id);
        }

        public void LoginTenantRemotely(SimpleTenant payload)
        {
            var user = Services.UserService.GetByUsername(payload.Username);
            UmbracoContext.Security.PerformLogin(user.Id);
        }

        public int RefreshAllTenants(string language = "en")
        {
            return new ControllerService().RefreshAllTenants(language);
        }

        public bool RefreshTenant(string tenantUid, string tenantName, string languageCode = "en")
        {
            return new ControllerService().RefreshTenant(tenantUid, tenantName, languageCode);
        }

        [Route("RemoveTenantDomain")]
        [ApiKeyAuthentication]
        public TenantDomains RemoveTenantDomain(TenantDomain payload)
        {
            return new ControllerService().RemoveTenantDomain(payload);
        }

        [Route("ResetPassword")]
        [ApiKeyAuthentication]
        public string ResetPassword([FromBody] SimpleTenant payload)
        {
            return new ControllerService().ResetPassword(payload);
        }
    }
}
