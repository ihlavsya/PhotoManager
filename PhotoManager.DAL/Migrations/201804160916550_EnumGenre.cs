namespace PhotoManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnumGenre : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "Genre", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Albums", "Genre");
        }
    }
}
