namespace PhotoManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeparateShutterSpeed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photos", "ShutterSpeedNumerator", c => c.Int(nullable: false));
            AddColumn("dbo.Photos", "ShutterSpeedDenominator", c => c.Int(nullable: false));
            DropColumn("dbo.Photos", "ShutterSpeed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Photos", "ShutterSpeed", c => c.Double(nullable: false));
            DropColumn("dbo.Photos", "ShutterSpeedDenominator");
            DropColumn("dbo.Photos", "ShutterSpeedNumerator");
        }
    }
}
