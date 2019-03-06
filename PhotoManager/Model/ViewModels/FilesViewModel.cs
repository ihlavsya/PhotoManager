using System.Collections.Generic;
using System.Web;

namespace PhotoManager.Model.ViewModels
{
    public class FilesViewModel
    {
        public IEnumerable<HttpPostedFileBase> Files { get; set; }
        public int AlbumId { get; set; }
    }
}