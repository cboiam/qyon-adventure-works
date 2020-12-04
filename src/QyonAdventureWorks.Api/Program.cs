using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace QyonAdventureWorks.Api
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.ConfigureKestrel(serverOptions =>
                   {
                       serverOptions.ListenAnyIP(8080);
                       serverOptions.AddServerHeader = false;
                   });
                   webBuilder.UseStartup<Startup>();
               });
    }
}
