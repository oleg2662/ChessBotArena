using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace BoardGame.Service
{
    /// <summary>
    /// The main class of the whole service.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main entry point of the service.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        /// <summary>
        /// Builds the web host.
        /// </summary>
        /// <param name="args">Arguments</param>
        /// <returns>The created webhost object.</returns>
        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                          // .UseSetting("https_port", "8080") // TODO: SSL later...
                          .UseStartup<Startup>()
                          .Build();
        }
    }
}
