using System;
using System.Collections.Generic;

namespace MsSql.Context_For_Scaffolding.Models
{
    public partial class DbScriptMigration
    {
        public Guid MigrationId { get; set; }
        public string MigrationName { get; set; }
        public DateTime MigrationDate { get; set; }
    }
}
