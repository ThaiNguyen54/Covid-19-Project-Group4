using Covid_19_WebApp.Models;

namespace Covid_19_WebApp
{
    public static class GlobalVariables
    {
        public static bool isCorrect = true;
        public static bool isLogin = false;
        public static bool isManagerLogin = false;
        public static bool isManagerAccountCorrect = true;
        public static Citizen citizen = new Citizen();
        public static Manager manager = new Manager();
        public static string CitizenFName;
        public static string CitizenLName;
        public static string ManagerFName;
        public static string ManagerLName;
    }

}
