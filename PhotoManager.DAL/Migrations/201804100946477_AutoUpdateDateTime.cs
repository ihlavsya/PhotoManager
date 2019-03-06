namespace PhotoManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AutoUpdateDateTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photos", "UpdateDateTime", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Photos", "UpdateDateTime");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Photos", new[] { "UpdateDateTime" });
            DropColumn("dbo.Photos", "UpdateDateTime");
        }
    }
}
