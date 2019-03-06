using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoManager.DAL.Entities;

namespace Services
{
    public interface IUserService
    {
        bool IsLoginDataCorrect(string name, string password);
        User GetUserByName(string name);
        User GetUserById(int id);
        void CreateUser(User user);
        void UpdateUser(User user);
        bool ValidatePassword(string password);
        //bool VerifyHashedPassword(string hashedPassword, string password);
        bool DoesUserExist(string username);
        bool IsOwner(string username, int albumId);
        bool IsOwner(string username, int albumId, int photoId);
        bool IsOwner(int photoId,string username);
        bool AnyUserAlbums(string username);
    }
}
