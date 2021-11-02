namespace FPT_Training.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableTrainingStaffThroughApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "isTrainingStaff", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "isTrainingStaff");
        }
    }
}
