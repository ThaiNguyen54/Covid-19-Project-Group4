using System.ComponentModel.DataAnnotations;

namespace Covid_19_WebApp.Models
{
    public class Vaccine
    {
        [Key]
        public int VaccineID { get; set; }
        public int WarehouseID { get; set; }
        public string? VaccineName { get; set; } 
    }
}
