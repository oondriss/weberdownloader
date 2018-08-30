using System.Diagnostics;
using System.ServiceProcess;
using Microsoft.AspNetCore.Hosting;
using TestApp.Infrastructure;

namespace TestApp.Extensions
{
	public static class WebHostServiceExtensions
    {
        public static void RunAsCustomService(this IWebHost host)
        {
            try
            {
                var webHostService = new ServiceWebHostService(host);
                ServiceBase.Run(webHostService);
            }
            catch (System.Exception)
            {
                Debugger.Launch();
                throw;
            }
        }
    }
}