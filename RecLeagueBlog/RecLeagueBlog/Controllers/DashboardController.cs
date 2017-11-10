using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RecLeagueBlog.Data;
using RecLeagueBlog.Models.Identity;
using RecLeagueBlog.Models;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using System.Web;
using Microsoft.AspNet.Identity.Owin;

namespace RecLeagueBlog.Controllers
{
    [Authorize(Roles = "admin, manager")]
    public class DashboardController : Controller
    {
        BlogManager manager = BlogManagerFactory.Create();

        [Authorize(Roles = "manager")]
        public ActionResult Index()
        {

            return View();
        }

        [Authorize(Roles = "manager")]
        public ActionResult Posts()
        {
            return View();
        }


        public ActionResult Categories()
        {

            return View();
        }

        public ActionResult Tags()
        {

            return View();
        }

        [HttpGet]
        public ActionResult Users()
        {
            var model = manager.GetAllUsers();
            return View(model);

        }

        [HttpGet]
        public ActionResult AddUser()
        {
            var model = new UserRoleViewModel();
            model.SetRoleItems(manager.GetAllRoles());
            return View(model);
        }

        [HttpPost]
        public ActionResult AddUser(UserRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                manager.ConvertVMtoUserForAdd(model);
                return RedirectToAction("Users");
                //throw new Exception("Error placeholder for now");
            }
            else
            {
                return View(model);
            }

        }

        [HttpGet]
        public ActionResult EditUser(string id)
        {
            var user = manager.GetUser(id);
            var model = manager.ConvertUserToVM(user);
            model.SetRoleItems(manager.GetAllRoles());
            return View(model);
        }

        [HttpPost]
        public ActionResult EditUser(UserRoleViewModel viewModel)
        {
            manager.ConvertVMtoUserForEdit(viewModel);
            return RedirectToAction("Users");
        }

        [HttpGet]
        public ActionResult DeleteUser(string id)
        {
            var user = manager.GetUser(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult DeleteUser(AppUser user)
        {
            manager.DeleteUser(user.Id);
            return RedirectToAction("Users");
        }

        public ActionResult Pages()
        {
            var model = manager.GetAllStaticPages();

            return View(model);
        }

        public ActionResult AddPages()
        {
            var model = new StaticPageViewModel();
            model.SetStatusItems(manager.GetAllStatuses());
            return View(model);
        }

        [HttpPost]
        public ActionResult AddPages(StaticPage staticPage)
        {
            if(!ModelState.IsValid)
            {
                throw new Exception("placeholder for error");
            }
            else
            {
                var model = new StaticPage();
                manager.CreateStaticPage(staticPage);
                return RedirectToAction("Pages");
            }
        }

        public ActionResult DeletePages(int id)
        {
            var page = manager.GetStaticPage(id);
            return View(page);
        }

        [HttpPost]
        public ActionResult DeletePages(StaticPage staticPage)

        {
            manager.DeleteStaticPage(staticPage.StaticPageId);
            return RedirectToAction("Pages");
        }

        [HttpGet]
        public ActionResult EditPages(int id)
        {
            StaticPage model = manager.GetStaticPage(id);
            //model.SetStatusItems(manager.GetAllStatuses());
            return View(model);
        }

        [HttpPost]
        public ActionResult EditPages(StaticPage editPage)
        {
            manager.EditStaticPage(editPage);
            return RedirectToAction("Pages");
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
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

            //return View("UpdatePassword");
        }
    }
}