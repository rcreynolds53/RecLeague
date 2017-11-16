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
using System.Linq;

namespace RecLeagueBlog.Controllers
{
    [Authorize(Roles = "admin, manager")]
    public class DashboardController : Controller
    {
        BlogManager manager = BlogManagerFactory.Create();

        [Authorize(Roles = "admin, manager")]
        public ActionResult Index()
        {

            return View();
        }

        [Authorize(Roles = "admin, manager")]
        public ActionResult Posts()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult Categories()
        {

            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult Tags()
        {

            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Users()
        {
            var model = manager.GetAllUsers();
            return View(model);

        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult AddUser()
        {
            var model = new UserRoleViewModel();
            model.SetRoleItems(manager.GetAllRoles());
            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult AddUser(UserRoleViewModel model)
        {
            model.AppUser.UserName = model.AppUser.Email;
            var context = new RecBlogDBContext();
            var userMgr = new UserManager<AppUser>(new UserStore<AppUser>(context));
            var roleMgr = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleMgr.RoleExists("admin"))
            {
                roleMgr.Create(new IdentityRole() { Name = "admin" });
            }

            if (!roleMgr.RoleExists("manager"))
            {
                roleMgr.Create(new IdentityRole() { Name = "manager" });
            }

            if (userMgr.FindByName(model.AppUser.UserName) == null)
            {
                var newuser = new AppUser()
                {
                    FirstName = model.AppUser.FirstName,
                    LastName = model.AppUser.LastName,
                    UserName = model.AppUser.UserName,
                    Email = model.AppUser.Email

                };
                userMgr.Create(newuser, model.NewPassword);
            }

            var user = userMgr.FindByName(model.AppUser.UserName);
            var role = context.Roles.SingleOrDefault(r => r.Id == model.Role.Id);
            userMgr.AddToRole(user.Id, role.Name);
            context.SaveChanges();
            return RedirectToAction("Users");

        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult EditUser(string id)
        {
            var user = manager.GetUser(id);
            var model = manager.ConvertUserToVM(user);
            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult EditUser(UserRoleViewModel viewModel)
        {
            manager.ConvertVMtoUserForEdit(viewModel);
            return RedirectToAction("Users");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult DeleteUser(string id)
        {
            var user = manager.GetUser(id);
            return View(user);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult DeleteUser(AppUser user)
        {
            manager.DeleteUser(user.Id);
            return RedirectToAction("Users");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Pages()
        {
            var model = manager.GetAllStaticPages();

            return View(model);
        }

        [Authorize(Roles = "admin")]
        public ActionResult AddPages()
        {
            var model = new StaticPageViewModel();
            model.SetStatusItems(manager.GetAllStatuses());
            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult AddPages(StaticPageViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                throw new Exception("placeholder for error");
            }
            else
            {
                manager.ConvertVmToPage(viewModel);
                return RedirectToAction("Pages");
            }
        }

        //public ActionResult DeletePages(int id)
        //{
        //    var page = manager.GetStaticPage(id);
        //    return View(page);
        //}

        //[HttpPost]
        //public ActionResult DeletePages(StaticPage staticPage)

        //{
        //    manager.DeleteStaticPage(staticPage.StaticPageId);
        //    return RedirectToAction("Pages");
        //}

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult EditPages(int id)
        {
            var page = manager.GetStaticPage(id);
            var model = manager.ConvertPageToVm(page);
            model.SetStatusItems(manager.GetAllStatuses());
            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult EditPages(StaticPageViewModel editedPage)
        {
            manager.ConvertVmToPage(editedPage);
            return RedirectToAction("Pages");
        }

        [Authorize(Roles = "admin")]
        public ActionResult ResetPassword()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
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
                return View("ResetPassword");
            }
            return View("ResetPassword");
        }

        [Authorize(Roles = "admin, manager")]
        public ActionResult UpdatePassword()
        {
            return View();
        }

        [Authorize(Roles = "admin, manager")]
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