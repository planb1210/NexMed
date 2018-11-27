namespace NexMed.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "City_Id", c => c.Int());
            CreateIndex("dbo.Users", "City_Id");
            AddForeignKey("dbo.Users", "City_Id", "dbo.Cities", "Id");
            DropColumn("dbo.Users", "City");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "City", c => c.String());
            DropForeignKey("dbo.Users", "City_Id", "dbo.Cities");
            DropIndex("dbo.Users", new[] { "City_Id" });
            DropColumn("dbo.Users", "City_Id");
        }
    }
}
