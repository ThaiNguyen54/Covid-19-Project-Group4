using Covid_19_WebApp.Models;

namespace Covid_19_WebApp
{
    public static class GlobalVariables
    {
        public static bool isRegisterSuccessfully = false;
        public static bool isCorrect = true;
        public static bool isLogin = false;
        public static bool isManagerLogin = false;
        public static bool isManagerAccountCorrect = true;
        public static Citizen citizen = new Citizen();
        public static Manager manager = new Manager();
        public static Vaccine_Record vaccineRecord = new Vaccine_Record();
        public static int day;
        public static int month;
        public static int year;
        public static VACCINE_REGISTRATION_FORM vaccine_registration_form = new VACCINE_REGISTRATION_FORM();
        public static string DateOfBirth;
        public static bool error = false;
        public static int[] Month;
        public static int[] NumberOfCitizen;
        public static int year_stat;
        public static bool NotFound = false;
    }

}
