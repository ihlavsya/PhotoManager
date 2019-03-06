using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Helpers;
using Services;

namespace PhotoManager.Model.ViewModels
{
    public class PhotoViewModel
    {
        public PhotoViewModel()
        {
            Albums=new List<AlbumViewModel>();
            Name = Guid.NewGuid().ToString()+".jpg";
            UpdateDateTime=DateTime.Now;
            PhotoTakingPlace=String.Empty;
            Description=String.Empty;
            CameraModel=String.Empty;
        }
 
        public PhotoViewModel(ImageSize size):base()          
        {
            FilePath = PathResolver.GetPathForViewPhoto(Name, ImageSize.Small); ;
        }
        public int ItemPosition { get; set; }
        public string OriginalName { get; set; }
        public string AlbumName { get; set; }
        public int AlbumId { get; set; }
        public int PhotoId { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Description: ")]
        public string Description { get; set; }
        public string FilePath { get; set; }
        public DateTime UpdateDateTime { get; set; }
        [Display(Name = "Place of taking photo: ")]
        public string PhotoTakingPlace { set; get; }
        [Display(Name = "Camera model: ")]
        public string CameraModel { set; get; }
        [Display(Name = "Lens Focal Length: ")]
        public int LensFocalLength { set; get; }//mm
        [Display(Name = "ISO: ")]
        public int ISO { set; get; }
        [Display(Name = "Diaphragm: ")]
        public string Diaphragm { set; get; }//diametr of the circle
        public List<AlbumViewModel> Albums { set; get; }
        [Display(Name = "Flash:")]
        public bool Flash { set; get; }
        [Display(Name = "Shutter speed (in sec). If you don`t want set a fractional number, set value only to upper box: ")]
        public ShutterSpeedValues ShutterSpeed { set; get; }// Shutter speed is measured in fractions of a second
    }
}