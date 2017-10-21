using System;

namespace VkHackathon.Models
{
    public class Schedule
    {
        public long Id { get; set; }
        public Source Source { get; set; }
        public Creation Creation { get; set; }
        public Company Company { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}