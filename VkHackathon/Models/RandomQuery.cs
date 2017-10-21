using System;

namespace VkHackathon.Models
{
    public class RandomQuery
    {
        public long Id { get; set; }
        public DateTime VisitTime { get; set; }
        public long UserId { get; set; }
        public string Buddies { get; set; }
        public string PlaceId { get; set; }
    }
}