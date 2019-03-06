using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoManager.DAL.Entities;
using PhotoManager.DAL.Interfaces;
using Services.Interfaces;
using Helpers;

namespace Services
{
    public class PhotoGetInfoService:IPhotoGetInfoService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Album> _albumRepository;
        private readonly IPhotoRepository _photoRepository;

        public PhotoGetInfoService(IRepository<User> userRepository, IRepository<Album> albumRepository, IPhotoRepository photoRepository)
        {
            _userRepository = userRepository;
            _albumRepository = albumRepository;
            _photoRepository = photoRepository;
        }
        public Photo GetPhotoById(int id)
        {
            return _photoRepository.Get(photo => photo.PhotoId == id);
        }

        public Photo GetPhotoById(int id, int userId)
        {
            return  _photoRepository.Get(photo => photo.PhotoId == id && photo.Albums.Any(album => album.UserId == userId));
        }

        public IEnumerable<Photo> GetAllPhotos(IEnumerable<int> photosIds,int objectsPerPages,int page)
        {
            return _photoRepository
                .Find(p => photosIds.Contains(p.PhotoId))
                .OrderByDescending(p => p.UpdateDateTime)
                .Skip((page - 1) * objectsPerPages).Take(objectsPerPages);
        }

        public IEnumerable<int> GetAllPhotosId(Func<User, bool> userPredicate, Func<Album, bool> albumPredicate)
        {
            return _userRepository.Get(userPredicate)
                .Albums
                .Where(albumPredicate)
                .SelectMany(a => a.Photos)
                .OrderByDescending(p => p.UpdateDateTime)
                .Select(p=>p.PhotoId);
        }

        public IEnumerable<Photo> GetAllPhotos(Func<User, bool> userPredicate, Func<Album, bool> albumPredicate,
            int objectsPerPages, int page)
        {
            return _userRepository.Get(userPredicate)
                .Albums
                .Where(albumPredicate)
                .SelectMany(a => a.Photos)
                .OrderByDescending(p => p.UpdateDateTime)
                .Skip((page - 1) * objectsPerPages).Take(objectsPerPages);

        }

        //public IEnumerable<Photo> GetAllPhotos(Func<User, bool> userPredicate, Func<Album, bool> albumPredicate,
        //    int objectsPerPages, int page)
        //{
        //    return _userRepository.Get(userPredicate)
        //        .Albums
        //        .Where(albumPredicate)
        //        .SelectMany(a => a.Photos)
        //        .OrderByDescending(p => p.UpdateDateTime)
        //        .Skip((page - 1) * objectsPerPages).Take(objectsPerPages);

        //}

        public IEnumerable<Photo> GetAllPhotos(string userName, int albumId,//check in profiler
            int objectsPerPages, int page)
        {
            return _photoRepository
                .Find(p => p.Albums.Any(a => a.Username == userName && a.AlbumId == albumId))
                .OrderByDescending(p => p.UpdateDateTime)
                .Skip((page - 1) * objectsPerPages).Take(objectsPerPages);
        }

        public IEnumerable<Photo> GetThreePhotos(int photoId, int albumId, string username)
        {
            var photos = _photoRepository
                .Find(p => p.Albums.Any(a => a.AlbumId == albumId && a.Username == username))
                .OrderByDescending(p => p.UpdateDateTime)
                .SkipWhile(p => p.PhotoId != photoId)
                .Skip(-1)
                .Take(3);

            return photos;
        }

        public IEnumerable<Photo> GetThreePhotos(string username, int albumId, int itemPosition)
        {
            var photos = _photoRepository
                .Find(p => p.Albums.Any(a => a.AlbumId == albumId && a.Username == username))
                .OrderByDescending(p => p.UpdateDateTime).ToList();

            if (itemPosition ==photos.Count()-1)
            {
                return photos.Skip(itemPosition-1).Take(2).Union(photos.Skip(0).Take(1));
            }

            if (itemPosition == 0)
            {
                return photos.Skip(photos.Count() - 1).Take(1).Union(photos.Take(2));
            }

            return photos.Skip(itemPosition-1).Take(3).ToList();

        }

        public IEnumerable<Photo> GetAllPhotos(Func<Album, bool> albumPredicate,int objectsPerPages, int page)
        {
            return _albumRepository.Get(albumPredicate)
                .Photos
                .OrderByDescending(p => p.UpdateDateTime)
                .Skip((page - 1) * objectsPerPages).Take(objectsPerPages);
        }

        public IEnumerable<Photo> AdvancedSearchPhotos(Photo photo,int page,int objectsPerPages)
        {
            var photos = _photoRepository.AdvancedSearchPhotos(photo)
                .OrderByDescending(p=>p.UpdateDateTime)
               .Skip((page - 1) * objectsPerPages).Take(objectsPerPages);
            return photos;
        }

        public IEnumerable<int> AdvancedSearchPhotosIds(Photo photo)
        {
            return _photoRepository.AdvancedSearchPhotos(photo)
                .OrderByDescending(p=>p.UpdateDateTime)
                .Select(p => p.PhotoId);
        }

        public List<Photo> FindPhotosByDescriptionQuery(string query, int page,int objectsPerPages)//скорее всего можно переписать на Ienumerable
        {
            var photos = new List<Photo>();

            string[] subDescriptionQueries = query.Split(' ');

            foreach (var q in subDescriptionQueries)
            {
                var result = _photoRepository
                    .Find(a => a.Description.Contains(q, StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(p=>p.UpdateDateTime)
                    .Skip((page - 1) * objectsPerPages).Take(objectsPerPages);

                photos.AddRange(result);
            }

            return photos;
        }

        public List<int> FindPhotosIdsByDescriptionQuery(string query)
        {
            var photosIds = new List<int>();

            string[] subDescriptionQueries = query.Split(' ');

            foreach (var q in subDescriptionQueries)
            {
                var result = _photoRepository
                    .Find(a => a.Description.Contains(q, StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(p=>p.UpdateDateTime)
                    .Select(p=>p.PhotoId);

                photosIds.AddRange(result);
            }

            return photosIds;
        }
    }
}
