using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Covid_19_WebApp.Models
{
    public class Vaccine_Entry
    {
        [Key]
        [DisplayName("Entry ID")]
        public int EntryId { get; set; }

        [DisplayName("Manager ID")]
        public int ManagerID { get; set; }

        [DisplayName("Supplier ID")]
        public int SupID { get; set; }

        [DisplayName("Entry Date")]
        public DateTime EntryDate { get; set; }

        [DisplayName("Amount of Vaccine")]
        public int Amount_Vaccine { get; set; }
    }
}
