namespace RoomsAndSpacesManagerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RaSM_Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category = c.String(),
                        SubCategory = c.String(),
                        Name = c.String(),
                        ShortName = c.String(),
                        RoomNumber = c.String(),
                        Area = c.Double(nullable: false),
                        BuildingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RaSM_Buildings", t => t.BuildingId, cascadeDelete: true)
                .Index(t => t.BuildingId);
            
            CreateTable(
                "dbo.RaSM_Buildings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Path = c.String(),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RaSM_Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.RaSM_Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RaSM_RoomCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RaSM_SubRoomCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Name = c.String(),
                        CategotyId = c.Int(nullable: false),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RaSM_RoomCategory", t => t.Category_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.RaSM_RoomNames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Name = c.String(),
                        SubCategotyId = c.Int(nullable: false),
                        SubCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RaSM_SubRoomCategory", t => t.SubCategory_Id)
                .Index(t => t.SubCategory_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RaSM_RoomNames", "SubCategory_Id", "dbo.RaSM_SubRoomCategory");
            DropForeignKey("dbo.RaSM_SubRoomCategory", "Category_Id", "dbo.RaSM_RoomCategory");
            DropForeignKey("dbo.RaSM_Rooms", "BuildingId", "dbo.RaSM_Buildings");
            DropForeignKey("dbo.RaSM_Buildings", "ProjectId", "dbo.RaSM_Projects");
            DropIndex("dbo.RaSM_RoomNames", new[] { "SubCategory_Id" });
            DropIndex("dbo.RaSM_SubRoomCategory", new[] { "Category_Id" });
            DropIndex("dbo.RaSM_Buildings", new[] { "ProjectId" });
            DropIndex("dbo.RaSM_Rooms", new[] { "BuildingId" });
            DropTable("dbo.RaSM_RoomNames");
            DropTable("dbo.RaSM_SubRoomCategory");
            DropTable("dbo.RaSM_RoomCategory");
            DropTable("dbo.RaSM_Projects");
            DropTable("dbo.RaSM_Buildings");
            DropTable("dbo.RaSM_Rooms");
        }
    }
}
