namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class MakeStudentLastNameRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Student", "LastName", c => c.String(nullable: false));
        }

        public override void Down()
        {
            AlterColumn("dbo.Student", "LastName", c => c.String());
        }
    }
}
