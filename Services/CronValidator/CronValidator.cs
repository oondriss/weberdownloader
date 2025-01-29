using System;
using NCrontab;

namespace TestApp.Services.CronValidator;

public class CronValidator : ICronValitador
{
    private readonly ILogger<CronValidator> _logger;

    public CronValidator(ILogger<CronValidator> logger)
    {
        _logger = logger;
    }
    public CrontabSchedule ValidateCron(string cron)
    {
        try
        {
            CrontabSchedule cronExp = CrontabSchedule.TryParse(cron);
            if (cronExp != null)
            {
                _logger.LogError("Failed validating cron expression '{0}'", cron);
            }

            return cronExp;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception while validating cron expression '{0}'", cron);
            return null;
        }
    }
}
