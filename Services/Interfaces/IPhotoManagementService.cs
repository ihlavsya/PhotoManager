using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using PhotoManager.DAL.Entities;

namespace Services.Interfaces
{
    public interface IPhotoManagementService
    {
        void CreatePhoto(Photo photo, HttpPostedFileBase file, int albumId);
        void CreatePhoto(int photoId, int albumId, string username);
        void UpdatePhoto(Photo photo);
        void DeletePhoto(int photoId);
    }
}
