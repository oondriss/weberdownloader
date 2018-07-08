using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Extensions.Logging;
using Hangfire.Common;
using TestApp.DbModels;

namespace TestApp.Services
{
	public class WeberReader : IWeberReader
    {
		private readonly ILogger _logger;
	    private readonly IDbManager _dbManager;

	    public WeberReader(ILogger<WeberReader> logger, IDbManager dbManager)
	    {
		    _logger = logger;
		    _dbManager = dbManager;
	    }

		public async void ReadWeberData(int headId, string headName, string headLocation)
		{
			_logger.LogInformation("job started {0},{1},{2}", headId, headName, headLocation);
			var head = await _dbManager.GetHead(headId);

			var jobLog = new JobLog
			{
				Head = head,
				Start = DateTime.Now
			};


			try
			{
				//logika nacitavania dat
				//logika exportu do csv
				


				jobLog.Finish = DateTime.Now;
				jobLog.WithoutException = true;
			}
			catch (Exception e)
			{
				jobLog.Finish = DateTime.Now;
				jobLog.Exception = $"{e.Message}\n{e.StackTrace}";
				jobLog.WithoutException = false;
				_logger.LogError(e, "Error while running job for head id: {0} name: {1}", headId, headName);
			}
			finally
			{
				_logger.LogInformation("job completed {0},{1},{2}", headId, headName, headLocation);
				await _dbManager.AddJobLog(jobLog);
			}
		}
    }
}
