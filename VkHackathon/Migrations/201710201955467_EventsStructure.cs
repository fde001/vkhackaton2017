namespace VkHackathon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventsStructure : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        FullName = c.String(),
                        Description = c.String(),
                        Address = c.String(),
                        Site = c.String(),
                        Url = c.String(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Rating = c.Double(nullable: false),
                        EntityType_Id = c.Int(),
                        Source_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EntityTypes", t => t.EntityType_Id)
                .ForeignKey("dbo.Sources", t => t.Source_Id)
                .Index(t => t.EntityType_Id)
                .Index(t => t.Source_Id);
            
            CreateTable(
                "dbo.EntityTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Creations",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Url = c.String(),
                        Description = c.String(),
                        Rating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Photo = c.String(),
                        EntityType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EntityTypes", t => t.EntityType_Id)
                .Index(t => t.EntityType_Id);
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        BeginTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Company_Id = c.Long(),
                        Creation_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.Company_Id)
                .ForeignKey("dbo.Creations", t => t.Creation_Id)
                .Index(t => t.Company_Id)
                .Index(t => t.Creation_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "Creation_Id", "dbo.Creations");
            DropForeignKey("dbo.Schedules", "Company_Id", "dbo.Companies");
            DropForeignKey("dbo.Creations", "EntityType_Id", "dbo.EntityTypes");
            DropForeignKey("dbo.Companies", "Source_Id", "dbo.Sources");
            DropForeignKey("dbo.Companies", "EntityType_Id", "dbo.EntityTypes");
            DropIndex("dbo.Schedules", new[] { "Creation_Id" });
            DropIndex("dbo.Schedules", new[] { "Company_Id" });
            DropIndex("dbo.Creations", new[] { "EntityType_Id" });
            DropIndex("dbo.Companies", new[] { "Source_Id" });
            DropIndex("dbo.Companies", new[] { "EntityType_Id" });
            DropTable("dbo.Schedules");
            DropTable("dbo.Creations");
            DropTable("dbo.Sources");
            DropTable("dbo.EntityTypes");
            DropTable("dbo.Companies");
        }
    }
}
