namespace Umbraco.Plugins.Connector.ConnectorServices
{
    using System;
    using Umbraco.Core.Persistence;
    using Umbraco.Plugins.Connector.Exceptions;
    using Umbraco.Plugins.Connector.Models;

    public class ApiKeysService : DbService<ApiKeys>
    {
        private readonly IUmbracoDatabase database;

        public ApiKeysService(IUmbracoDatabase database) : base(database, ApiKeys.TABLENAME)
        {
            this.database = database;
        }

        public void Save(ExtendedTenant tenant)
        {
            database.Execute($"INSERT INTO {ApiKeys.TABLENAME} (UserId, AppId, ApiKey, CreatedOn, TenantId) VALUES ({tenant.UserId}, '{tenant.Tenant.AppId}', '{tenant.Tenant.ApiKey}', '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}','{tenant.Tenant.TenantUId}')");
        }

        public void PurgeTenant(string tenantUid)
        {
            database.Execute($"DELETE FROM {ApiKeys.TABLENAME} WHERE [TenantId] = '{tenantUid}'");
        }

        public void Validate(Tenant tenant)
        {
            if (string.IsNullOrEmpty(tenant.AppId) || string.IsNullOrEmpty(tenant.ApiKey))
            {
                throw new TenantException(ExceptionCode.TenantApiIncorrect.CodeToString(), ExceptionCode.TenantApiIncorrect, tenant.TenantUId);
            }
        }

        public void UpdateApiForTenant(ExtendedTenant tenant)
        {
            database.Execute($"UPDATE {ApiKeys.TABLENAME} SET UserId = {tenant.UserId}, AppId = '{tenant.Tenant.AppId}', ApiKey = '{tenant.Tenant.ApiKey}' WHERE TenantId ='{tenant.Tenant.TenantUId}'");
        }
    }
}
