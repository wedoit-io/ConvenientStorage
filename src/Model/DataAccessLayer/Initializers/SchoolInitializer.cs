namespace Model.DataAccessLayer.Initializers
{
    using Model.DataAccessLayer.Contexts;
    using Model.Seeding;

    public class SchoolInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<SchoolContext>
    {
        protected override void Seed(SchoolContext context)
        {
            var generator = new SeedGenerator(context);
            generator.Generate();
        }
    }
}
