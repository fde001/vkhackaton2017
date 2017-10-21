using System.Data.Entity.Spatial;

namespace VkHackathon.Models
{
    public class Company
    {
        public long Id { get; set; }
        public Source Source { get; set; }
        public EntityType EntityType { get; set; }
        public string ExternalId { get; set; }

        public string Name { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }

        public string Address { get; set; }
        public string Site { get; set; }
        public string Url { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public double Rating { get; set; }

        public DbGeography GeoIndex { get; set; }
        public string TextIndex { get; set; }

    }
}