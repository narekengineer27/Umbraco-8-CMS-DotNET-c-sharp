namespace Umbraco.Plugins.Connector.Models
{
    using NPoco;
    using System;
    using Umbraco.Core.Persistence.DatabaseAnnotations;

    [TableName(TABLENAME)]
    [PrimaryKey("Id", AutoIncrement = true)]
    public class ApiKeys
    {
        [Column("Id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        public string TenantId { get; set; }

        public int UserId { get; set; }

        public string AppId { get; set; }

        public string ApiKey { get; set; }

        public DateTime CreatedOn { get; set; }

        [Ignore]
        public string TableName => TABLENAME;

        public const string TABLENAME = "pluginApiKeys";
    }
}
