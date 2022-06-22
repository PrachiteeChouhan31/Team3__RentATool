using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ContosoCrafts.WebSite
{
    /// <summary>
    /// Program class for the project
    /// </summary>
    public class Program
    {
        /// <summary>
        /// this is a main and call to create a host.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {

            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// this create a host for the project and calls startup.cs
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}