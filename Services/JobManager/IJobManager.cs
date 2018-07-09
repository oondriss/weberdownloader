using System.Threading.Tasks;

namespace TestApp.Services
{
	public interface IJobManager
	{
		Task<bool> CreateAllHeadsJobs();
		bool RemoveAllJobs();
		bool IsJobsRunning();
	}
}
