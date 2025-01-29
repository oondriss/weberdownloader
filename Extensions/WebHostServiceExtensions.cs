using System.ServiceProcess;
using TestApp.Infrastructure;

namespace TestApp.Extensions;

public static class WebHostServiceExtensions
{
    public static void RunAsCustomService(this IWebHost host)
    {
        try
        {
            ServiceWebHostService webHostService = new(host);
            ServiceBase.Run(webHostService);
        }
        catch (System.Exception)
        {
            _ = Debugger.Launch();
            throw;
        }
    }
}