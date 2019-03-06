namespace PhotoManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LensFocalLength : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photos", "LensFocalLength", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photos", "LensFocalLength");
        }
    }
}
