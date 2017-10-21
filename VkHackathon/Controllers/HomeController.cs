using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VkHackathon.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("IndexMobile");
        }

        public ActionResult IndexMobile()
        {
            return View();
        }

        
    }
}