using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Covid_19_WebApp.Models
{
    public class VACCINE_REGISTRATION_FORM
    {
        [Key]
        public int FormID { get; set; }
        public int VaccineID { get; set; }
        public int CitizenID { get; set; }
        [DisplayName("Your preferred vaccination date")]
        public DateTime InjectionDate { get; set; }
        [DisplayName("Register date")]
        public DateTime RegisterDate { get; set; }
        [DisplayName("Injection number")]
        public string? InjectionNumber { get; set; }
    }
}
