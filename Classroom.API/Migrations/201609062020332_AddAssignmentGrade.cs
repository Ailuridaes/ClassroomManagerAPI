namespace Classroom.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAssignmentGrade : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assignments", "Grade", c => c.Int(nullable: false, defaultValue: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Assignments", "Grade");
        }
    }
}
