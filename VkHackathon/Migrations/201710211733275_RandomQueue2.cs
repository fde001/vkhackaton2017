namespace VkHackathon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RandomQueue2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RandomQueries", "UserId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RandomQueries", "UserId");
        }
    }
}
