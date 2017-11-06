using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RecLeagueBlog.Data;
using RecLeagueBlog.Models.Identity;
using RecLeagueBlog.Models;
using RecLeagueBlog.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using RecLeagueBlog.Data.MockRepositories;

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

        [HttpGet]
        public ActionResult Users()
        {
            var repo = new MockUserRepository();
            var model = repo.GetAllUsers();
            return View(model);
        }

        [HttpGet]
        public ActionResult DeleteUser(string id)
        {
            var repo = new MockUserRepository();
            var user = repo.GetUser(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult DeleteUser(AppUser user)
        {
            var repo = new MockUserRepository();
            repo.DeleteUser(user.Id);
            return RedirectToAction("Users");
        }

        public ActionResult UserProfile()
        {
            ViewBag.Message = "Your user profile page.";

            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> ResetPassword(string userId, string newPassword, ResetPasswordModel model)
        {

            RecBlogDBContext context = new RecBlogDBContext();
            UserStore<AppUser> store = new UserStore<AppUser>(context);
            UserManager<AppUser> userManager = new UserManager<AppUser>(store);
            userId = User.Identity.GetUserId();
            //if (model.NewPassword != null && model.Email != null)
            if (ModelState.IsValid)
            {
                newPassword = model.NewPassword;
                string hashedNewPassword = userManager.PasswordHasher.HashPassword(newPassword);
                AppUser identityUser = await store.FindByIdAsync(userId);
                await store.SetPasswordHashAsync(identityUser, hashedNewPassword);
                await store.UpdateAsync(identityUser);

                TempData["PasswordReset"] = "Password has been successfully reset";
                return View("UserProfile");
            }
            return View("UserProfile");
        }

        public ActionResult UpdatePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePassword(UpdatePasswordModel model, string userId, string newPassword)
        {
            RecBlogDBContext context = new RecBlogDBContext();
            UserStore<AppUser> store = new UserStore<AppUser>(context);
            UserManager<AppUser> userManager = new UserManager<AppUser>(store);
            userId = User.Identity.GetUserId();

            //if ((model.NewPassword == null) && (model.ConfirmPassword == null))
            if (!ModelState.IsValid)
            {
                return View("UpdatePassword");
            }
            //else if (model.NewPassword == model.ConfirmPassword)
            else
            {
                newPassword = model.ConfirmPassword;
                string hashedNewPassword = userManager.PasswordHasher.HashPassword(newPassword);
                AppUser identityUser = await store.FindByIdAsync(userId);
                await store.SetPasswordHashAsync(identityUser, hashedNewPassword);
                await store.UpdateAsync(identityUser);

                TempData["PasswordUpdate"] = "Password has been successfully updated!";
                return View("UpdatePassword");
            }

            return View("UpdatePassword");
        }
    }
}