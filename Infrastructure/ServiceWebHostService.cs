using System;
using System.ServiceProcess;
using Microsoft.AspNetCore.Hosting.WindowsServices;

namespace TestApp.Infrastructure;

public class ServiceWebHostService : WebHostService
{
    private readonly ILogger _logger;
    private readonly IWebHost _host;

    public ServiceWebHostService(IWebHost host) : base(host)
    {
        _host = host;
        _logger = _host.Services.GetRequiredService<ILogger<ServiceWebHostService>>();
    }

    protected override void OnStarting(string[] args)
    {
        _logger.LogInformation("Starting new instance");
        base.OnStarting(args);
    }

    protected override void OnStarted()
    {
        _logger.LogInformation("Started new instance");
        base.OnStarted();
    }

    protected override void OnStopping()
    {
        _logger.LogInformation("Stopping instance");
        base.OnStopping();
    }

    protected override object GetService(Type service)
    {
        return _host.Services.GetRequiredService(service);
    }

    protected override void OnContinue()
    {
        _logger.LogInformation("Continued instance");
        base.OnContinue();
    }

    protected override void OnPause()
    {
        _logger.LogInformation("Paused instance");
        base.OnPause();
    }

    protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
    {
        _logger.LogInformation("Power event {0}", powerStatus.ToString());
        return base.OnPowerEvent(powerStatus);
    }

    protected override void OnSessionChange(SessionChangeDescription changeDescription)
    {
        _logger.LogInformation("Session changed - reason: '{0}', sessionid: '{1}'", changeDescription.Reason, changeDescription.SessionId);
        base.OnSessionChange(changeDescription);
    }

    protected override void OnShutdown()
    {
        _logger.LogInformation("PC Shutdown");
        base.OnShutdown();
    }

    protected override void OnCustomCommand(int command)
    {
        _logger.LogInformation("Custom command: {0}", command);
        base.OnCustomCommand(command);
    }

    protected override void OnStopped()
    {
        _logger.LogInformation("Stopped instance");
        base.OnStopped();
    }
}