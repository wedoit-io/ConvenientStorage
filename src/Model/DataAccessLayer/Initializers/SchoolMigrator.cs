namespace Model.DataAccessLayer.Initializers
{
    using Model.DataAccessLayer.Contexts;
    using Model.Migrations;

    public class SchoolMigrator : System.Data.Entity.MigrateDatabaseToLatestVersion<SchoolContext, Configuration>
    {
    }
}