using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoManager.DAL.Entities;

namespace Services.Interfaces
{
    public interface IAlbumManagementService
    {
        void CreateAlbum(Album album, string username);
        void UpdateAlbum(Album album);
        void DeleteAlbum(int albumId);
    }
}
