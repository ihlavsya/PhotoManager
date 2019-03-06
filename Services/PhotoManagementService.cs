using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using PhotoManager.DAL.Entities;
using Services.Interfaces;
using PhotoManager.DAL.Interfaces;
using Helpers;

namespace Services
{
    public class PhotoManagementService:IPhotoManagementService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Photo> _photoRepository;
        private readonly IRepository<Album> _albumRepository;

        public PhotoManagementService(IRepository<User> userRepository, IRepository<Photo> photoRepository, IRepository<Album> albumRepository)
        {
            _userRepository = userRepository;
            _photoRepository = photoRepository;
            _albumRepository = albumRepository;
        }
        private void SaveImageAsJpeg(Image image, string imagePath)
        {
            ImageCodecInfo jpgInfo = ImageCodecInfo.GetImageEncoders()
                .Where(codecInfo => codecInfo.MimeType == "image/jpeg").First();
            using (EncoderParameters encParams = new EncoderParameters(1))
            {
                long quality = 100;
                encParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                //quality should be in the range [0..100]
                image.Save(imagePath, jpgInfo, encParams);
            }
        }

        public void CreatePhoto(int photoId, int albumId,string username)
        {
            User user = _userRepository.Get(u => u.Username == username);
            var album = user.Albums.First(a => a.AlbumId == albumId);
            var photo = album.Photos.First(p => p.PhotoId == photoId);
            album.Photos.Add(photo);
            photo.UpdateDateTime = DateTime.Now;
            _userRepository.Update(user);
        }

        public void CreatePhoto(Photo photo, HttpPostedFileBase file, int albumId)
        {
            string originalPhotoPath = PathResolver.GetPathForSavePhoto(photo.Name, ImageSize.Original);
            file.UploadFile(originalPhotoPath);

            Image image = Image.FromFile(originalPhotoPath);

            Image smallImage = ScaleImage.Scale(image, (int)ImageSize.Small, (int)ImageSize.Small);
            string smallPhotoPath = PathResolver.GetPathForSavePhoto(photo.Name, ImageSize.Small);

            SaveImageAsJpeg(smallImage, smallPhotoPath);

            Image mediumImage = ScaleImage.Scale(image, (int)ImageSize.Medium, (int)ImageSize.Medium);
            string mediumPhotoPath = PathResolver.GetPathForSavePhoto(photo.Name, ImageSize.Medium);

            SaveImageAsJpeg(mediumImage, mediumPhotoPath);

            var album = _albumRepository.Get(a => a.AlbumId == albumId);
            album.Photos.Add(photo);
            _albumRepository.Update(album);

            image.Dispose();// поменять на фигурные скобки
            smallImage.Dispose();
            mediumImage.Dispose();
        }

        public void DeletePhoto(int photoId)
        {
            var photo = _photoRepository.Get(p => p.PhotoId == photoId);

            string smallPhotoPath = PathResolver.GetPathForSavePhoto(photo.Name, ImageSize.Small);
            string mediumPhotoPath = PathResolver.GetPathForSavePhoto(photo.Name, ImageSize.Medium);
            string originalPhotoPath = PathResolver.GetPathForSavePhoto(photo.Name, ImageSize.Original);

            File.Delete(smallPhotoPath);
            File.Delete(mediumPhotoPath);
            File.Delete(originalPhotoPath);

            _photoRepository.Delete(photoId);
        }

        public void UpdatePhoto(Photo photo)
        {
            photo.UpdateDateTime = DateTime.Now;
            _photoRepository.Update(photo);
        }
    }
}
