using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoManager.DAL.Entities;

namespace Services.Interfaces
{
    public interface IAlbumGetInfoService
    {
        Album GetAlbumByName(string albumName);
        Album GetAlbumByName(string username,string albumName);
        string GetAlbumNameById(int id);
        Album GetAlbumById(int id);
        Album GetAlbumById(int id, int userId);
        Album GetAlbumById(int id, string username);
        IEnumerable<Album> GetAlbumsByUserName(string username,int objectsPerPages,int page);
        IEnumerable<int> GetAllAlbumsId(string username);
        int CountPhotos(string username,Func<Album, bool> albumIdPredicate);
        bool DoesAlbumExist(int albumId);
        int GetAlbumIdByName(string name);
    }
}
