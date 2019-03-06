using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Services;
using System.Data.Entity;
using PhotoManager.DAL.Interfaces;
using PhotoManager.DAL.EF;
using PhotoManager.DAL.Entities;
using PhotoManager.DAL.Repositories;
using AutoMapper;
using PhotoManager.Helpers.Attributes;
using PhotoManager.Model.ViewModels;

namespace PhotoManager.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult LoginOrRegister()
        {
            if (_userService.DoesUserExist(User.Identity.Name))
            {
                return RedirectToAction("AlbumsOverview", "Album");
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid) return View(model);

            if (_userService.IsLoginDataCorrect(model.Username, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.Username, false);//why do i need this? - for setting info to cookie - user shouldn`t login all the time
                return RedirectToAction("AlbumsOverview", "Album");
            }
            else
            {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(String.Empty, "Your data is wrong, please try again.");
            }

            if (!_userService.ValidatePassword(model.Password))
            {
                ModelState.AddModelError(String.Empty, "Your password is invalid, please try again.");
            }

            if (!_userService.DoesUserExist(model.Username))
            {
                // create new user
                var user = _mapper.Map<User>(model);
                _userService.CreateUser(user);//some problems should be resolve inside
                FormsAuthentication.SetAuthCookie(model.Username, true);
                return RedirectToAction("AlbumsOverview", "Album");
            }
            else
            {
                ModelState.AddModelError(String.Empty, "User with such username already exists");
            }
            return View(model);
        }

        [AppAuthorize]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }
    }
}