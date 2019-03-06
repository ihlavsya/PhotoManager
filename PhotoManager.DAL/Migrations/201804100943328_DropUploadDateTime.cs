namespace PhotoManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropUploadDateTime : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Photos", new[] { "UploadDateTime" });
            DropColumn("dbo.Photos", "UploadDateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Photos", "UploadDateTime", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Photos", "UploadDateTime");
        }
    }
}
