using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Covid_19_WebApp.Models
{
    public class Vaccine_Record
    {
        [Key]
        [DisplayName("Record ID")]
        public int RecordID { get; set; }

        [DisplayName("Citizen ID")]
        public int CitizenID { get; set; }

        [DisplayName("Number of Injection")]
        public int Number_Injection { get; set; }

        [DisplayName("First Date of Injection")]
        public DateTime? FirstDate { get; set; }

        [DisplayName("First Vaccine")]
        public string? FirstVaccine { get; set; }

        [DisplayName("Second Date of Injection")]
        public DateTime? SecondDate { get; set; }

        [DisplayName("Second Vaccine")]
        public string? SecondVaccine { get; set; }

        [DisplayName("Third Date of Injection")]
        public DateTime? ThirdDate { get; set; }

        [DisplayName("Third Vaccine")]
        public string? ThirdVaccine { get; set; }

        [DisplayName("ID of The First Nurse")]
        public int? First_NurseID { get; set; }

        [DisplayName("ID of The Second Nurse")]
        public int? Second_NurseID { get; set; }

        [DisplayName("ID of The Third Nurse")]
        public int? Third_NurseID { get; set; }
    }
}
