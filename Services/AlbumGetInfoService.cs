using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoManager.DAL.Entities;
using PhotoManager.DAL.Interfaces;
using Services.Interfaces;

namespace Services
{
    public class AlbumGetInfoService:IAlbumGetInfoService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Album> _albumRepository;

        public AlbumGetInfoService(IRepository<User> userRepository, IRepository<Album> albumRepository)
        {
            _userRepository = userRepository;
            _albumRepository = albumRepository;
        }

        public Album GetAlbumByName(string albumName)
        {
            return _albumRepository.Get(album => album.Name == albumName);
        }

        public Album GetAlbumByName(string username,string albumName)
        {
            try
            {
                return _userRepository.Get(u => u.Username == username).Albums.First(a => a.Name == albumName);
            }
            catch (InvalidOperationException)
            {
                return null;
            }

        }
        public Album GetAlbumById(int id)//good
        {
            return _albumRepository.Get(id);
        }

        public Album GetAlbumById(int id,string username)//good
        {
            return _albumRepository.Get(a => a.AlbumId == id && a.Username == username);
        }

        public IEnumerable<int> GetAllAlbumsId(string username)
        {
            return _userRepository
                .Get(u => u.Username == username)
                .Albums.OrderByDescending(a=>a.UpdateDateTime).Select(a => a.AlbumId);
        }

        public string GetAlbumNameById(int id)
        {
            return _albumRepository.Get(a => a.AlbumId == id).Name;
        }

        public IEnumerable<Album> GetAlbumsByUserName(string username,int objectsPerPages,int page)
        {
            return _userRepository.Get(u => u.Username == username)
                .Albums
                .OrderByDescending(a => a.UpdateDateTime)
                .Skip((page - 1) * objectsPerPages).Take(objectsPerPages);
        }
        public Album GetAlbumById(int id, int userId)
        {
            var user = _userRepository.Get(u => u.UserId == userId);
            var userAlbums = user.Albums.Any(p => p.AlbumId == id);
            if (userAlbums)
            {
                return GetAlbumById(id);
            }

            return null;
        }

        public int GetAlbumIdByName(string name)
        {
            return _albumRepository.Get(a => a.Name == name).AlbumId;
        }

        public bool DoesAlbumExist(int albumId)
        {
            if (_albumRepository.Get(a => a.AlbumId == albumId)!=null)
            {
                return true;
            }

            return false;
        }

        public int CountPhotos(string username,Func<Album,bool> albumIdPredicate)
        {
            return _userRepository.Get(u=>u.Username==username).Albums.Where(albumIdPredicate).SelectMany(a=>a.Photos).Count();
        }

    }
}
