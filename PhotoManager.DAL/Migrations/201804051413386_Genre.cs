namespace PhotoManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Genre : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "Genre", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Albums", "Genre");
        }
    }
}
