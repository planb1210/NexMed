namespace NexMed.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Weathers", "City_Id", c => c.Int());
            CreateIndex("dbo.Weathers", "City_Id");
            AddForeignKey("dbo.Weathers", "City_Id", "dbo.Cities", "Id");
            DropColumn("dbo.Weathers", "CityName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Weathers", "CityName", c => c.String());
            DropForeignKey("dbo.Weathers", "City_Id", "dbo.Cities");
            DropIndex("dbo.Weathers", new[] { "City_Id" });
            DropColumn("dbo.Weathers", "City_Id");
        }
    }
}
