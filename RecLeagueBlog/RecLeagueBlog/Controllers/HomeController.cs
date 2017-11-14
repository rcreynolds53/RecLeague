using RecLeagueBlog.Data;
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
        BlogManager manager = BlogManagerFactory.Create();

        public ActionResult Index()
        {
            var model = manager.GetThreeRecent();
            return View(model);
        }

        public ActionResult Blog()
        {
            var model = manager.GetAllPosts();
            return View(model);
        }

        public ActionResult Post(int id)
        {
            var model = manager.GetPost(id);
            return View(model);
        }
        [Route("/StaticPage/{id}")]
        public ActionResult StaticPage(int id)
        {
           var page = manager.GetStaticPage(id);
            return Content(page.Content);
        }
    }
}