namespace PhotoManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Photos", new[] { "UpdateDateTime" });
            AlterColumn("dbo.Photos", "UpdateDateTime", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Photos", "UpdateDateTime");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Photos", new[] { "UpdateDateTime" });
            AlterColumn("dbo.Photos", "UpdateDateTime", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Photos", "UpdateDateTime");
        }
    }
}
