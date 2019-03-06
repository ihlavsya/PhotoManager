using PhotoManager.DAL.Entities;
using PhotoManager.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService: IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public bool ValidatePassword(string password)
        {
            return PasswordHelper.Validate(password);
        }

        public void CreateUser(User user)
        {
            user.Password = PasswordHelper.GetMd5Hash(user.Password);
            _userRepository.Create(user);
        }

        public void UpdateUser(User user)
        {
            user.Password = PasswordHelper.GetMd5Hash(user.Password);
            _userRepository.Update(user);
        }

        public User GetUserById(int id)
        {
            return _userRepository.Get(user => user.UserId == id);
        }

        public User GetUserByName(string userName)
        {
            return _userRepository.Get(user => user.Username == userName);
        }

        public bool IsLoginDataCorrect(string name, string password)
        {
            User user = GetUserByName(name);
            return PasswordHelper.VerifyMd5Hash(password, user.Password);
        }

        public bool IsOwner(string username, int albumId)
        {
            if (username == String.Empty)
            {
                return false;
            }
            return _userRepository
                .Get(u => u.Username == username)
                .Albums
                .Any(a => a.AlbumId == albumId);
        }

        public bool IsOwner(string username,int albumId,int photoId)
        {
            var isAlbumOwner = IsOwner(username,albumId);
            var isPhotoOwner = IsOwner(photoId, username);

            return isAlbumOwner && isPhotoOwner;
        }

        public bool IsOwner(int photoId, string username)
        {
            if (username == String.Empty)
            {
                return false;
            }
            return _userRepository
                .Get(u => u.Username == username)
                .Albums.SelectMany(a=>a.Photos)
                .Any(p=>p.PhotoId==photoId);
        }

        public bool AnyUserAlbums(string username)
        {
            return _userRepository.Get(u => u.Username == username).Albums.Any();
        }

        public bool DoesUserExist(string username)
        {
            if (_userRepository.Get(u => u.Username == username) != null)
            {
                return true;
            }

            return false;
        }

    }
}
