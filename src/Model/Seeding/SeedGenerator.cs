namespace Model.Seeding
{
    using System.Data.Entity.Migrations;
    using Convenient.Data.Entity.Seeding;
    using Model.DataAccessLayer.Contexts;

    public class SeedGenerator : BaseSeedGenerator
    {
        private readonly SchoolContext context;

        public SeedGenerator(SchoolContext context)
            : base(context)
        {
            this.context = context;
        }

        // TODO: A better way to generate seed data: https://brosteins.com/2015/08/14/generating-test-data-in-entity-framework/
        public override void Generate()
        {
            this.context.Students.AddOrUpdate(new StudentFactory().All());
            this.context.SaveChanges();

            base.Generate();
        }
    }
}
