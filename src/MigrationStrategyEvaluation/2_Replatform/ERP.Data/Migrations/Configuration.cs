using System.Data.Entity.Migrations;
using ERP.Data.Seed;

namespace ERP.Data.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<ErpContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "ERP.Data.ErpContext";
        }

        protected override void Seed(ErpContext context)
        {
            ErpSeedDaten.Initialisieren(context);
        }
    }
}
