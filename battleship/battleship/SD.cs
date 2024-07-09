using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleship
{
    internal class SD
    {
        public static int alertOffsetX = 80;
        public static int alertOffsetY = 0;

        public static string dbConnectionSuccessfullAlert = "db connection opened ...";
        public static string registerSuccessfulAlert = "register to database successfull ...";
        public static string registerErrorAlert = "error occured while registering database ...";
        public static string loginSuccessAlert = "log in successfull ...";
        public static string loginErrorAlert = "log in error ... ";

        public static ScreenType currentScreen = ScreenType.Home; // initial screen of program

        public static string userName ;
        public static string password = "";
        public static string email = "";


    }
    internal enum ScreenType
    {
        Home,
        Register,
        LogIn,
        Wait,
        Game
    }
}
