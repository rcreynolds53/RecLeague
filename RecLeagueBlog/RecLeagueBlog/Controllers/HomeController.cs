using RecLeagueBlog.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecLeagueBlog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Blog()
        {
            var repo = new MockPostRepository();
            var model = repo.GetAllPosts();
            return View(model);
        }

        public ActionResult Post(int id)
        {
            var repo = new MockPostRepository();
            var model = repo.GetPostById(id);
            return View(model);
        }
    }
}