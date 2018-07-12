using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Logging;
using TestApp.DbModels;
using TestApp.Extensions;

namespace TestApp.Services
{
	public class WeberReader : IWeberReader
    {
		private readonly ILogger _logger;
	    private readonly IDbManager _dbManager;
	    private const string Nr = "Nr";
	    private const string ProgNum = "ProgNum";
	    private const string Res1 = "Res1";
	    private const string Res2 = "Res2";
	    private const string Res3 = "Res3";
	    private const string Ionio = "IONIO";
	    private const string Duration = "Duration";
	    private const string Id = "ID";
	    private const string Cycle = "Cycle";
	    private const string Time = "DateTime";
	    private Exception ex;
	    private VarComm _varComm;
	    private C_CommonFunctions _commonFunctions;


		public WeberReader(ILogger<WeberReader> logger, IDbManager dbManager)
	    {
		    _logger = logger;
		    _dbManager = dbManager;
	    }

		public void ReadWeberData(int headId, string headName, string headLocation)
		{
			var logs = new List<string>();
			var dataTable = new DataTable();
			var head = _dbManager.GetHead(headId).Result;
			var addColumns = _dbManager.GetAdditionalColumns().OrderBy(i => i.Id).ToList();
			var addValues = _dbManager.GetAdditionalValues(head).OrderBy(i => i.Column.Id).ToList();
			var addCollDefaultValues = new Dictionary<string, string>();
			addColumns.ForEach(i => addCollDefaultValues.Add(i.Name, addValues.FirstOrDefault(j => j.Column == i)?.Value));
			JobLog jobLog;
			jobLog = new JobLog
			{
				Head = head,
				Start = DateTime.Now
			};
			var columns = new List<string>
			{
				Nr,
				ProgNum,
				Res1,
				Res2,
				Res3,
				Ionio,
				Duration,
				Id,
				Cycle,
				Time
			};

			logs.AddLogMessage("job started {0},{1},{2}", headId, headName, headLocation);
			_logger.LogInformation("job started {0},{1},{2}", headId, headName, headLocation);

			


			
			dataTable.PrepareColumns(columns, addCollDefaultValues);

			try
			{
				//logika nacitavania dat
				//logika exportu do csv

				logs.AddLogMessage("Settuping connection to head {0} {1} {2}", head.Id, head.Name, head.Ip);
				this._logger.LogInformation("Settuping connection to head {0} {1} {2}", head.Id, head.Name, head.Ip);
				if (!this.SetupCommunicationAndAcquireData(head))
				{
					logs.AddLogMessage("Could not connect to screw head, exiting job!!", Array.Empty<object>());
					this._logger.LogError("Could not connect to screw head, exiting job!!", Array.Empty<object>());
					throw new ConnectionAbortedException("Could not connect to screw head, exiting job!!");
				}

				if (!this.ParseWeberData(ref dataTable, head))
				{
					logs.AddLogMessage("Could not parse screw head data, exiting job!!", Array.Empty<object>());
					this._logger.LogError("Could not parse screw head data, exiting job!!", Array.Empty<object>());
					throw new DataException("Could not parse screw head data, exiting job!!");
				}

				string contents = dataTable.ToCsv();
				string directoryName = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

				if (directoryName != null)
				{
					File.WriteAllText(Path.Combine(directoryName, "Output", head.GetFileName()), contents);
					this._logger.LogInformation("Head id: {0} saved output file: {1}", head.Id, head.GetFileName());
					logs.AddLogMessage("Head id: {0} saved output file: {1}", head.Id, head.GetFileName());
				}
				else
				{
					logs.AddLogMessage("Data downloaded, but could not find output folder, exiting job!!", Array.Empty<object>());
					this._logger.LogError("Data downloaded, but could not find output folder, exiting job!!", Array.Empty<object>());
					throw new InvalidOperationException("Data downloaded, but could not find output folder, exiting job!!");
				}
				
				jobLog.Finish = DateTime.Now;
				jobLog.WithoutException = true;
			}
			catch (Exception e)
			{
				ex = e;
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
				var res = _dbManager.AddJobLog(jobLog).Result;
				if (ex != null)
				{
					throw ex;
				}
			}
		}

	    private bool SetupCommunicationAndAcquireData(Head head)
	    {
		    this._varComm = new VarComm(this._logger, IPAddress.Parse(head.Ip));
		    if (!this._varComm.ReceiveVarBlock(32))
		    {
			    this._logger.LogError("Could not receive StatSampleBlock!", Array.Empty<object>());
			    return false;
		    }
		    this._commonFunctions = new C_CommonFunctions(this._varComm);
		    return true;
	    }

		private bool ParseWeberData(ref DataTable dataTable, Head head)
		{
			dataTable.Rows.Clear();
			if (this._varComm.StatSample.Info.Length > 0)
			{
				int num5 = this._varComm.StatSample.Info.Position - 1;
				try
				{
					for (int i = 0; i < this._varComm.StatSample.Info.Length; i++)
					{
						DataRow newRow = dataTable.NewRow();
						newRow["Nr"] = i + 1;
						string text = this._commonFunctions.ByteToString(this._varComm.StatSample.Data[num5].ProgName);
						if (text.Length == 0)
						{
							newRow[ProgNum] = this._varComm.StatSample.Data[num5].ProgNum.ToString();
						}
						else
						{
							newRow[ProgNum] = text;
						}
						double num6;
						string empty10;
						if ((this._varComm.StatSample.Data[num5].Valid & 1) > 0)
						{
							empty10 = this._commonFunctions.GetResName(this._varComm.StatSample.Data[num5].ResultParam1) + "(" + (this._varComm.StatSample.Data[num5].ResultStep1 + 1) + "): ";
							float num4 = (1f + this._commonFunctions.GetResFactor(this._varComm.StatSample.Data[num5].ResultParam1) * (this._varComm.TorqueConvert - 1f)) * this._varComm.StatSample.Data[num5].Res1;
							string str = empty10;
							num6 = Math.Round((double)num4, this._commonFunctions.GetResDigits(this._varComm.StatSample.Data[num5].ResultParam1));
							empty10 = str + num6.ToString(CultureInfo.InvariantCulture);
							empty10 += this._commonFunctions.GetResUnit(this._varComm.StatSample.Data[num5].ResultParam1, this._varComm.TorqueUnitName);
						}
						else
						{
							empty10 = "NotValid";
						}
						newRow[Res1] = empty10;
						if ((this._varComm.StatSample.Data[num5].Valid & 2) > 0)
						{
							empty10 = this._commonFunctions.GetResName(this._varComm.StatSample.Data[num5].ResultParam2) + "(" + (this._varComm.StatSample.Data[num5].ResultStep2 + 1) + "): ";
							float num4 = (1f + this._commonFunctions.GetResFactor(this._varComm.StatSample.Data[num5].ResultParam2) * (this._varComm.TorqueConvert - 1f)) * this._varComm.StatSample.Data[num5].Res2;
							string str2 = empty10;
							num6 = Math.Round((double)num4, this._commonFunctions.GetResDigits(this._varComm.StatSample.Data[num5].ResultParam2));
							empty10 = str2 + num6.ToString(CultureInfo.InvariantCulture);
							empty10 += this._commonFunctions.GetResUnit(this._varComm.StatSample.Data[num5].ResultParam2, this._varComm.TorqueUnitName);
						}
						else
						{
							empty10 = "NotValid";
						}
						newRow[Res2] = empty10;
						if ((this._varComm.StatSample.Data[num5].Valid & 4) > 0)
						{
							empty10 = this._commonFunctions.GetResName(this._varComm.StatSample.Data[num5].ResultParam3) + "(" + (this._varComm.StatSample.Data[num5].ResultStep3 + 1) + "): ";
							float num4 = (1f + this._commonFunctions.GetResFactor(this._varComm.StatSample.Data[num5].ResultParam3) * (this._varComm.TorqueConvert - 1f)) * this._varComm.StatSample.Data[num5].Res3;
							string str3 = empty10;
							num6 = Math.Round((double)num4, this._commonFunctions.GetResDigits(this._varComm.StatSample.Data[num5].ResultParam3));
							empty10 = str3 + num6.ToString(CultureInfo.InvariantCulture);
							empty10 += this._commonFunctions.GetResUnit(this._varComm.StatSample.Data[num5].ResultParam3, this._varComm.TorqueUnitName);
						}
						else
						{
							empty10 = "NotValid";
						}
						newRow[Res3] = empty10;
						switch (this._varComm.StatSample.Data[num5].IONIO)
						{
							case 1:
								empty10 = "OK";
								break;
							case 0:
								empty10 = "n. def.";
								break;
							default:
								empty10 = "NOK: NIO" + this._varComm.StatSample.Data[num5].IONIO + "(" + (this._varComm.StatSample.Data[num5].LastStep + 1) + ")";
								break;
						}
						newRow[Ionio] = empty10;
						newRow[Duration] = this._varComm.StatSample.Data[num5].ScrewDuration.ToString("f" + 2) + "Second";
						newRow[Id] = this._commonFunctions.ByteToString(this._varComm.StatSample.Data[num5].ScrewID);
						newRow[Cycle] = this._varComm.StatSample.Data[num5].Cycle;
						DateTime dateTime;
						try
						{
							dateTime = new DateTime(this._varComm.StatSample.Data[num5].Time.Year, this._varComm.StatSample.Data[num5].Time.Month, this._varComm.StatSample.Data[num5].Time.Day, this._varComm.StatSample.Data[num5].Time.Hour, this._varComm.StatSample.Data[num5].Time.Minute, this._varComm.StatSample.Data[num5].Time.Second);
						}
						catch
						{
							dateTime = new DateTime(1, 1, 1, 0, 0, 0);
						}
						newRow[Time] = dateTime.ToString(CultureInfo.InvariantCulture);
						num5--;
						if (num5 < 0)
						{
							num5 = 1999;
						}
						dataTable.Rows.Add(newRow);
					}
					return true;
				}
				catch (Exception ex)
				{
					this._logger.LogError(ex, "Error while loading results from head id:'{0}', name:'{1}', ip:'{2}'", head.Id, head.Name, head.Ip);
					return false;
				}
			}
			this._logger.LogError("No data to download in screw head id:'{0}', name:'{1}', ip:'{2}'", head.Id, head.Name, head.Ip);
			return false;
		}
	}
}
