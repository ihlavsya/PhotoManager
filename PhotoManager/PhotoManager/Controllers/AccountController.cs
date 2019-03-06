﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services;
using PhotoManager.ViewModels;
using System.Web.Security;

namespace PhotoManager.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid) return View(model);

            var userValid = _userService.IsLoginDataCorrect(model.Username, model.Password);
            if (userValid)
            {
                FormsAuthentication.SetAuthCookie(model.Username, false);
                if (Url.IsLocalUrl(returnUrl)
                    && returnUrl.Length > 1
                    && returnUrl.StartsWith("/")
                    && !returnUrl.StartsWith("//")
                    && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("ResultManagement", "Admin");
                }
            }
            else
            {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }
    }
}