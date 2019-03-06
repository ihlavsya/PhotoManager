namespace PhotoManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAlbumIdFromPhoto : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Photos", "AlbumId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Photos", "AlbumId", c => c.Int(nullable: false));
        }
    }
}
