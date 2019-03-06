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
    public class AlbumManagementService:IAlbumManagementService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Album> _albumRepository;
        private readonly IPhotoManagementService _photoManagementService;

        public AlbumManagementService(IRepository<User> userRepository, IRepository<Album> albumRepository, IPhotoManagementService photoManagementService)
        {
            _userRepository = userRepository;
            _albumRepository = albumRepository;
            _photoManagementService = photoManagementService;
        }

        public void DeleteAlbum(int albumId)
        {
            Album album = _albumRepository.Get(albumId);
            var photosId = album.Photos.Select(x => x.PhotoId).ToList();

            album.Photos.Clear();
            _albumRepository.Update(album);
            foreach (var photoId in photosId)
            {
                _photoManagementService.DeletePhoto(photoId);
            }
            _albumRepository.Delete(albumId);
        }

        public void UpdateAlbum(Album album)
        {
            album.UpdateDateTime = DateTime.Now;
            _albumRepository.Update(album);
        }

        public void CreateAlbum(Album album, string username)
        {
            var user = _userRepository.Get(u => u.Username == username);//вынести в отдельный метод
            album.UpdateDateTime = DateTime.Now;
            user.Albums.Add(album);
            _userRepository.Update(user);
        }
    }
}
