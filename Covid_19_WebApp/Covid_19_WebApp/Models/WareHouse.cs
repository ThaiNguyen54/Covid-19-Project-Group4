using System.ComponentModel.DataAnnotations;

namespace Covid_19_WebApp.Models
{
    public class WareHouse
    {
        [Key]
        public int WarehouseID { get; set; }
        
        public string? Location { get; set; }

    }
}
