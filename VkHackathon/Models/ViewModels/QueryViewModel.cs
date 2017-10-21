using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VkHackathon.Models.ViewModels
{
    public class QueryViewModel
    {
        public string Id { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public double? Distance { get; set; }
        public string Icon { get; set; }                    
        public string Photo { get; set; }
        public string Description { get; set; }
        public int Members { get; set; }
        public string Site { get; set; }
        
    }
}