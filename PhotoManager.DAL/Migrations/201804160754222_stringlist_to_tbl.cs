namespace PhotoManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stringlist_to_tbl : DbMigration
    {
        public override void Up()
        {
            Sql(_createFunctionScript);
        }
        
        public override void Down()
        {
            Sql(DropFunctionScript);
        }

        #region SQL Scripts
        private readonly string _createFunctionScript = 
        @"CREATE FUNCTION [dbo].[stringlist_to_tbl] (@list nvarchar(MAX))
        RETURNS @tbl TABLE(string nvarchar(100) NOT NULL) AS
            BEGIN
        DECLARE @pos int,
        @nextpos    int,
        @valuelen   int

            SELECT @pos = 0, @nextpos = 1

        WHILE @nextpos > 0
        BEGIN
            SELECT @nextpos = charindex(' ', @list, @pos + 1)
        SELECT @valuelen = CASE WHEN @nextpos > 0
        THEN @nextpos
        ELSE len(@list) + 1
        END - @pos - 1
        INSERT @tbl(string)
        VALUES(convert(nvarchar(100), substring(@list, @pos + 1, @valuelen)))
        SELECT @pos = @nextpos
        END
            RETURN
        END";

        private const string DropFunctionScript = "DROP FUNCTION [dbo].[stringlist_to_tbl]";
        #endregion
    }
}
