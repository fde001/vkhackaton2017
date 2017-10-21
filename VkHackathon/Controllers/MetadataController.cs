using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VkHackathon.Models;

namespace VkHackathon.Controllers
{
    public class MetadataController : Controller
    {
        private MetadataDBDataContext db = new MetadataDBDataContext();

        // GET: Ticketing
        public ActionResult Ticketing()
        {
            return Json(db.TicketSystems.ToList(), JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}