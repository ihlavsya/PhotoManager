using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoManager.DAL.Entities;

namespace Services.Interfaces
{
    public interface IPhotoGetInfoService
    {
        List<Photo> FindPhotosByDescriptionQuery(string query, int page, int objectsPerPages);
        List<int> FindPhotosIdsByDescriptionQuery(string query);
        IEnumerable<Photo> AdvancedSearchPhotos(Photo photo, int page, int objectsPerPages);
        IEnumerable<int> AdvancedSearchPhotosIds(Photo photo);
        Photo GetPhotoById(int id);
        Photo GetPhotoById(int id, int userId);
        //IEnumerable<Photo> GetAllPhotos(Func<User,bool> userPredicate, Func<Album,bool> albumPredicate, int objectsPerPages, int page);
        //IEnumerable<Photo> GetAllPhotos(IEnumerable<int> photosIds, int objectsPerPages, int page);
        IEnumerable<int> GetAllPhotosId(Func<User,bool> userPredicate,Func<Album,bool> albumPredicate);
        //IEnumerable<int> GetAllPhotosId(Func<Album, bool> albumPredicate);
        //IEnumerable<Photo> GetAllPhotos(Func<Album, bool> albumPredicate, int objectsPerPages, int page);
        IEnumerable<Photo> GetThreePhotos(string username, int albumId, int itemPosition);
        IEnumerable<Photo> GetThreePhotos(int photoId, int albumId, string username);
        IEnumerable<Photo> GetAllPhotos(string userName, int albumId, //check in profiler
            int objectsPerPages, int page);
    }
}
