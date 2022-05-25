using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Covid_19_WebApp_Project.Models;

namespace Covid_19_WebApp_Project.Data
{
    public class Covid_19_WebApp_ProjectContext : DbContext
    {
        public Covid_19_WebApp_ProjectContext (DbContextOptions<Covid_19_WebApp_ProjectContext> options)
            : base(options)
        {
        }

        public DbSet<Covid_19_WebApp_Project.Models.CitizenModel>? CitizenModel { get; set; }
    }
}
