using System;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Storage;
using Microsoft.Extensions.Logging;

namespace TestApp.Services
{
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
				var allHeads = _dbManager.GetHeadsAsync();
				foreach (var head in await allHeads)
				{
					var id = $"HE:'{head.Id.ToString()}'HN:'{head.Name}'HL:'{head.Hall}'";

					RecurringJob.AddOrUpdate<WeberReader>(id, i => i.ReadWeberData(head.Id, head.Name, head.Location), head.CronExp);
					_logger.LogInformation("Job id: '{0}' for head id: '{1}' name: '{2}' loc: '{3}' successfully scheduled.", id, head.Id, head.Name, head.Location);
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
				using (var connection = JobStorage.Current.GetConnection())
				{
					connection.GetRecurringJobs().ForEach(i => RecurringJob.RemoveIfExists(i.Id));
				}
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
		    using (var connection = JobStorage.Current.GetConnection())
		    {
			    return connection.GetRecurringJobs().Any();
		    }
	    }
	}
}
