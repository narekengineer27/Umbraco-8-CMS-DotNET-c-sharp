namespace Umbraco.Plugins.Connector.Migrations
{
    using Umbraco.Core;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Scoping;
    using Umbraco.Core.Services;
    using Umbraco.Core.Composing;
    using Umbraco.Core.Migrations;
    using Umbraco.Core.Migrations.Upgrade;
    public class MigrationUpgradeComponentComposer : ComponentComposer<MigrationUpgradeComponent>
    {
        public override void Compose(Composition composition)
        {
            composition.Components().Append<MigrationUpgradeComponent>();
        }
    }

    public class MigrationUpgradeComponent : IComponent
    {
        private readonly IScopeProvider scopeProvider;
        private readonly IMigrationBuilder migrationBuilder;
        private readonly IKeyValueService keyValueService;
        private readonly ILogger logger;

        public MigrationUpgradeComponent(IScopeProvider scopeProvider, IMigrationBuilder migrationBuilder, IKeyValueService keyValueService, ILogger logger)
        {
            this.scopeProvider = scopeProvider;
            this.migrationBuilder = migrationBuilder;
            this.keyValueService = keyValueService;
            this.logger = logger;
            ConnectorContext.ScopeProvider = scopeProvider;
        }

        public void Initialize()
        {
            var plan = new MigrationPlan("pluginApiKeys");
            plan.From(string.Empty)
                .To<InitialMigration>("state-0");
                //.To<NoAppIdMigration>("state-1");

            var upgrader = new Upgrader(plan);
            upgrader.Execute(scopeProvider, migrationBuilder, keyValueService, logger);
        }

        public void Terminate() { }
    }
}
