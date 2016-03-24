namespace Model.DataAccessLayer.Initializers
{
    using System.Data.Entity;
    using Model.DataAccessLayer.Contexts;

    public class NoopInitializer : IDatabaseInitializer<SchoolContext>
    {
        public void InitializeDatabase(SchoolContext context)
        {
        }
    }
}