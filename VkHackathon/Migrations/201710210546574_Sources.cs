namespace VkHackathon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sources : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Creations", "Source_Id", c => c.Int());
            AddColumn("dbo.Schedules", "Source_Id", c => c.Int());
            CreateIndex("dbo.Creations", "Source_Id");
            CreateIndex("dbo.Schedules", "Source_Id");
            AddForeignKey("dbo.Creations", "Source_Id", "dbo.Sources", "Id");
            AddForeignKey("dbo.Schedules", "Source_Id", "dbo.Sources", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "Source_Id", "dbo.Sources");
            DropForeignKey("dbo.Creations", "Source_Id", "dbo.Sources");
            DropIndex("dbo.Schedules", new[] { "Source_Id" });
            DropIndex("dbo.Creations", new[] { "Source_Id" });
            DropColumn("dbo.Schedules", "Source_Id");
            DropColumn("dbo.Creations", "Source_Id");
        }
    }
}
