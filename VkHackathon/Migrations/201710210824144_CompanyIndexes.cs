namespace VkHackathon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Spatial;
    
    public partial class CompanyIndexes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "GeoIndex", c => c.Geography());
            AddColumn("dbo.Companies", "TextIndex", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "TextIndex");
            DropColumn("dbo.Companies", "GeoIndex");
        }
    }
}
