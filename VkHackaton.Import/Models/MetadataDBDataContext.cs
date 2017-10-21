using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace VkHackathon.Models
{
    public class MetadataDBDataContext: DbContext
    {

        public DbSet<Company> Companies { get; set; }
        public DbSet<Creation> Creations { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<EntityType> EntityTypes { get; set; }


        public DbSet<Schedule> Schedule { get; set; }
    }
}