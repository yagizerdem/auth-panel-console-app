using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace battleship
{
    internal class Keyboard : EventEmitter
    {
        public Dictionary<string, Action> moveCommands;

        private Cursor cursor;

        private EventEmitter eventEmitter;

        private Database database;

        public Keyboard()
        {
            this.moveCommands = new Dictionary<string, Action>();


            this.moveCommands.Add("DownArrow", () => MoveKey("DownArrow"));
            this.moveCommands.Add("UpArrow", () => MoveKey("UpArrow"));
            this.moveCommands.Add("LeftArrow", () => MoveKey("LeftArrow"));
            this.moveCommands.Add("RightArrow", () => MoveKey("RightArrow"));

            this.cursor = Service.provider.GetService<Cursor>()!;    
            this.eventEmitter = Service.provider.GetService<EventEmitter>()!;
            this.database = Service.provider.GetService<Database>()!;
        }
        public void MoveKey(string key)
        {
            // log cursor position
            cursor.logCursorValues();
            switch (key)
            {
                case "DownArrow":
                    cursor.cy += cursor.stepY;
                    break;
                case "UpArrow":
                    cursor.cy -= cursor.stepY;
                    break;
                case "LeftArrow":
                    cursor.cx -= cursor.stepX;
                    break;
                case "RightArrow":
                    cursor.cy += cursor.stepX;
                    break;
            }

        }


        public void Enter()
        {
            if(SD.currentScreen == ScreenType.Home)
            {
                if(cursor.cy == 1)
                {
                    // register
                    eventEmitter.OnToRegisterScreen(EventArgs.Empty);
                }
                else if(cursor.cy == 2)
                {
                    // log in
                    eventEmitter.OnToLogInScreen(EventArgs.Empty);  
                }
                else if(cursor.cy  == 3)
                {
                    // search match (play)
                    eventEmitter.OnToSearchMatch(EventArgs.Empty);
                }
            }
            if (SD.currentScreen == ScreenType.Register)
            {
                if(cursor.cy == 3)
                {
                    // go back
                    eventEmitter.OnToHomeScreen(EventArgs.Empty);
                }
                else if(cursor.cy == 4)
                {
                    // register
                    eventEmitter.OnToRegister(EventArgs.Empty);

                }
            }
            if(SD.currentScreen == ScreenType.LogIn)
            {
                if(cursor.cy== 2)
                {
                    this.eventEmitter.OnToHomeScreen(EventArgs.Empty);
                }
                if(cursor.cy == 3)
                {
                    this.eventEmitter.OnToLogIn(EventArgs.Empty);
                }
            }
        }
 
        public void AlfaNumeric(string key)
        {
            if (key == "f1") key = "@";
            if (key == "f2") key = "i";
            if (key == "f3") key = ".";
            string pattern = @"^[a-z1-9@.]$";
            Regex rg = new Regex(pattern);
            if (!rg.IsMatch(key)) return;

            if (SD.currentScreen == ScreenType.Register)
            {
                if (cursor.cy == 0) SD.email += key;
                if(cursor.cy == 1) SD.password += key;
                if (cursor.cy == 2) SD.userName += key;

                eventEmitter.OnToRevalidateUI(EventArgs.Empty);
            }
            if(SD.currentScreen == ScreenType.LogIn)
            {
                if (cursor.cy == 0) SD.email += key;
                if (cursor.cy == 1) SD.password += key;

                eventEmitter.OnToRevalidateUI(EventArgs.Empty);
            }
        }
    
        public void BackSpace(string key)
        {
            if (key != "Backspace") return;
            if (SD.currentScreen == ScreenType.Home) return;
            if(SD.currentScreen == ScreenType.Register)
            {
                if (cursor.cy == 0 && SD.email.Length > 0) SD.email = SD.email.Remove(SD.email.Length - 1);
                if (cursor.cy == 1 && SD.password.Length > 0) SD.password = SD.password.Remove(SD.password.Length - 1);
                if (cursor.cy == 2 && SD.userName.Length > 0) SD.userName = SD.userName.Remove(SD.userName.Length - 1);
                eventEmitter.OnToRevalidateUI(EventArgs.Empty);
            }
            if(SD.currentScreen == ScreenType.LogIn)
            {
                if (cursor.cy == 0 && SD.email.Length > 0) SD.email = SD.email.Remove(SD.email.Length - 1);
                if (cursor.cy == 1 && SD.password.Length > 0) SD.password = SD.password.Remove(SD.password.Length - 1);
                eventEmitter.OnToRevalidateUI(EventArgs.Empty);
            }
        }

    }
}
