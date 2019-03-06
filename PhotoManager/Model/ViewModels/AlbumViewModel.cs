using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PhotoManager.DAL.Entities;

namespace PhotoManager.Model.ViewModels
{
    public class AlbumViewModel
    {
        public AlbumViewModel()
        {
            Photos=new List<PhotoViewModel>();

            if (Photos.Count != 0)
            {
                Photos.OrderByDescending(p => p.UpdateDateTime);
                PhotoAlbumCoverPath = Photos.First().FilePath;//you must to check out does filepath has some value
                UpdateDateTime=DateTime.Now;
                Description=String.Empty;
            }
        }
        public int AlbumId { get; set; }
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please enter the name.")]
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PhotoViewModel> Photos { set; get; }
        public string PhotoAlbumCoverPath { get; set; }
        public Genres Genre { get; set; }
        public string Username { get; set; }
        public int GenresId { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}