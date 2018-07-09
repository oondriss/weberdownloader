using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;
using TestApp.DbModels;
using TestApp.Extensions;

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
			var logs = new List<string>();

			logs.AddLogMessage("job started {0},{1},{2}", headId, headName, headLocation);
			_logger.LogInformation("job started {0},{1},{2}", headId, headName, headLocation);

			var head = await _dbManager.GetHead(headId);

			var jobLog = new JobLog
			{
				Head = head,
				Start = DateTime.Now
			};

			var addColumns = _dbManager.GetAdditionalColumns();
			var addValues = _dbManager.GetAdditionalValues(head);

			var addDataTable = new DataTable();
			addColumns.ToList().ForEach(i => addDataTable.Columns.Add(i.Name));


			try
			{
				//logika nacitavania dat
				//logika exportu do csv















				var data = new DataTable();
				var stringResult = data.ToCsv();
				
				var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
				var pathToContentRoot = Path.GetDirectoryName(pathToExe);

				File.WriteAllText(Path.Combine(pathToContentRoot, path2: "Output", path3: head.GetFileName()), stringResult);
				_logger.LogInformation("Head id: {0} saved output file: {1}", head.Id, head.GetFileName());
				logs.AddLogMessage("Head id: {0} saved output file: {1}", head.Id, head.GetFileName());

				jobLog.Finish = DateTime.Now;
				jobLog.WithoutException = true;
			}
			catch (Exception e)
			{
				jobLog.Finish = DateTime.Now;
				jobLog.Exception = $"{e.Message}\n{e.StackTrace}";
				jobLog.WithoutException = false;
				logs.AddLogMessage("Error while running job for head id: {0} name: {1}", headId, headName);
				_logger.LogError(e, "Error while running job for head id: {0} name: {1}", headId, headName);
			}
			finally
			{
				logs.AddLogMessage("job completed {0},{1},{2}", headId, headName, headLocation);
				_logger.LogInformation("job completed {0},{1},{2}", headId, headName, headLocation);

				jobLog.JobLogs = logs.GetLogs();
				await _dbManager.AddJobLog(jobLog);
			}
		}
    }
}
