using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PBE.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {   
            System.Web.HttpContext.Current.Session["user"] = ViewBag.user;
            System.Web.HttpContext.Current.Session["Identite"] = ViewBag.Identite;
            System.Web.HttpContext.Current.Session["Poste"] = ViewBag.Poste;
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
    }
}