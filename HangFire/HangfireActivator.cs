using System;

namespace TestApp.HangFire;

public class HangfireActivator : JobActivator
{
    private readonly IServiceProvider _serviceProvider;

    public HangfireActivator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public override object ActivateJob(Type type)
    {
        return _serviceProvider.GetService(type);
    }
}

public static class HangfireConfigurationExtensions
{
    public static void SetupHangfire(this IApplicationBuilder app, WebHostBuilderContext host)
    {
        int retryCount = host.Configuration.GetValue<int>("RetryCount");
        int concurrentExecutionTimeout = host.Configuration.GetValue<int>("DisableConcurrentExecutionTimeout");

        AutomaticRetryAttribute filter = (AutomaticRetryAttribute)GlobalJobFilters.Filters.SingleOrDefault(i => i.Instance is AutomaticRetryAttribute)?.Instance;
        if (filter != null)
        {
            filter.Attempts = retryCount;
        }
        else
        {
            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute() { Attempts = retryCount });
        }

        DisableConcurrentExecutionAttribute filter2 = (DisableConcurrentExecutionAttribute)GlobalJobFilters.Filters.SingleOrDefault(i => i.Instance is DisableConcurrentExecutionAttribute)?.Instance;
        if (filter2 != null)
        {
            filter2 = new DisableConcurrentExecutionAttribute(concurrentExecutionTimeout);
        }
        else
        {
            GlobalJobFilters.Filters.Add(new DisableConcurrentExecutionAttribute(concurrentExecutionTimeout));
        }

        _ = GlobalConfiguration.Configuration.UseActivator(new HangfireActivator(app.ApplicationServices));
        _ = app.UseHangfireDashboard("/jobs");
    }

}
