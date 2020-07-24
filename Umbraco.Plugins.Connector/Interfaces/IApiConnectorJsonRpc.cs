namespace Umbraco.Plugins.Connector.Interfaces
{
    using Umbraco.Plugins.Connector.Models;

    public interface IApiConnectorJsonRpc
    {
        JsonRpcResponseData AssignUserGroups(string[] groups, string username, ApiAuthorization authorization);
        JsonRpcResponseData ChangePassword(TenantUser payload, ApiAuthorization authorization);
        JsonRpcResponseData CreateTenant(Tenant tenant, ApiAuthorization authorization, string tenantToBeCopied = "");
        JsonRpcResponseData CreateUser(TenantUser payload, ApiAuthorization authorization);
        JsonRpcResponseData CreateGroup(TenantGroup payload, ApiAuthorization authorization);
        JsonRpcResponseData DisableTenant(SimpleTenant payload, ApiAuthorization authorization);
        JsonRpcResponseData DisableUser(SimpleTenant payload, ApiAuthorization authorization);
        JsonRpcResponseData EnableUser(SimpleTenant payload, ApiAuthorization authorization);
        JsonRpcResponseData EditTenant(TenantData tenant, ApiAuthorization authorization, TenantUser user = null, TenantGroup group = null);
        JsonRpcResponseData EnableTenant(SimpleTenant payload, ApiAuthorization authorization);
        JsonRpcResponseData LoginTenant(SimpleTenant payload, ApiAuthorization authorization);
        JsonRpcResponseData PurgeTenant(SimpleTenant payload, ApiAuthorization authorization);
        JsonRpcResponseData ResetPassword(SimpleTenant payload, ApiAuthorization authorization);
        JsonRpcResponseData AddTenantDomain(TenantDomain payload, ApiAuthorization authorization);
        JsonRpcResponseData RemoveTenantDomain(TenantDomain payload, ApiAuthorization authorization);
        JsonRpcResponseData ReassignDomains(string tenantUid, ApiAuthorization authorization);
        JsonRpcResponseData RefreshTenant(string tenantUid, string tenantName, ApiAuthorization authorization, string languageCode = "en");
        JsonRpcResponseData RefreshAllTenants(ApiAuthorization authorization, string language = "en");
        JsonRpcResponseData ClearAllApiCache(ApiAuthorization authorization);
        JsonRpcResponseData ClearApiCache(string tenantUid, string cacheName, ApiAuthorization authorization);
        JsonRpcResponseData GetAllApiCache(ApiAuthorization authorization);
        JsonRpcResponseData GetApiCache(string tenantUid, string cacheName, ApiAuthorization authorization);
    }
}
