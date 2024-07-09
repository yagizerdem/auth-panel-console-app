using Microsoft.Extensions.Configuration;
using Supabase.Gotrue.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace battleship
{
    internal class Database
    {
        private Supabase.Client _supabase { get; set; }
        public bool isConnected { get; set; }

        private readonly IConfiguration _config;
        public Database(IConfiguration config)
        {
            // options for supbase db
            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            this._config = config;
            this._supabase = new Supabase.Client(this._config["EnvironmentVariables:SUPABASE_URL"], this._config["EnvironmentVariables:SUPABASE_KEY"], options);
            this.isConnected = false;
        }
        public async void OpenConnection(Func<Task> action = null)
        {
            try
            {
                await this.Open();
                this.isConnected = true;
                if(action != null)  await action();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
            }
        }
        private async Task Open()
        {
            await this._supabase.InitializeAsync();
        }

        public async Task<bool> Register(string email, string password)
        {
            try
            {
                await this.SignUp(email, password);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return false;
            }
        }
        private async Task SignUp(string email, string password)
        {
            await this._supabase.Auth.SignUp(email, password);
        }

        public Supabase.Gotrue.Session? GetSession()
        {
            try
            {
                Supabase.Gotrue.Session auth = this._supabase.Auth.CurrentSession; 
                return auth;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        
        public async Task<bool> LogIn(string email , string password)
        {
            try
            {
                var session = await this._supabase.Auth.SignIn(email, password);
                return true;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

    }
}
