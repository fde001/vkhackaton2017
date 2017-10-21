using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VkNet;

namespace VkHackathon.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            var vk = new VkApi();

            //vk.Authorize(6228899, "79196125192", "spbgasu", VkNet.Enums.Filters.Settings.Messages);

            vk.Authorize(new ApiAuthParams
            {
                ApplicationId = 6227709,
            //    Login = ,
            //    Password = ,
            //    Settings = VkNet.Enums.Filters.Settings.All
                
             AccessToken = //"0dc8bbbc0dc8bbbc0dc8bbbca60d97bd4100dc80dc8bbbc5422b32e100687d30e831066"

                "608c5c34fcca27bd26e7a1e2f3408d0674e5f6a40d2dc9a3e9415d67e1f149c1db2f492c727bd9f37f637"
            });

           // var testList = new List<ulong>();
           // testList.Add(901194);
           // testList.Add(185014513);
           // var test = vk.Users.Get("fde001");
            //var test3 = vk.Messages.CreateChat(testList, "test");
            var test2 = vk.Messages.Send(
                new VkNet.Model.RequestParams.MessagesSendParams()
                {
                    UserIds = new long[] { 901194, 440354743 },
                    Message = "test"
                });
                
                //901194, false, "test");
            
            return Content("OK");
        }
    }
}