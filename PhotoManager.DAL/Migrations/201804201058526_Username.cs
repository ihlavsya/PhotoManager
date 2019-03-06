namespace PhotoManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Username : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "Username", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Albums", "Username");
        }
    }
}
