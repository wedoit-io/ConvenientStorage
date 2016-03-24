namespace Model.DataAccessLayer.Contexts
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Convenient.Data.Entity;

    public abstract class DbContextWithOurConventions : ConvenientDbContext
    {
        protected DbContextWithOurConventions(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
