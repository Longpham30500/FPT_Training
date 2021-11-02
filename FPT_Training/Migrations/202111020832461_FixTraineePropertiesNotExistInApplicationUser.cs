namespace FPT_Training.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixTraineePropertiesNotExistInApplicationUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserCourses",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.CourseId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CourseId);
            
            AddColumn("dbo.AspNetUsers", "Education", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserCourses", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserCourses", "CourseId", "dbo.Courses");
            DropIndex("dbo.UserCourses", new[] { "CourseId" });
            DropIndex("dbo.UserCourses", new[] { "UserId" });
            DropColumn("dbo.AspNetUsers", "Education");
            DropTable("dbo.UserCourses");
        }
    }
}
