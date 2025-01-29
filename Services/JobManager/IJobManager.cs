using System.Threading.Tasks;

namespace TestApp.Services.JobManager;

public interface IJobManager
{
    Task<bool> CreateAllHeadsJobs();
    bool RemoveAllJobs();
    bool IsJobsRunning();
}
