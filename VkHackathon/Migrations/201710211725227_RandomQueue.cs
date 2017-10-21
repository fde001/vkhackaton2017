namespace VkHackathon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RandomQueue : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RandomQueries",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        VisitTime = c.DateTime(nullable: false),
                        Buddies = c.String(),
                        PlaceId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RandomQueries");
        }
    }
}
