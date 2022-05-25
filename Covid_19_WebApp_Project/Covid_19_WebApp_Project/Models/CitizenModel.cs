using System.ComponentModel.DataAnnotations;

namespace Covid_19_WebApp_Project.Models
{
    public class CitizenModel
    {   
        public int Id { get; set; }
        public string? FName { get; set; }
        public string? LName { get; set; }

        [DataType(DataType.Date)]
        public DateTime Bdate { get; set; }
        public string? Address { get; set; }
        public int Age { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; }
    }
}
