namespace PhotoManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CameraModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photos", "CameraModel", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photos", "CameraModel");
        }
    }
}
