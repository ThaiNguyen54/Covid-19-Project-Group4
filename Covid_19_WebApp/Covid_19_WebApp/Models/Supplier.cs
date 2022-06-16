using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Covid_19_WebApp.Models
{
    public class Supplier
    {
        [Key]
        [DisplayName("Supplier ID")]
        public int SupID { get; set; }

        [DisplayName("Supplier Name")]
        public string? SupName { get; set; }

        [DisplayName("Supplier Address")]
        public string? SupAddress { get; set; }

        [DisplayName("Supplier Phone")]
        public string? Phone { get; set; }
    }
}
