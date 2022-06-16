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

        public DbSet<Covid_19_WebApp.Models.VACCINE_REGISTRATION_FORM>? VACCINE_REGISTRATION_FORM { get; set; }

        public DbSet<Covid_19_WebApp.Models.WareHouse>? WareHouse { get; set; }

        public DbSet<Covid_19_WebApp.Models.Vaccine>? Vaccine { get; set; }

        public DbSet<Covid_19_WebApp.Models.Nurse>? Nurse { get; set; }

        public DbSet<Covid_19_WebApp.Models.Supplier>? Supplier { get; set; }

        public DbSet<Covid_19_WebApp.Models.Vaccine_Entry>? Vaccine_Entry { get; set; }

        public DbSet<Covid_19_WebApp.Models.Warehouse_Vaccine>? Warehouse_Vaccine { get; set; }

        public DbSet<Covid_19_WebApp.Models.Entry_Detail>? Entry_Detail { get; set; }

        public DbSet<Covid_19_WebApp.Models.Vaccine_Record>? Vaccine_Record { get; set; }
    }
}
