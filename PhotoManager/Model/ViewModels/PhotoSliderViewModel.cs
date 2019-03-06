using System.Collections.Generic;

namespace PhotoManager.Model.ViewModels
{
    public class PhotoSliderViewModel
    {
        public PhotoViewModel PreviousPhoto { get; set; }
        public PhotoViewModel ActivePhoto { get; set; }
        public PhotoViewModel NextPhoto { get; set; }
        public bool IsUserOwner { get; set; }
    }
}