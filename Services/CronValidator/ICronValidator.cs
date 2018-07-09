using NCrontab;

namespace TestApp.Services
{
	public interface ICronValitador
	{
		CrontabSchedule ValidateCron(string cron);
	}
}
