using System.Linq;
using System.Web.Configuration;
using AutoMapper;
using PhotoManager.DAL.Entities;
using Services;
using Helpers;

namespace PhotoManager.Model.ViewModels.Mapping
{
    public class AdvancedMapper : IAdvancedMapper
    {
        private readonly IMapper _mapper;

        public AdvancedMapper(IMapper mapper)
        {
            _mapper = mapper;
        }
        public AlbumViewModel MapAlbum(Album album, ImageSize size)
        {
            var mappedAlbum = _mapper.Map<AlbumViewModel>(album);
            if (mappedAlbum.Photos.Count != 0)
            {
                mappedAlbum.Photos = mappedAlbum.Photos.OrderByDescending(m => m.UpdateDateTime).ToList();
                mappedAlbum.PhotoAlbumCoverPath = PathResolver.GetPathForViewPhoto(mappedAlbum.Photos.First().Name, size);
            }
            else
            {
                mappedAlbum.PhotoAlbumCoverPath = WebConfigurationManager.AppSettings["NoPhotoImage"];
            }

            return mappedAlbum;
        }

        public PhotoViewModel MapPhoto(Photo photo, ImageSize size)
        {
            var mappedPhoto = _mapper.Map<PhotoViewModel>(photo);
            mappedPhoto.FilePath = PathResolver.GetPathForViewPhoto(photo.Name, size);
            return mappedPhoto;
        }

        public PhotoViewModel MapPhoto(Photo photo, ImageSize size,int albumId, string albumName, int itemPosition)
        {
            var mappedPhoto = MapPhoto(photo, size);
            mappedPhoto.AlbumId = albumId;
            mappedPhoto.AlbumName = albumName;
            mappedPhoto.ItemPosition = itemPosition;
            return mappedPhoto;
        }

        public Photo MapPhotoViewModel(PhotoViewModel photoViewModel)
        {
            var mappedPhoto = _mapper.Map<Photo>(photoViewModel);
            return mappedPhoto;
        }

        public Album MapAlbumViewModel(AlbumViewModel albumViewModel)
        {
            var mappedAlbum = _mapper.Map<Album>(albumViewModel);
            return mappedAlbum;
        }
    }
}