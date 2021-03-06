using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Covid_19_WebApp.Models
{
    public class Citizen
    {
        [Key]
        [DisplayName("Citizen ID")]
        public int CitizenId { get; set; }
        [DisplayName("First Name")]
        public string? FName { get; set; }
        [DisplayName("Last Name")]
        public string? LName { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Birth Date")]
        public DateTime Bdate { get; set; }
        public string? Address { get; set; }
        public int? Age { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }

        
    }
}
