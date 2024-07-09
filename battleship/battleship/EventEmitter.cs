using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleship
{
    internal class EventEmitter
    {
        public event EventHandler ToRegisterScreen;
        public event EventHandler ToHomeScreen;
        public event EventHandler ToRevalidateUI;
        public event EventHandler ToRegister;
        public event EventHandler ToSearchMatch;
        public event EventHandler ToLogInScreen;
        public event EventHandler ToLogIn;

        protected internal virtual void OnToRegisterScreen(EventArgs e)
        {
            ToRegisterScreen?.Invoke(this, e);
        }
        protected internal virtual void OnToRevalidateUI(EventArgs e)
        {
            ToRevalidateUI?.Invoke(this, e);
        }
        protected internal virtual void OnToHomeScreen(EventArgs e)
        {
            ToHomeScreen?.Invoke(this, e);
        }
        protected internal virtual void OnToRegister(EventArgs e)
        {
            ToRegister?.Invoke(this, e);
        }
        protected internal virtual void OnToSearchMatch(EventArgs e)
        {
            ToSearchMatch?.Invoke(this, e);
        }
        protected internal virtual void OnToLogInScreen(EventArgs e)
        {
            ToLogInScreen?.Invoke(this, e);
        }
        protected internal virtual void OnToLogIn(EventArgs e)
        {
            ToLogIn?.Invoke(this, e);
        }
    }
}
