using RecLeagueBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult UserProfile()
        {
            ViewBag.Message = "Your user profile page.";

            return View();
        }

        //public ActionResult ResetPasswordToken()
        //{
        //    return View();
        //}

        public ActionResult ResetPassword()
        {
            //ResetPasswordModel model = new ResetPasswordModel
            //{
            //};

            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            return View();
        }
    }
}