namespace VkHackathon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MissingFields1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "ExternalId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "ExternalId");
        }
    }
}
