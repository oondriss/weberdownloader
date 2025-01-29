using System;
using System.Threading.Tasks;
using Hangfire.Storage;
using TestApp.Services.DbManager;

namespace TestApp.Services.JobManager;

public class JobManager : IJobManager
{
    private readonly IDbManager _dbManager;
    private readonly ILogger<JobManager> _logger;

    public JobManager(IDbManager dbManager, ILogger<JobManager> logger)
    {
        _dbManager = dbManager;
        _logger = logger;
    }

    public async Task<bool> CreateAllHeadsJobs()
    {
        try
        {
            Task<IQueryable<DbModels.Head>> allHeads = _dbManager.GetHeadsAsync();
            foreach (DbModels.Head head in await allHeads)
            {
                string id = $"HE:{head.Id}-HN:{head.Name}-HL:{head.Hall}";

                RecurringJob.AddOrUpdate<WeberReader.WeberReader>(id, i => i.ReadWeberData(head.Id, head.Name, head.Location), head.CronExp);
                _logger.LogInformation($"Job id: '{id}' for head id: '{head.Id}' name: '{head.Name}' loc: '{head.Location}' successfully scheduled.");
            }
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while setting jobs");
            return false;
        }
    }

    public bool RemoveAllJobs()
    {
        try
        {
            using IStorageConnection connection = JobStorage.Current.GetConnection();
            connection.GetRecurringJobs().ForEach(i => RecurringJob.RemoveIfExists(i.Id));
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while removing all jobs");
            return false;
        }
    }

    public bool IsJobsRunning()
    {
        using IStorageConnection connection = JobStorage.Current.GetConnection();
        return connection.GetRecurringJobs().Any();
    }
}
