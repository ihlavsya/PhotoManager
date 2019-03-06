using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoManager.DAL.Entities;

namespace PhotoManager.DAL.Interfaces
{
    public interface IPhotoRepository:IRepository<Photo>
    {
        IEnumerable<Photo> AdvancedSearchPhotos(Photo photo);
    }
}
