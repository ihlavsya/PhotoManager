using Helpers;
using PhotoManager.DAL.Entities;
using Services;

namespace PhotoManager.Model.ViewModels.Mapping
{
    public interface IAdvancedMapper
    {
        AlbumViewModel MapAlbum(Album album, ImageSize size);
        PhotoViewModel MapPhoto(Photo photo,ImageSize size,int albumId,string albumName,int itemPosition);
        PhotoViewModel MapPhoto(Photo photo, ImageSize size);
        Photo MapPhotoViewModel(PhotoViewModel photoViewModel);
        Album MapAlbumViewModel(AlbumViewModel albumViewModel);
    }
}
