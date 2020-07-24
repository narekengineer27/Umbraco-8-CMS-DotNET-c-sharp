#pragma warning disable RCS1093 // Remove file with no code.
#pragma warning restore RCS1093 // Remove file with no code.
                               //namespace Umbraco.Plugins.Connector
                               //{
                               //    using Umbraco.Core.Migrations;

//    public class NoAppIdMigration : MigrationBase
//    {
//        private const string tableName = "pluginApiKeys";

//        public NoAppIdMigration(IMigrationContext context) : base(context)
//        {
//        }

//        public override void Migrate()
//        {
//            Logger.Debug(typeof(InitialMigration), "Removing App Id");

//            if (ColumnExists(tableName, "AppId"))
//            {
//                Execute.Sql($"ALTER TABLE {tableName} ALTER COLUMN AppId nvarchar(255) NULL").Do();
//                Execute.Sql($"DELETE FROM {tableName}").Do();
//            }
//        }
//    }
//}

