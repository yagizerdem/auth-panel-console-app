using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace battleship
{
    internal class UI
    {
        Cursor cursor;

        Stack<Action> actions;

        Templates templates;

        EventEmitter eventEmitter;
        public UI()
        {
            this.cursor = Service.provider.GetService<Cursor>()!;
            this.actions = new Stack<Action>();
            this.templates = Service.provider.GetService<Templates>()!;
            this.eventEmitter = Service.provider.GetService<EventEmitter>()!;

            // register evenst
            eventEmitter.ToRevalidateUI += (object sender, EventArgs e) =>
            {
                this.actions.Clear(); // clear actions automatically reprint ui on screen
            };
        }
        public void ShowAlert(string message , int cx , int cy , ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(cx, cy);
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.White;
            this.SyncCursor();
        }

        public void SyncCursor()
        {
            Console.SetCursorPosition(this.cursor.cx , this.cursor.cy);
        }

        public void ClearAlert(int cx , int cy , int length)
        {
            Console.SetCursorPosition(cx, cy);
            Console.Write(string.Concat(Enumerable.Repeat(" ", length)));
            this.SyncCursor();
        }


        //private delegate void PrintDelegate(ScreenType screenType);
        //private PrintDelegate PrintScreenDeleagte = (type) =>
        //{
        //    Console.SetCursorPosition(0, 0);
        //    Templates templates = Service.provider.GetService<Templates>()!;
        //    string screen = templates.store[type];
        //    Console.WriteLine(screen);
        //};


        // wrapper for delegate type
        public void PrintHomeScreen()
        {
            SyncCursor();
            Console.SetCursorPosition(0, 0);

            string screen = templates.store[ScreenType.Home];
            Console.WriteLine(screen);
            // store action in 
            Action action = PrintHomeScreen;
            this.actions.Push(action);

        }
        public void PrintRegisterScreen()
        {
            //pritn frame
            Console.SetCursorPosition(0, 0);
            string screen = templates.store[ScreenType.Register];
            Console.WriteLine(screen);

            // write crendentials
            Console.SetCursorPosition(20, 0);
            Console.Write(SD.email);
            Console.SetCursorPosition(20, 1);
            Console.Write(SD.password);
            Console.SetCursorPosition(20, 2);
            Console.Write(SD.userName);


            // store action in 
            Action action = PrintRegisterScreen;
            this.actions.Push(action);

            this.SyncCursor();
        }

        public void PrintLogInScreen()
        {
            //pritn frame
            Console.SetCursorPosition(0, 0);
            string screen = templates.store[ScreenType.LogIn];
            Console.WriteLine(screen);

            // write crendentials
            Console.SetCursorPosition(20, 0);
            Console.Write(SD.email);
            Console.SetCursorPosition(20, 1);
            Console.Write(SD.password);
            Console.SetCursorPosition(20, 2);
            Console.Write(SD.userName);


            // store action in 
            Action action = PrintLogInScreen;
            this.actions.Push(action);

            this.SyncCursor();
        }

        public void PrintCursor()
        {
            this.SyncCursor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(">");
            Console.ForegroundColor = ConsoleColor.White;
        }
    
        public void ClearScreen()
        {
            Console.Clear();
        }
        public void PrintScreen()
        {
            Action? lastAction = this.actions.FirstOrDefault();
            
            if (SD.currentScreen == ScreenType.Home)
            {
                Action expectedAction = PrintHomeScreen;
                if (!expectedAction.Method.Equals(lastAction?.Method))
                {
                    Console.Clear();
                    this.PrintHomeScreen();
                }
            }
            else if(SD.currentScreen == ScreenType.Register)
            {
                Action expectedAction = PrintRegisterScreen;
                if (!expectedAction.Method.Equals(lastAction?.Method))
                {
                    Console.Clear();
                    this.PrintRegisterScreen();
                }
            }
            else if(SD.currentScreen == ScreenType.LogIn)
            {
                Action expectedAction = PrintLogInScreen;
                if (!expectedAction.Method.Equals(lastAction?.Method))
                {
                    Console.Clear();
                    this.PrintLogInScreen();
                }
            }

        }
        
        public void DeltePrevCursor()
        {
            Console.SetCursorPosition(cursor.prevcx, cursor.prevcy);
            Console.Write(" ");
            this.SyncCursor();
        }
    }
}
