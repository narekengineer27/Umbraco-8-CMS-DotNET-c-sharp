namespace Umbraco.Plugins.Connector
{
    using Umbraco.Core.Migrations;
    using Umbraco.Plugins.Connector.Models;

    public class InitialMigration : MigrationBase
    {
        private const string tableName = "pluginApiKeys";

        public InitialMigration(IMigrationContext context) : base(context)
        {
        }

        public override void Migrate()
        {
            Logger.Debug(typeof(InitialMigration), "Running Initial Migration with Api Keys");

            if (!this.TableExists(tableName))
            {
                this.Create.Table<ApiKeys>().Do();
            }
        }
    }


}
