using System.ServiceProcess;
using Microsoft.AspNetCore.Hosting;
using TestApp.Infrastructure;

namespace TestApp.Extensions
{
	public static class WebHostServiceExtensions
    {
        public static void RunAsCustomService(this IWebHost host)
        {
            var webHostService = new ServiceWebHostService(host);
            ServiceBase.Run(webHostService);
        }
    }
}