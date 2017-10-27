using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecLeagueBlog.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Posts()
        {
            ViewBag.Message = "Your blog posts page.";

            return View();
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
    }
}