using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace battleship
{
    internal static class Service
    {
        public static ServiceProvider provider;
        
        static Service()
        {
            // Setup configuration
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Setup DI container
            Service.provider = new ServiceCollection()
                .AddSingleton<IConfiguration>(config)
                .AddSingleton<Database>()
                .AddSingleton<Cursor>()
                .AddSingleton<Templates>()
                .AddSingleton<Keyboard>()
                .AddSingleton<UI>()
                .AddSingleton<EventEmitter>()
                .BuildServiceProvider();
        }
    }
}
