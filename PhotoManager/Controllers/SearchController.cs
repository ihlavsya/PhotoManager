using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Helpers;
using PhotoManager.Model.ViewModels;
using PhotoManager.Model.ViewModels.Mapping;
using Services.Interfaces;

namespace PhotoManager.Controllers
{
    public class SearchController : Controller
    {
        private readonly IPhotoGetInfoService _photoGetInfoService;

        private readonly IAdvancedMapper _advancedMapper;

        private readonly List<string> _diaphragmList;
        private readonly int _objectsPerPages;
        private readonly int _objectsPerRow;

        public SearchController(IPhotoGetInfoService photoGetInfoService, IAdvancedMapper advancedMapper)
        {
            _advancedMapper = advancedMapper;
            _photoGetInfoService = photoGetInfoService;
            _diaphragmList = new List<string> {"diaphragm","f/1.4", "f/2", "f/2.8", "f/4", "f/5.6", "f/8", "f/11", "f/16", "f/22"};
            _objectsPerPages = 20;
            _objectsPerRow = 5;
            ViewBag.ObjectsPerRow = _objectsPerRow;
        }

        [HttpGet]
        public ActionResult ForAdvancedSearch()
        {
            var photoViewModel = new PhotoViewModel();
            ViewBag.DiaphragmList = _diaphragmList;
            return View(photoViewModel);
        }

        //[HttpGet]
        //public ActionResult AdvancedSearch(PhotoViewModel photoViewModel, int page = 1)//searchQuery
        //{
        //    ViewBag.SearchViewModel = photoViewModel;
        //    var photo = _advancedMapper.MapPhotoViewModel(photoViewModel);
        //    var pagedPhotoViewModels = _photoGetInfoService.AdvancedSearchPhotos(photo,page,_objectsPerPages)
        //        .Select(q => _advancedMapper.MapPhoto(q, ImageSize.Small)).ToList();

        //    var photoViewModelIds =_photoGetInfoService.AdvancedSearchPhotosIds(photo).ToList();

        //    var ilvm = new ItemListViewModel<PhotoViewModel>(pagedPhotoViewModels, photoViewModelIds, new PageInfo(page, _objectsPerPages, photoViewModelIds.Count()));

        //    ViewBag.DiaphragmList = _diaphragmList;

        //    return View("AdvancedSearchResult", ilvm);
        //}

        //[HttpGet]
        //public ActionResult SearchResult(string searchQuery, int page = 1)
        //{
        //    ViewBag.SearchQuery = searchQuery;
        //    var pagedPhotoViewModels = new List<PhotoViewModel>();
        //    var photoViewModelIds = _photoGetInfoService.FindPhotosIdsByDescriptionQuery(searchQuery);
        //    if (!String.IsNullOrEmpty(searchQuery))
        //    {
        //        pagedPhotoViewModels = _photoGetInfoService.FindPhotosByDescriptionQuery(searchQuery,page, _objectsPerPages)
        //            .Select(q => _advancedMapper.MapPhoto(q, ImageSize.Small)).ToList();
        //    }

        //    var ilvm = new ItemListViewModel<PhotoViewModel>(pagedPhotoViewModels, photoViewModelIds, new PageInfo(page, _objectsPerPages, photoViewModelIds.Count()));

        //    ViewBag.DiaphragmList = _diaphragmList;

        //    return View(ilvm);
        //}
    }
}