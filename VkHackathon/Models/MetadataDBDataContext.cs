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

        public DbSet<Schedule> Schedule { get; set; }

        public DbSet<TicketSystem> TicketSystems { get; set; }
    }
}