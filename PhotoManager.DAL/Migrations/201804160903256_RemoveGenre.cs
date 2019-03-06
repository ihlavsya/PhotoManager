namespace PhotoManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveGenre : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Albums", "Genre");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Albums", "Genre", c => c.String());
        }
    }
}
