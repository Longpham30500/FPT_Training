namespace FPT_Training.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTrainerPropertyIntoUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Address", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "Age");
        }
    }
}
