using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using TestApp.Extensions;
using TestApp.Infrastructure;

namespace TestApp
{
	// ReSharper disable once ClassNeverInstantiated.Global
	public class Program
    {
        public static void Main(string[] args)
        {
            var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
            var pathToContentRoot = Path.GetDirectoryName(pathToExe);

            IWebHost host;

            if (Debugger.IsAttached || args.Contains("console"))
            {
                host = WebHost.CreateDefaultBuilder()
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .UseStartup<Startup>()
                        .Build();
				host.Run();
            }
            else
            {
                host = WebHost.CreateDefaultBuilder()
                        .UseContentRoot(pathToContentRoot)
                        .UseStartup<Startup>()
                        .Build();
                host.RunAsCustomService();
            }
        }
    }
}
