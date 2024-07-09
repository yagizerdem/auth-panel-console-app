using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Supabase.Functions.Interfaces;
using System.Diagnostics.Eventing.Reader;
using System.Security.Cryptography.X509Certificates;

namespace battleship
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Init(); // initial config of app

            var database = Service.provider.GetService<Database>();
            Cursor cursor = Service.provider.GetService<Cursor>()!;
            Keyboard keyboard = Service.provider.GetService<Keyboard>()!;
            EventEmitter eventEmitter = Service.provider.GetService<EventEmitter>()!;
            UI ui = Service.provider.GetService<UI>()!;

            
            database!.OpenConnection(async Task () =>
            {
                ui.ShowAlert( SD.dbConnectionSuccessfullAlert,SD.alertOffsetX , SD.alertOffsetY, ConsoleColor.Green);
                await Task.Delay(5000);
                ui.ClearAlert(SD.alertOffsetX, SD.alertOffsetY, SD.dbConnectionSuccessfullAlert.Length);

            });

            // register evnest
            eventEmitter.ToRegisterScreen += (object sender, EventArgs e) =>
            {
                SD.currentScreen = ScreenType.Register;
            };
            eventEmitter.ToHomeScreen += (object sender, EventArgs e) =>
            {
                SD.email = null;
                SD.userName = null;
                SD.password = null;
                SD.currentScreen = ScreenType.Home;
            };
            eventEmitter.ToRegister += async (object sender, EventArgs e) =>
            {
                bool flag = await database.Register(SD.email , SD.password);
                if (flag)
                {
                    ui.ShowAlert(SD.registerSuccessfulAlert, SD.alertOffsetX , SD.alertOffsetY,ConsoleColor.Green);
                    await Task.Delay(5000);
                    ui.ClearAlert(SD.alertOffsetX, SD.alertOffsetY, SD.registerSuccessfulAlert.Length);
                }
                else
                {
                    ui.ShowAlert(SD.registerErrorAlert, SD.alertOffsetX, SD.alertOffsetY, ConsoleColor.Red);
                    await Task.Delay(5000);
                    ui.ClearAlert(SD.alertOffsetX, SD.alertOffsetY, SD.registerErrorAlert.Length);
                }

            };
            eventEmitter.ToSearchMatch += (object sender, EventArgs e) =>
            {
                var session = database.GetSession();
            };
            eventEmitter.ToLogInScreen += (object sender, EventArgs e) =>
            {
                SD.currentScreen = ScreenType.LogIn;
            };
            eventEmitter.ToLogIn += async (object sender, EventArgs e) =>
            {
                bool flag = await database.LogIn(SD.email, SD.password);
                if (flag)
                {
                    ui.ShowAlert(SD.loginSuccessAlert, SD.alertOffsetX, SD.alertOffsetY, ConsoleColor.Green);
                    await Task.Delay(5000);
                    ui.ClearAlert(SD.alertOffsetX, SD.alertOffsetY, SD.loginSuccessAlert.Length);
                }
                else
                {
                    ui.ShowAlert(SD.loginErrorAlert, SD.alertOffsetX, SD.alertOffsetY, ConsoleColor.Red);
                    await Task.Delay(5000);
                    ui.ClearAlert(SD.alertOffsetX, SD.alertOffsetY , SD.loginErrorAlert.Length);
                }
            };


            while (true)
            {
                // midleware for keyboard inputs
                if (Console.KeyAvailable) // non blocking process
                {
                    ConsoleKeyInfo ckey = Console.ReadKey(true);

                    Action? arrowKeyMiddleware = null;

                    keyboard.moveCommands.TryGetValue(ckey.Key.ToString(), out arrowKeyMiddleware);
                    // arrow key middleware
                    if (arrowKeyMiddleware != null) arrowKeyMiddleware();
                    // delte prev cursor ui
                    ui.DeltePrevCursor();


                    // enter input
                    if(ckey.Key.ToString() == "Enter")
                    {
                        keyboard.Enter();
                    }

                    // listen characters
                    keyboard.AlfaNumeric(ckey.Key.ToString().ToLower());
                    // listen backspace
                    keyboard.BackSpace(ckey.Key.ToString());
                }

                // ui middleware
                ui.PrintScreen();
                ui.PrintCursor();
                Thread.Sleep(100);
            }


   
        }
 
        static void Init()
        {
            Console.CursorVisible = false;
            Cursor cursor = Service.provider.GetService<Cursor>()!;
            cursor.stepX = 0;
            cursor.stepY = 1;
            cursor.boundX = [3, 3];
            cursor.boundY = [1, 3];
            cursor.cy = 1;
            cursor.cx = 3;

            SD.currentScreen = ScreenType.Home;
        }
    }
}
