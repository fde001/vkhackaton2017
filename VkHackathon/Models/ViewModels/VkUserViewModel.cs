using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VkHackathon.Models.ViewModels
{
    public class VkUserViewModel
    {
        public ulong uid { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
    }
}