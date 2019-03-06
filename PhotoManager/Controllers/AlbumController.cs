using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Helpers;
using PhotoManager.DAL.Entities;
using PhotoManager.Helpers.Attributes;
using PhotoManager.Model.ViewModels;
using PhotoManager.Model.ViewModels.Mapping;
using Services;
using Services.Interfaces;

namespace PhotoManager.Controllers
{
    [AppAuthorize]
    public class AlbumController:Controller
    {
        private readonly IUserService _userService;
        private readonly IAlbumManagementService _albumManagementService;
        private readonly IPhotoManagementService _photoManagementService;
        private readonly IAlbumGetInfoService _albumGetInfoService;
        private readonly IPhotoGetInfoService _photoGetInfoService;

        private readonly IAdvancedMapper _advancedMapper;
        private readonly int _objectsPerPages;
        private readonly int _objectsPerRow;

        public AlbumController(IUserService userService, IAlbumGetInfoService albumGetInfoService, IPhotoGetInfoService photoGetInfoService,
            IAdvancedMapper advancedMapper, IAlbumManagementService albumManagementService, IPhotoManagementService photoManagementService)
        {
            _userService = userService;
            _albumGetInfoService = albumGetInfoService;
            _photoGetInfoService = photoGetInfoService;
            _advancedMapper = advancedMapper;
            _albumManagementService = albumManagementService;
            _photoManagementService = photoManagementService;
            _objectsPerPages = 20;
            _objectsPerRow = 5;
            ViewBag.ObjectsPerRow = _objectsPerRow;
        }

        //[HttpGet]
        //public ActionResult AlbumsEdit(int page = 1)
        //{
        //    var pagedAlbumViewModels = _albumGetInfoService
        //        .GetAlbumsByUserName(User.Identity.Name, _objectsPerPages, page)
        //        .Select(a=>_advancedMapper.MapAlbum(a, ImageSize.Small))
        //        .ToList();

        //    var albumViewModelIds = _albumGetInfoService.GetAllAlbumsId(User.Identity.Name).ToList();

        //    var ilvm=new ItemListViewModel<AlbumViewModel>(pagedAlbumViewModels, albumViewModelIds, new PageInfo(page,_objectsPerPages, albumViewModelIds.Count()));

        //    return View(ilvm);
        //}

        //[HttpGet]
        //public ActionResult AddExistingPhotosToAnotherAlbum(int albumId, int page = 1)
        //{
        //    var photos = _photoGetInfoService.GetAllPhotos(u=>u.Username==User.Identity.Name,a=>a.AlbumId!=albumId,_objectsPerPages,page);

        //    ItemListViewModel<PhotoViewModel> ilvm = new ItemListViewModel<PhotoViewModel>();
        //    if (!photos.Any())
        //    {
        //        ModelState.AddModelError("", "There are no existing photos that you can add.");
        //        return View(ilvm);
        //    }

        //    var albumName = _albumGetInfoService.GetAlbumNameById(albumId);

        //    var pagedPhotoViewModels = photos.Select(q => _advancedMapper.MapPhoto(q, ImageSize.Small, albumId, albumName)).ToList();

        //    var photoViewModelIds = _photoGetInfoService.GetAllPhotosId(u=>u.Username==User.Identity.Name, a=>a.AlbumId!=albumId).ToList();

        //    ilvm = new ItemListViewModel<PhotoViewModel>(pagedPhotoViewModels, photoViewModelIds, new PageInfo(page, _objectsPerPages, photoViewModelIds.Count()));
        //    return View(ilvm);
        //}

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddExistingPhotosToAnotherAlbum(int albumId, int[] itemsToAdd)
        {
            if (itemsToAdd == null)
            {
                ModelState.AddModelError("", "You didn`t choose any photo!");
                return View("AddExistingPhotosToAnotherAlbum");
            }

            if (!_userService.IsOwner(User.Identity.Name,albumId,itemsToAdd.First()))
            {
                return View("EnterForeignAccount");
            }

            foreach (var photoId in itemsToAdd)
            {
                _photoManagementService.CreatePhoto(photoId, albumId, User.Identity.Name);
            }

            return RedirectToAction("AlbumEdit", "Photo",new { albumId = albumId });
        }

        [HttpGet]
        public ActionResult UpdateAlbum(int albumId)
        {
            if (!_userService.IsOwner(User.Identity.Name,albumId))
            {
                return View("EnterForeignAccount");
            }

            Album album = _albumGetInfoService.GetAlbumById(albumId, User.Identity.Name);

            var albumViewModel = _advancedMapper.MapAlbum(album, ImageSize.Medium);
            return View(albumViewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult UpdateAlbum(AlbumViewModel albumViewModel)
        {
            if (!_userService.IsOwner(User.Identity.Name,albumViewModel.AlbumId))
            {
                return View("EnterForeignAccount");
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "You have inputed wrong data. Please, try again");
                return View(albumViewModel);
            }
            Album album = _advancedMapper.MapAlbumViewModel(albumViewModel);
            _albumManagementService.UpdateAlbum(album);
            return RedirectToAction("AlbumsOverview");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult RemoveAlbums(int[] itemsToDelete)
        {
            if (itemsToDelete == null)
            {
                ModelState.AddModelError("", "You didn`t choose any album!");
                return View("AlbumsEdit");
            }

            if (!_userService.IsOwner(User.Identity.Name,itemsToDelete.First()))
            {
                return View("EnterForeignAccount");
            }

            foreach (var id in itemsToDelete)
            {
                _albumManagementService.DeleteAlbum(id);
            }

            if (!_userService.AnyUserAlbums(User.Identity.Name))
            {
                return View("NoAlbums");
            }
            return RedirectToAction("AlbumsEdit");
        }

        [HttpGet]
        public ActionResult CreateAlbum()
        {
            var username = User.Identity.Name;
            User user = _userService.GetUserByName(username);
            AlbumViewModel albumViewModel = new AlbumViewModel();
            albumViewModel.UserId = user.UserId;
            return View(albumViewModel);
        }

        [HttpPost]
        public ActionResult CreateAlbum(AlbumViewModel albumViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "You have inputed the wrong data. Please try again!");
                return View(albumViewModel);
            }

            Album album = _albumGetInfoService.GetAlbumByName(User.Identity.Name,albumViewModel.Name);

            if (album == null)
            {
                album = _advancedMapper.MapAlbumViewModel(albumViewModel);
                _albumManagementService.CreateAlbum(album, User.Identity.Name);
            }
            else
            {
                ModelState.AddModelError("", "The album with such name already exists!");
                return View(albumViewModel);
            }

            return RedirectToAction("AlbumOverview","Photo", new { albumId = album.AlbumId });
        }

        [HttpGet]
        public ActionResult AlbumsOverview(int page = 1)
        {
            if (!_userService.AnyUserAlbums(User.Identity.Name))
            {
                return View("NoAlbums");
            }

            var pagedAlbumViewModels = _albumGetInfoService
                .GetAlbumsByUserName(User.Identity.Name, _objectsPerPages, page)
                .Select(a => _advancedMapper.MapAlbum(a, ImageSize.Small))
                .ToList();

            var albumViewModelIds = _albumGetInfoService.GetAllAlbumsId(User.Identity.Name).ToList();

            var ilvm = new ItemListViewModel<AlbumViewModel>(pagedAlbumViewModels, new PageInfo(page, _objectsPerPages, 50));
            return View(ilvm);
        }
    }
}