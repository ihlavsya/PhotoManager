namespace PhotoManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Flash : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photos", "Flash", c => c.Boolean(nullable: false));
            AddColumn("dbo.Photos", "ShutterSpeed", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photos", "ShutterSpeed");
            DropColumn("dbo.Photos", "Flash");
        }
    }
}
