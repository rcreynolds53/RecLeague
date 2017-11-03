using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecLeagueBlog.Models.Identity;
using RecLeagueBlog.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace RecLeagueBlog.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            LoginModel model = new LoginModel
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var ctx = Request.GetOwinContext();
            var authMgr = ctx.Authentication;
            var userMgr = HttpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();
            var user = userMgr.Find(model.Email, model.Password);

            if(user == null)
            {
                return Redirect(Url.Action("Login", "Auth"));
            }

            var userToLogin = userMgr.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            authMgr.SignIn(userToLogin);

            if(string.IsNullOrEmpty(model.ReturnUrl) || !Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(Url.Action("Index", "Dashboard"));
            }
            return Redirect(model.ReturnUrl);
        }

        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authmgr = ctx.Authentication;

            authmgr.SignOut("ApplicationCookie");

            return RedirectToAction("Login");
        }
    }
}