namespace PhotoManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FloatDiaphragm : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Photos", "Diaphragm", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Photos", "Diaphragm", c => c.Int(nullable: false));
        }
    }
}
