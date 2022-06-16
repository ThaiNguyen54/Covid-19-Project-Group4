using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Covid_19_WebApp.Models
{
    public class Warehouse_Vaccine
    {
        [Key]
        [DisplayName("Vaccine ID")]
        public int VaccineId { get; set; }

        [DisplayName("Warehouse ID")]
        public int WarehouseID { get; set; }

        [DisplayName("Stock")]
        public int Stock { get; set; }
    }
}
