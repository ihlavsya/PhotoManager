using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoManager.DAL.EF;
using PhotoManager.DAL.Entities;
using System.Data.SqlClient;
using PhotoManager.DAL.Interfaces;

namespace PhotoManager.DAL.Repositories
{
    public class PhotoRepository:GenericRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(PhotoManagerContext context):base(context){}

        public IEnumerable<Photo> AdvancedSearchPhotos(Photo photo)// you should alter proccedure that she can accept table, not list of paramenters
        {//and then you will be able tj pass your model normally (i hope)
            SqlParameter paramDescr = new SqlParameter("@Description", photo.Description);
            SqlParameter paramPhotoTakingPlace = new SqlParameter("@PhotoTakingPlace", photo.PhotoTakingPlace);
            SqlParameter paramCameraModel = new SqlParameter("@CameraModel", photo.CameraModel);
            SqlParameter paramISO = new SqlParameter("@ISO", photo.ISO);
            SqlParameter paramFlash = new SqlParameter("@Flash", photo.Flash);
            SqlParameter paramShutterSpeedNumerator = new SqlParameter("@ShutterSpeedNumerator", photo.ShutterSpeedNumerator);
            SqlParameter paramShutterSpeedDenominator = new SqlParameter("@ShutterSpeedDenominator", photo.ShutterSpeedDenominator);
            SqlParameter paramDiaphragm = new SqlParameter("@Diaphragm", photo.Diaphragm);
            SqlParameter paramLensFocalLength = new SqlParameter("@LensFocalLength", photo.LensFocalLength);

            SqlParameter[] parameters=new SqlParameter[9];
            parameters[0]=paramDescr;
            parameters[1]=paramPhotoTakingPlace;
            parameters[2]=paramCameraModel;
            parameters[3]=paramISO;
            parameters[4]=paramFlash;
            parameters[5]=paramShutterSpeedNumerator;
            parameters[6] = paramShutterSpeedDenominator;
            parameters[7]=paramDiaphragm;
            parameters[8]=paramLensFocalLength;

            var photos = _dataContext.Database.SqlQuery<Photo>("exec dbo.[AdvancedSearchPhotos] " +
                                                               "@Description," +
                                                               "@PhotoTakingPlace," +
                                                               "@CameraModel," +
                                                               "@ISO," +
                                                               "@Flash," +
                                                               "@ShutterSpeedNumerator," +
                                                               "@ShutterSpeedDenominator," +
                                                               "@Diaphragm," +
                                                               "@LensFocalLength", parameters);
            
            return photos;
        }
    }
}
