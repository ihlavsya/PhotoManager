namespace PhotoManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveIsPaid : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "IsPaid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "IsPaid", c => c.Boolean(nullable: false));
        }
    }
}
