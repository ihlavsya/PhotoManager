namespace PhotoManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Diaphragm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photos", "Diaphragm", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photos", "Diaphragm");
        }
    }
}
