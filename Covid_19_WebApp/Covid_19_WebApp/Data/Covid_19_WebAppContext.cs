using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Covid_19_WebApp.Models;

namespace Covid_19_WebApp.Data
{
    public class Covid_19_WebAppContext : DbContext
    {
        public Covid_19_WebAppContext (DbContextOptions<Covid_19_WebAppContext> options)
            : base(options)
        {
        }

        public DbSet<Covid_19_WebApp.Models.Citizen>? Citizen { get; set; }

        public DbSet<Covid_19_WebApp.Models.Manager>? Manager { get; set; }
    }
}
