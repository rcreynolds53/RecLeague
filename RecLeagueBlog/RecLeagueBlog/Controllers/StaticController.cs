using RecLeagueBlog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
//using System.Web.Http;
using System.Web.Mvc;
using System.Threading.Tasks;
using RecLeagueBlog.Models;

namespace RecLeagueBlog.Controllers
{
    public class StaticController : Controller
    {
        BlogManager manager = BlogManagerFactory.Create();

        public ActionResult Pages()
        {
            var model = manager.GetAllStaticPages();

            return View(model);
        }

        [HttpGet]
        public ActionResult AddPage()
        {
            var model = new StaticPage();
            //model.SetRoleItems(manager.GetAllRoles());
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

        public ActionResult EditPages()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DeletePage(int id)
        {
            var page = manager.GetStaticPage(id);
            return View(page);
        }

        [HttpPost]
        public ActionResult DeletePage(StaticPage page)
        {
            manager.DeleteStaticPage(page.StaticPageId);
            return RedirectToAction("Pages");
        }
    }
}