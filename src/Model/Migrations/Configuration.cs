namespace Model.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Model.DataAccessLayer;
    using Model.DataAccessLayer.Contexts;
    using Model.Seeding;

    public sealed class Configuration : DbMigrationsConfiguration<SchoolContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SchoolContext context)
        {
            var generator = new SeedGenerator(context);
            generator.Generate();
        }
    }
}
