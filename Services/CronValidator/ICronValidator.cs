using NCrontab;

namespace TestApp.Services.CronValidator;

public interface ICronValitador
{
    CrontabSchedule ValidateCron(string cron);
}
