using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhotoManager.Model.ViewModels;

namespace PhotoManager.Helpers
{
    public class PhotoSlideMaker
    {
        public PhotoViewModel ActivePhotoViewModel { get; set; }
        public List<int> PhotoViewModelsIds { get; set; }
        public int PhotoId { get; set; }

        public PhotoSlideMaker(PhotoViewModel photoViewModel, List<int> photoViewModelsIds, int photoId)
        {
            ActivePhotoViewModel = photoViewModel;
            PhotoViewModelsIds = photoViewModelsIds;
            PhotoId = photoId;
        }

        public PhotoSliderViewModel GetFilledPhotoSliderViewModel()
        {


            var photoSlider = new PhotoSliderViewModel()
            {
                ActivePhoto = ActivePhotoViewModel,
                NextPhoto = PhotoViewModels[nextIndex],
                PreviousPhoto = PhotoViewModels[previousIndex],
                AllPhotosIds = PhotoViewModelsIds
            };

            return photoSlider;
        }
    }
}