namespace PhotoManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhotoTakingPlace : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photos", "PhotoTakingPlace", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photos", "PhotoTakingPlace");
        }
    }
}
