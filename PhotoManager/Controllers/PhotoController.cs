using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Helpers;
using PhotoManager.DAL.Entities;
using PhotoManager.Helpers;
using PhotoManager.Helpers.Attributes;
using PhotoManager.Model.ViewModels;
using PhotoManager.Model.ViewModels.Mapping;
using Services;
using Services.Interfaces;

namespace PhotoManager.Controllers
{
    [AppAuthorize]
    public class PhotoController:Controller
    {
        private readonly IUserService _userService;
        private readonly IPhotoGetInfoService _photoGetInfoService;
        private readonly IAlbumGetInfoService _albumGetInfoService;

        private readonly IAdvancedMapper _advancedMapper;
        private readonly IPhotoManagementService _photoManagementService;
        private readonly int _objectsPerPages;
        private readonly List<string> _diaphragmList;
        private readonly int _objectsPerRow;

        public PhotoController(IUserService userService, IPhotoGetInfoService photoGetInfoService, IAlbumGetInfoService albumGetInfoService
            , IAdvancedMapper advancedMapper, IPhotoManagementService photoManagementService)
        {
            _userService = userService;
            _photoGetInfoService = photoGetInfoService;

            _advancedMapper = advancedMapper;
            _photoManagementService = photoManagementService;
            _albumGetInfoService = albumGetInfoService;
            _objectsPerPages = 20;
            _diaphragmList=new List<string>(){ "diaphragm","f/1.4", "f/2", "f/2.8", "f/4", "f/5.6", "f/8", "f/11", "f/16", "f/22" };

            _objectsPerRow = 5;
            ViewBag.ObjectsPerRow = _objectsPerRow;
        }

        [HttpGet]
        public ActionResult PhotoUpload(int albumId)
        {
            if (!_userService.IsOwner(User.Identity.Name,albumId))
            {
                return View("EnterForeignAccount");
            }

            var files = new FilesViewModel();
            files.AlbumId = albumId;
            return View(files);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult PhotoUpload(FilesViewModel files)
        {
            if (!_userService.IsOwner(User.Identity.Name,files.AlbumId))
            {
                return View("EnterForeignAccount");
            }

            var filesList = files.Files.ToList();
            for (int i=0;i<filesList.Count();i++)
            {
                var file = filesList[i];
                var allowedExtensions = new[] { ".jpg", ".jpeg" };
                var extension = Path.GetExtension(file.FileName);
                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("", String.Format("You used the file with incorrect extension. You have added only first {0} photos",i));
                    return View(files);
                }

                if (file != null)
                {
                    var photoViewModel = new PhotoViewModel();
                    var photo = _advancedMapper.MapPhotoViewModel(photoViewModel);
                    photo.OriginalName = file.FileName;
                    _photoManagementService.CreatePhoto(photo, file, files.AlbumId);
                }
            }

            // after successfully uploading redirect the user
            return RedirectToAction("AlbumEdit", "Photo", new { albumId = files.AlbumId });
        }

        //[HttpGet]
        //public ActionResult AlbumEdit(int albumId, int page = 1)
        //{
        //    if (!_userService.IsOwner(User.Identity.Name,albumId))
        //    {
        //        return View("EnterForeignAccount");
        //    }

        //    if (!_albumGetInfoService.DoesAlbumExist(albumId))
        //    {
        //        return View("NoAlbum");
        //    }
        //    ViewBag.PhotosCount = _albumGetInfoService.CountPhotos(User.Identity.Name,a=>a.AlbumId!=albumId);

        //    var albumName = _albumGetInfoService.GetAlbumNameById(albumId);

        //    var pagedPhotoViewModels = _photoGetInfoService.GetAllPhotos(u=>u.Username==User.Identity.Name, a=>a.AlbumId==albumId,_objectsPerPages,page)
        //        .Select(q => _advancedMapper.MapPhoto(q, ImageSize.Small, albumId, albumName))
        //        .ToList();

        //    var photoViewModelIds = _photoGetInfoService.GetAllPhotosId(u=>u.Username==User.Identity.Name, a=>a.AlbumId==albumId).ToList();

        //    var ilvm = new ItemListViewModel<PhotoViewModel>(pagedPhotoViewModels, photoViewModelIds, new PageInfo(page, _objectsPerPages, photoViewModelIds.Count()));
        //    return View(ilvm);
        //}

        //[AllowAnonymous]
        //[HttpGet]
        //public ActionResult PhotoSlider(string photosId, int photoId)
        //{
        //    ViewBag.IsOwner = _userService.IsOwner(photoId, User.Identity.Name);

        //    var photosIds = System.Web.Helpers.Json.Decode<List<int>>(photosId);

        //    var photo = _photoGetInfoService.GetPhotoById(photoId);
        //    var photoViewModel = _advancedMapper.MapPhoto(photo, ImageSize.Original);

        //    var index = photosIds.FindIndex(x => x == photoId);

        //    int nextIndex = 0;
        //    if (index + 1 < photosIds.Count)
        //    {
        //        nextIndex = index + 1;
        //    }

        //    int previousIndex = photosIds.Count - 1;
        //    if (index - 1 > 0)
        //    {
        //        previousIndex = index - 1;
        //    }

        //    var photoSliderViewModel = new PhotoSliderViewModel()
        //    {
        //        ActivePhoto = photoViewModel,
        //        AllPhotosIds = photosIds,
        //        NextPhotoId = photosIds[nextIndex],
        //        PreviousPhotoId= photosIds[previousIndex],
        //    };

        //    return View(photoSliderViewModel);
        //}

        [AllowAnonymous]
        [HttpGet]
        public ActionResult PhotoSlider(int albumId, int itemPosition)// можно не использовать itemPosition skipwhile
        {
            var photoSliderViewModel = new PhotoSliderViewModel();
            Album album = _albumGetInfoService.GetAlbumById(albumId, User.Identity.Name);
            if (album == null)
            {
                photoSliderViewModel.IsUserOwner = false;
            }
            else
            {
                photoSliderViewModel.IsUserOwner=true;
            }
              

            List<PhotoViewModel> photoSlider=new List<PhotoViewModel>();

            int i = itemPosition-1;
            if(i==-1)
            {
                i = album.Photos.Count-1;
            }
            photoSlider = _photoGetInfoService.GetThreePhotos(User.Identity.Name, albumId, itemPosition)
                    .Select(q => _advancedMapper.MapPhoto(q, ImageSize.Small, album.AlbumId,album.Name,i++)).ToList();


            photoSliderViewModel.PreviousPhoto = photoSlider[0];
            photoSliderViewModel.ActivePhoto = photoSlider[1];
            photoSliderViewModel.NextPhoto = photoSlider[2];

            return View(photoSliderViewModel);
        }

        [HttpGet]
        public ActionResult AlbumOverview(int albumId, int page = 1)
        {

            Album album = _albumGetInfoService.GetAlbumById(albumId,User.Identity.Name);

            if (album == null)
            {
                return View("EnterForeignAccount");
            }

            if (album.Photos.Count==0)
            {
                return View("NoPhotos", albumId);
            }
            int i = 0;
            var pagedPhotoViewModels = _photoGetInfoService.GetAllPhotos(User.Identity.Name, albumId, _objectsPerPages, page)
                .Select(q => _advancedMapper.MapPhoto(q, ImageSize.Small, albumId, album.Name, i++))
                .ToList();

            var ilvm = new ItemListViewModel<PhotoViewModel>(pagedPhotoViewModels, new PageInfo(page, _objectsPerPages,album.Photos.Count));
            return View(ilvm);
        }

        [HttpGet]
        public ActionResult UpdatePhoto(int id)
        {
            var username = User.Identity.Name;
            User user = _userService.GetUserByName(username);
            var photo = _photoGetInfoService.GetPhotoById(id, user.UserId);
            if (photo == null)
            {
                return View("EnterForeignAccount");//view +status code
            }

            ViewBag.DiaphragmList = _diaphragmList;

            var photoView = _advancedMapper.MapPhoto(photo, ImageSize.Medium);
            return View(photoView);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult UpdatePhoto(PhotoViewModel photoViewModel)
        {
            if (!_userService.IsOwner(photoViewModel.PhotoId, User.Identity.Name))
            {
                return View("EnterForeignAccount");
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "You have inputed wrong data");
                return View(photoViewModel);
            }

            if (photoViewModel.ShutterSpeed.Numerator <= 0)
            {
                ModelState.AddModelError("", "You can`t set negative number or 0 to numerator");
                return View(photoViewModel);
            }

            var photo = _advancedMapper.MapPhotoViewModel(photoViewModel);
            _photoManagementService.UpdatePhoto(photo);
            return RedirectToAction("AlbumsEdit", "Album");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult RemovePhotos(int[] itemsToDelete)
        {
            if (itemsToDelete == null)
            {
                ModelState.AddModelError("", "You didn`t choose any photo!");
                return View("AlbumEdit");
            }

            if (!_userService.IsOwner(itemsToDelete.First(),User.Identity.Name))
            {
                return View("EnterForeignAccount");
            }

            foreach (var id in itemsToDelete)
            {
                _photoManagementService.DeletePhoto(id);
            }

            return RedirectToAction("AlbumsEdit", "Album");
        }

        //[AllowAnonymous]
        //[HttpGet]
        //public ActionResult PublicAlbumOverview(string name, int page = 1)
        //{
        //    var albumId = _albumGetInfoService.GetAlbumIdByName(name);
        //    var pagedPhotoViewModels = _photoGetInfoService.GetAllPhotos(a=>a.Name==name,_objectsPerPages,page)
        //        .Select(q => _advancedMapper.MapPhoto(q, ImageSize.Small, albumId, name))
        //        .ToList();

        //    var photoViewModelIds = _photoGetInfoService.GetAllPhotosId(a=>a.Name==name).ToList();

        //    var ilvm = new ItemListViewModel<PhotoViewModel>(pagedPhotoViewModels, photoViewModelIds, new PageInfo(page, _objectsPerPages, photoViewModelIds.Count()));

        //    return View(ilvm);
        //}
    }
}