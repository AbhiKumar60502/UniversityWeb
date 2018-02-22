using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityWeb.Areas.UniversityHome.ViewModels;

namespace UniversityWeb.Areas.UniversityHome.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/Home/

        public ActionResult Index()
        {
            return View("~/Areas/UniversityHome/Views/Index.cshtml");

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View("~/Areas/UniversityHome/Views/About.cshtml");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Shell()
        {
            return View("~/Areas/UniversityHome/Views/Shell.cshtml", new HomeViewModel());

        }

    }
}
