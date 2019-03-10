using Blog.BLL.Identity;
using Blog.Entity.Identity;
using Blog.Entity.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Mvc_IdentityOwin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.PL.Controllers
{
    public class AccountController : BaseController
    {
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]    
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var usermanager = IdentityTools.NewUserManager();
            //var kullanici = usermanager.FindByName(model.Username);
            var kullanici = usermanager.FindByEmail(model.Email);
            if (kullanici != null)
            {
                ModelState.AddModelError("", "Bu email sistemde kayıtlı!");
                return View(model);
            }
            ApplicationUser user = new ApplicationUser();
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Email = model.Email;
            user.UserName = model.Username;

            var result = usermanager.Create(user, model.Password);

            if (result.Succeeded)
            {
                usermanager.AddToRole(user.Id, "Admin");
                return RedirectToAction("Login");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }
            return View(model);
        }
        public ActionResult Login(string returnUrl)
        {
            LoginViewModel model = new LoginViewModel() { returnUrl = returnUrl };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var usermanager = IdentityTools.NewUserManager();
            var kullanici = usermanager.FindByName(model.Username);
            if (kullanici == null)
            {
                ModelState.AddModelError("", "Böyle bir kullanıcı kayıtlı değil!");
                return View(model);
            }
            else
            {
                if (!usermanager.CheckPassword(kullanici, model.Password))
                {
                    ModelState.AddModelError("", "Girilen şifre yanlış!");
                    return View(model);
                }
                else
                {

                var authManager = HttpContext.GetOwinContext().Authentication;
                var identity = usermanager.CreateIdentity(kullanici, "ApplicationCookie");
                var authProperty = new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe
                };
                authManager.SignIn(authProperty, identity);
                return Redirect(string.IsNullOrEmpty(model.returnUrl) ? "/" : model.returnUrl);
                }
            }
        }
        [Authorize]
        public ActionResult LogOut()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}