using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using VkHackathon.Models;
using VkHackathon.Models.ViewModels;

namespace VkHackathon.Controllers
{
    public class QueryController : Controller
    {
        private MetadataDBDataContext db = new MetadataDBDataContext();

        // GET: api/Query/5
        public async Task<JsonResult> Index(double lat, double lon, double dist, string query)
        {
            var sourcePoint = DbGeography.FromText("POINT(" + lon + " " + lat + ")");

            var result = await db.Companies.Where(
                c => c.TextIndex.Contains(query) &&
                c.GeoIndex.Distance(sourcePoint) < dist
                )
            .OrderBy(loc => loc.GeoIndex.Distance(sourcePoint))
            .Take(500)
            .Select(i=> new QueryViewModel()
            {
                Id= "" + i.Id,
                Description = i.Description??"",
                Distance =i.GeoIndex.Distance(sourcePoint),
                Lat = i.Latitude,
                Lon = i.Longitude,
                Title = i.FullName,
                Site = i.Site,
                Url = i.Url,
                Rating = i.Rating + 0.001d
            })
            .ToListAsync();
            return
                Json(result
                , JsonRequestBehavior.AllowGet);
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