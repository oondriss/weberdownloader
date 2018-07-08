using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NCrontab;

namespace TestApp.Services
{
	public interface ICronValitador
	{
		CrontabSchedule ValidateCron(string cron);
	}
}
