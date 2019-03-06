namespace PhotoManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        AlbumId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AlbumId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        PhotoId = c.Int(nullable: false, identity: true),
                        AlbumId = c.Int(nullable: false),
                        UploadDateTime = c.DateTime(nullable: false),
                        Description = c.String(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.PhotoId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        IsPaid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.PhotoAlbums",
                c => new
                    {
                        Photo_PhotoId = c.Int(nullable: false),
                        Album_AlbumId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Photo_PhotoId, t.Album_AlbumId })
                .ForeignKey("dbo.Photos", t => t.Photo_PhotoId, cascadeDelete: true)
                .ForeignKey("dbo.Albums", t => t.Album_AlbumId, cascadeDelete: true)
                .Index(t => t.Photo_PhotoId)
                .Index(t => t.Album_AlbumId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Albums", "UserId", "dbo.Users");
            DropForeignKey("dbo.PhotoAlbums", "Album_AlbumId", "dbo.Albums");
            DropForeignKey("dbo.PhotoAlbums", "Photo_PhotoId", "dbo.Photos");
            DropIndex("dbo.PhotoAlbums", new[] { "Album_AlbumId" });
            DropIndex("dbo.PhotoAlbums", new[] { "Photo_PhotoId" });
            DropIndex("dbo.Albums", new[] { "UserId" });
            DropTable("dbo.PhotoAlbums");
            DropTable("dbo.Users");
            DropTable("dbo.Photos");
            DropTable("dbo.Albums");
        }
    }
}
