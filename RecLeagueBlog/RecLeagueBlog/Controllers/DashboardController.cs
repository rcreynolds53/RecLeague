using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RecLeagueBlog.Data;
using RecLeagueBlog.Models;
using RecLeagueBlog.Models.Identity;
﻿using RecLeagueBlog.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RecLeagueBlog.Controllers
{
    [Authorize(Roles = "admin")]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to Rec League Sports.";

            return View();
        }

        public ActionResult Posts()
        {
            var repo = new MockPostRepository();
            var model = repo.GetAllPosts();
            return View(model);
        }

        public ActionResult Pages()
        {
            ViewBag.Message = "Your static pages page.";

            return View();
        }

        public ActionResult Categories()
        {
            ViewBag.Message = "Your categories page.";

            return View();
        }

        public ActionResult Tags()
        {
            ViewBag.Message = "Your tags page.";

            return View();
        }

        public ActionResult Users()
        {
            ViewBag.Message = "Your users page.";

            return View();
        }

        public ActionResult UserProfile()
        {
            ViewBag.Message = "Your user profile page.";

            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult ResetPassword()
        {
            //ResetPasswordModel model = new ResetPasswordModel
            //{
            //};

            return View();
        }

        [HttpPost]
        [Authorize(Roles ="admin")]
        public async Task<ActionResult> ResetPassword(string userId, string newPassword)
        {

            RecBlogDBContext context = new RecBlogDBContext();
            UserStore<IdentityUser> store = new UserStore<IdentityUser>(context);
            UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(store);
            userId = User.Identity.GetUserId();
            newPassword = "Test123!";
            string hashedNewPassword = userManager.PasswordHasher.HashPassword(newPassword);
            IdentityUser identityUser = await store.FindByIdAsync(userId);
            await store.SetPasswordHashAsync(identityUser, hashedNewPassword);
            await store.UpdateAsync(identityUser);



            return View();
        }

        public ActionResult UpdatePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdatePassword(UpdatePasswordModel model)
        {


            return View();
        }
    }
}