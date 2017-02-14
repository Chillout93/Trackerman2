using System.Data.Entity.Migrations;

namespace Trackerman2.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Core.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Trackerman2.Core.ApplicationDbContext";
        }
    }
}
