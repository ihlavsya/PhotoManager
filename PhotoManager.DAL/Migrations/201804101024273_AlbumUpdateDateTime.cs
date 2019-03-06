namespace PhotoManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlbumUpdateDateTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "UpdateDateTime", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Albums", "UpdateDateTime");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Albums", new[] { "UpdateDateTime" });
            DropColumn("dbo.Albums", "UpdateDateTime");
        }
    }
}
