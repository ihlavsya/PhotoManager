namespace PhotoManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OriginalName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photos", "OriginalName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photos", "OriginalName");
        }
    }
}
