using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services.Interfaces;
using PhotoManager.DAL.Entities;
using PhotoManager.DAL.Interfaces;
using PhotoManager.DAL.Repositories;

namespace Services.Tests
{
    [TestClass]
    public class AlbumGetInfoServiceTests
    {
        public AlbumGetInfoServiceTests()
        {
            _photos = new List<Photo>()
            {
                new Photo()
                {
                    CameraModel = String.Empty,
                    Description = "Valencia Spain Architecture",
                    Diaphragm = 0,
                    Flash = false,
                    LensFocalLength = 0,
                    Name = "611b9acf-4d5d-4145-9131-c4bf4882594c.jpg",
                    ISO = 0,
                    OriginalName = "download (1).jpg",
                    PhotoId = 1,
                    PhotoTakingPlace = "Spain, Valencia",
                    UpdateDateTime = new DateTime(2018,04,19,10,51,56),
                    ShutterSpeedDenominator = 1,
                    ShutterSpeedNumerator = 1
                },

                new Photo()
                {
                    CameraModel = String.Empty,
                    Description = "Valencia Spain",
                    Diaphragm = 0,
                    Flash = false,
                    LensFocalLength = 0,
                    Name = "430d0e2c-12c0-4cfc-b196-c6e878910dcd.jpg",
                    ISO = 0,
                    OriginalName = "sciurus3.jpg",
                    PhotoId = 1,
                    PhotoTakingPlace = "Spain",
                    UpdateDateTime = new DateTime(2016,03,18,10,50,57),
                    ShutterSpeedDenominator = 1,
                    ShutterSpeedNumerator = 1
                },

                new Photo()
                {
                    CameraModel = String.Empty,
                    Description = "Beautiful Valencia with cool architecture",
                    Diaphragm = 0,
                    Flash = false,
                    LensFocalLength = 0,
                    Name = "19010bf9-d220-43fe-b6cc-012f53e114d3.jpg",
                    ISO = 0,
                    OriginalName = "image.jpg",
                    PhotoId = 1,
                    PhotoTakingPlace = "Spain, Valencia",
                    UpdateDateTime = new DateTime(2017,04,16,11,55,43),
                    ShutterSpeedDenominator = 1,
                    ShutterSpeedNumerator = 1,
                },

             };

            _albums = new List<Album>()
            {
                new Album()
                {
                    AlbumId = 1,
                    Description = "Spain Valencia",
                    Genre=Genres.Cities,
                    GenresId = 1,
                    Name = "Valencia",
                    UserId = 1,
                    UpdateDateTime = new DateTime(2018,04,19, 10,48,59),
                    Photos = new List<Photo>(){_photos[1],_photos[2]}
                },

                new Album()
                {
                    AlbumId = 2,
                    Description = "Valencia",
                    Genre=Genres.Cities,
                    GenresId = 1,
                    Name = "Gorgeous valencia",
                    UserId = 1,
                    UpdateDateTime = new DateTime(2013,07,20, 12,43,56),
                    Photos = new List<Photo>(){_photos[0],_photos[2]}
                },

            };

            _photos[0].Albums = new List<Album>() { _albums[1] };
            _photos[1].Albums = new List<Album>() { _albums[0] };
            _photos[2].Albums = new List<Album>() { _albums[0], _albums[1] };

            _users =new List<User>()
            {
                new User()
                {
                    Albums = _albums,
                    Password = "225679c9cb766413da2fa1b0ac3673ea",
                    UserId = 1,
                    Username = "admin",
                }
            };

            _albumMock = new Mock<IRepository<Album>>();
            _userMock = new Mock<IRepository<User>>();

            _albumMock
                .Setup(repo => repo.Get(It.IsAny<int>()))
                .Returns((int id) => GetStubAlbumById(id));

            _albumMock
                .Setup(repo => repo.Get(It.IsAny<Func<Album, Boolean>>()))
                .Returns((Func<Album, Boolean> predicate) => GetStubAlbumByAlbumName(predicate));

            _userMock
                .Setup(repo => repo.Get(It.IsAny<Func<User, Boolean>>()))
                .Returns((Func<User, Boolean> predicate) => GetStubUserByUserName(predicate));

            _albumGetInfoService = new AlbumGetInfoService(_userMock.Object, _albumMock.Object);
            _checkedAlbumName = "Gorgeous valencia";
            _checkedAlbumId = 1;
            _checkedUserName = "admin";
        }

        [TestMethod]
        public void GetAlbumByIdTest()
        {
            Album expectedResult = GetStubAlbumById(_checkedAlbumId);

            //Act
            Album actualResult = _albumGetInfoService.GetAlbumById(_checkedAlbumId);

            //Assert
            Assert.AreEqual(expectedResult.Name, actualResult.Name);
        }

        [TestMethod]
        public void GetAlbumByNameTest()
        {

            Album expectedResult = GetStubAlbumByAlbumName(a => a.Name.Equals(_checkedAlbumName, StringComparison.InvariantCultureIgnoreCase));

            //Act
            Album actualResult = _albumGetInfoService.GetAlbumByName(_checkedAlbumName);
            
            //Assert
            Assert.AreEqual(expectedResult.AlbumId, actualResult.AlbumId);
        }

        [TestMethod]
        public void GetAlbumByUserAndAlbumNamesTest()
        {

            Album expectedResult = GetStubUserByUserName(a => a.Username.Equals(_checkedUserName, StringComparison.InvariantCultureIgnoreCase))
                .Albums
                .First(a=>a.Name==_checkedAlbumName);

            //Act
            Album actualResult = _albumGetInfoService.GetAlbumByName(_checkedUserName,_checkedAlbumName);

            //Assert
            Assert.AreEqual(expectedResult.AlbumId, actualResult.AlbumId);
        }

        [TestMethod]
        public void GetAlbumNameByAlbumIdTest()
        {
            string expectedResult = GetStubAlbumNameByAlbumId(_checkedAlbumId);

            string actualResult = _albumGetInfoService.GetAlbumNameById(_checkedAlbumId);

            Assert.AreEqual(expectedResult,actualResult);
        }

        public string GetStubAlbumNameByAlbumId(Int32 id)
        {
            return _albums.FirstOrDefault(a => a.AlbumId == id)?.Name;
        }

        public Album GetStubAlbumById(Int32 id)
        {
            return _albums.FirstOrDefault(a => a.AlbumId == id);
        }

        public Album GetStubAlbumByAlbumName(Func<Album, Boolean> predicate)
        {
            return _albums.FirstOrDefault(predicate);
        }

        public User GetStubUserByUserName(Func<User, Boolean> predicate)
        {
            return _users.First(predicate);
        }

        private readonly string _checkedAlbumName;
        private readonly string _checkedUserName;
        private readonly int _checkedAlbumId;
        private readonly List<Photo> _photos;
        private readonly List<User> _users;
        private readonly List<Album> _albums;
        private Mock<IRepository<Album>> _albumMock;
        private readonly AlbumGetInfoService _albumGetInfoService;
        private readonly Mock<IRepository<User>> _userMock;
    }
}

