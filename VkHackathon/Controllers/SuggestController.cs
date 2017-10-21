using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VkHackathon.Models;
using VkHackathon.Models.ViewModels;
using VkNet;

namespace VkHackathon.Controllers
{
    public class SuggestController : Controller
    {
        private MetadataDBDataContext db = new MetadataDBDataContext();

        // GET: Suggest
        [HttpPost]
        public ActionResult Random(long time, QueryViewModel place, VkUserViewModel user)
        {
            var dtTime = new DateTime(time);
            var placeId = place.Id;
            var userId = user.uid;
            var query = db.RandomQuery.Where(r => r.PlaceId == placeId && r.VisitTime == dtTime && r.Buddies == null);
            if (query.Count() >1)
            {
                var items = query.Take(2).ToList();
                var buddies = userId + "," + items[0].UserId + "," + items[1].UserId;

                items[0].Buddies = buddies;
                items[1].Buddies = buddies;

                db.RandomQuery.Add(new RandomQuery()
                {
                    Buddies = buddies,
                    PlaceId = placeId,
                    UserId = userId,
                    VisitTime = dtTime
                });

                var vk = new VkApi();
                vk.Authorize(new ApiAuthParams
                {
                    ApplicationId = 6227709,
                    AccessToken = "608c5c34fcca27bd26e7a1e2f3408d0674e5f6a40d2dc9a3e9415d67e1f149c1db2f492c727bd9f37f637"
                });

                var links = "https://vk.com/id" + userId + ", https://vk.com/id" + items[0].UserId + ", https://vk.com/id" + items[1].UserId;

                try
                {
                    vk.Messages.Send(
                        new VkNet.Model.RequestParams.MessagesSendParams()
                        {
                            UserIds = new long[] { (long)userId, (long)items[0].UserId, (long)items[1].UserId },
                            Message = @"Добрый день, приложение МЕСТА нашло для вас вариант компании для посещения " + place.Title + " ("+ place.Url + "):  " + links
                        });
                }
                catch { }

            } else
            {
                db.RandomQuery.Add(new RandomQuery()
                {
                    Buddies = null,
                    PlaceId = placeId,
                    UserId = userId,
                    VisitTime = dtTime
                });
            }


            db.SaveChanges();


            return Content("OK");
        }

        [HttpPost]
        public ActionResult Friends(long time, QueryViewModel place, List<long> friends)
        {
            var vk = new VkApi();
            vk.Authorize(new ApiAuthParams
            {
                ApplicationId = 6227709,
                AccessToken = "608c5c34fcca27bd26e7a1e2f3408d0674e5f6a40d2dc9a3e9415d67e1f149c1db2f492c727bd9f37f637"
            });


            var links = string.Join(", ", friends.Select(f => "https://vk.com/id" + f));
                

            try
            {
                vk.Messages.Send(
                    new VkNet.Model.RequestParams.MessagesSendParams()
                    {
                        UserIds = friends,
                        Message = @"Добрый день, предлагаем вам посетить совместно " + place.Title + " (" + place.Url + "):  " + links
                    });
            }
            catch { }

            return Content("OK");
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