namespace RoomsAndSpacesManagerDesktop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteRoomsDtoFieldsCategotyAndSubCategory : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RaSM_Rooms", "Category");
            DropColumn("dbo.RaSM_Rooms", "SubCategory");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RaSM_Rooms", "SubCategory", c => c.String());
            AddColumn("dbo.RaSM_Rooms", "Category", c => c.String());
        }
    }
}
