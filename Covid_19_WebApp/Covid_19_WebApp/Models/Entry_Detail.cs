using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Covid_19_WebApp.Models
{
    public class Entry_Detail
    {
        [Key]
        [DisplayName("Vaccine ID")]
        public int VaccineID { get; set; }

        [DisplayName("Entry ID")]
        public int EntryID { get; set; }

        public int Quantity { get; set; }

        private void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entry_Detail>().HasKey(ed => new { ed.VaccineID, ed.EntryID });
        }
    }
}
