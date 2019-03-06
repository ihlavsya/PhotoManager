namespace PhotoManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedUploadDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Photos", "UploadDateTime", c => c.DateTime(nullable: false,defaultValueSql: "GETUTCDATE()"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Photos", "UploadDateTime", c => c.DateTime(nullable: false));
        }
    }
}
