using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoManager.ViewModels
{
    public class SearchViewModel
    {
        public List<PhotoViewModel> PhotoViewModels { get; set; }
        public List<AlbumViewModel> AlbumViewModels { get; set; }
    }
}