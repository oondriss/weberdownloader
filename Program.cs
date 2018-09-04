using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using TestApp.Extensions;
using TestApp.Infrastructure;

namespace TestApp
{
	// ReSharper disable once ClassNeverInstantiated.Global
	public class Program
	{
	    private static string _appDirectory;
	    public static string LogFile;

        public static void Main(string[] args)
        {
            
            var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
            _appDirectory = Path.GetDirectoryName(pathToExe);
            LogFile = Path.Combine(_appDirectory, "startuplog.txt");

            Directory.SetCurrentDirectory(_appDirectory);
            try
            {
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
                        .UseContentRoot(_appDirectory)
                        .UseStartup<Startup>()
                        .Build();
                    host.RunAsCustomService();
                }
            }
            catch (System.Exception ex)
            {
                File.WriteAllText(LogFile, ex.GetLogMessage());
                throw;
            }
        }
    }
}
