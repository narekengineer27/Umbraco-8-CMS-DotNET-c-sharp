namespace Umbraco.Plugins.Connector.Interfaces
{
    using Umbraco.Plugins.Connector.Models;

    public interface IApiConnector
    {
        string AssignUserGroups(string[] groups, string username);
        string ChangePassword(TenantUser payload);
        ExtendedTenant CreateTenant(Tenant tenant, string tenantToBeCopied = "");
        string CreateUser(TenantUser payload);
        string CreateGroup(TenantGroup payload);
        string DisableTenant(SimpleTenant payload);
        string DisableUser(SimpleTenant payload);
        string EnableUser(SimpleTenant payload);
        ExtendedTenant EditTenant(TenantData tenant, TenantUser user = null, TenantGroup group = null);
        ExtendedTenant EditTenantWrap(TenantEdit edit);
        string EnableTenant(SimpleTenant payload);
        void LoginTenant(SimpleTenant payload);
        string ResetPassword(SimpleTenant payload);
        TenantDomains AddTenantDomain(TenantDomain payload);
        TenantDomains RemoveTenantDomain(TenantDomain payload);
        bool RefreshTenant(string tenantUid, string tenantName, string languageCode = "en");
        int RefreshAllTenants(string language = "en");
    }
}
