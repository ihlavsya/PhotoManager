namespace PhotoManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ISO : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photos", "ISO", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photos", "ISO");
        }
    }
}
