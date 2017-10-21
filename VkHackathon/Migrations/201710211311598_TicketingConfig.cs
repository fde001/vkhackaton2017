namespace VkHackathon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TicketingConfig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TicketSystems",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ExternalId = c.String(),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TicketSystems");
        }
    }
}
