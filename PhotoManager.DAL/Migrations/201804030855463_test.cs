namespace PhotoManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Photos", "UploadDateTime");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Photos", new[] { "UploadDateTime" });
        }
    }
}
