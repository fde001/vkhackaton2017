using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VkHackathon.Models.ViewModels;

namespace VkHackathon.Controllers
{
    public class SuggestController : Controller
    {
        // GET: Suggest
        [HttpPost]
        public ActionResult Random(long time, QueryViewModel place, VkUserViewModel user)
        {
            return Content("OK");
        }

        [HttpPost]
        public ActionResult Friends(long time, QueryViewModel place, List<ulong> friends)
        {
            return Content("OK");
        }
    }

   
}