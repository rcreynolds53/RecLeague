using RecLeagueBlog.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace RecLeagueBlog.Controllers
{
    public class StaticController : Controller
    {
        BlogManager manager = BlogManagerFactory.Create();
        public ActionResult Pages()
        {
            var model = manager.GetAllPosts();

            return View(model);
        }

        public ActionResult AddPages()
        {
            return View();
        }

        public ActionResult DeletePages()
        {
            return View();
        }

        public ActionResult EditPages()
        {
            return View();
        }

    }
}