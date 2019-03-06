namespace PhotoManager.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterAdvancedSearch : DbMigration
    {
        public override void Up()
        {
            AlterStoredProcedure("dbo.AdvancedSearchPhotos", p => new
                {
                    Description = p.String(),
                    PhotoTakingPlace = p.String(),
                    CameraModel = p.String(),
                    ISO = p.Int(),
                    Flash = p.Boolean(),
                    ShutterSpeedNumerator = p.Int(),
                    ShutterSpeedDenominator = p.Int(),
                    Diaphragm = p.Double(),
                    LensFocalLength = p.Int()
                },
                body: @"SELECT DISTINCT  PhotoId, 
	                  Description,
	                  Name, 
	                  PhotoTakingPlace,
	                  CameraModel, 
	                  LensFocalLength, 
	                  ISO, 
	                  Flash, 
	                  ShutterSpeedNumerator, 
	                  ShutterSpeedDenominator, 
	                  Diaphragm,
	                  OriginalName, 
	                  UpdateDateTime FROM Photos p
	                  INNER JOIN stringlist_to_tbl(@Description) stringtblDescr
	                  on p.Description LIKE CONCAT('%', stringtblDescr.string, '%')
	                  INNER JOIN stringlist_to_tbl(@PhotoTakingPlace) stringtblPhotoTakingPlace
	                  on p.PhotoTakingPlace LIKE CONCAT('%', stringtblPhotoTakingPlace.string, '%')
                      WHERE p.CameraModel=@CameraModel
	                  AND p.LensFocalLength=@LensFocalLength
	                  AND p.ISO=@ISO
	                  AND p.Flash=@Flash
	                  AND p.ShutterSpeedNumerator=@ShutterSpeedNumerator
	                  AND p.ShutterSpeedDenominator=@ShutterSpeedDenominator
	                  AND p.Diaphragm=@Diaphragm");
        }
        
        public override void Down()
        {
        }
    }
}
