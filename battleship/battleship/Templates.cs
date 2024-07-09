using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleship
{
    internal class Templates
    {

        public Dictionary<ScreenType, string> store;

        public Templates()
        {
            this.store = new Dictionary<ScreenType, string>();


            string homeTemplate =
"""
-----------------
|    Register   |
|    LogIn      |
|    Play       |
-----------------
""";
            string RegisterTemplate =
"""
    Email:
    Password:
    UserName:
    goBack
    submit


    f1 --> @
    f2 --> i
    f3 --> .
    backspace --> delte 1 character
    enter --> to submit
    arrow keys --> move
""";

            string logInTemplate =
"""
    Email:
    Password:
    goBack
    submit


    f1 --> @
    f2 --> i
    f3 --> .
    backspace --> delte 1 character
    enter --> to submit
    arrow keys --> move
""";

            this.store.Add(ScreenType.Home, homeTemplate);
            this.store.Add(ScreenType.Register, RegisterTemplate);
            this.store.Add(ScreenType.LogIn ,logInTemplate);


        }

    }
}
