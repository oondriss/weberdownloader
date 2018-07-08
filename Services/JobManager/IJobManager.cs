using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire.Storage;

namespace TestApp.Services
{
	public interface IJobManager
	{
		Task<bool> CreateAllHeadsJobs();
		bool RemoveAllJobs();
		bool IsJobsRunning();
	}
}
