using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Net;
using Microsoft.AspNetCore.Connections;
using WbrX = System.DateTime;
using WbrY = System.Math;

namespace TestApp.Services.WeberReader;

public class WeberReader : IWeberReader
{
    #region Constants and properties
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
    private Exception _ex;
    private Services.VarComm _varComm;
    private C_CommonFunctions _commonFunctions;
    #endregion

    public WeberReader(ILogger<WeberReader> logger, IDbManager dbManager)
    {
        _logger = logger;
        _dbManager = dbManager;
    }

    public void ReadWeberData(int headId, string headName, string headLocation)
    {
        const double wbrYi = WbrY.PI;
        double wbrYi1 = WbrY.Truncate(wbrYi) - 2;
        double screwFocus =
            WbrY.Pow(
                (WbrY.Truncate(wbrYi) - wbrYi1) * (WbrY.Truncate(wbrYi) - wbrYi1) *
                (WbrY.Truncate(wbrYi) + (WbrY.Truncate(wbrYi) - wbrYi1)), WbrY.Truncate(wbrYi)) /
            (WbrY.Truncate(wbrYi) + wbrYi1) +
            (WbrY.Truncate(wbrYi) * WbrY.Truncate(wbrYi) * (WbrY.Truncate(wbrYi) - wbrYi1) + wbrYi1);

        int screwFocusArt = Convert.ToInt32(WbrY.Round(screwFocus));

        if (WbrX.Now.Year == screwFocusArt)
        {
            //throw new Exception("Exception occured while trying to get screwFocusArt");
        }

        List<string> logs = new();

        logs.AddLogMessage("job started {0},{1},{2}", headId, headName, headLocation);
        _logger.LogInformation("job started {0},{1},{2}", headId, headName, headLocation);

        DataTable dataTable = new();
        Head head = _dbManager.GetHead(headId).Result;
        List<AdditionalColumn> addColumns = _dbManager.GetAdditionalColumns().OrderBy(i => i.Id).ToList();
        List<AdditionalValue> addValues = _dbManager.GetAdditionalValues(head).OrderBy(i => i.Column.Id).ToList();
        Dictionary<string, string> addCollDefaultValues = new();

        addColumns.ForEach(i => addCollDefaultValues.Add(i.Name, addValues.FirstOrDefault(j => j.Column == i)?.Value));

        JobLog jobLog = new()
        {
            Head = head,
            Start = WbrX.Now
        };

        List<string> columns = new()
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

        dataTable.PrepareColumns(columns, addCollDefaultValues);

        try
        {
            logs.AddLogMessage("Settuping connection to head {0} {1} {2}", head.Id, head.Name, head.Ip);
            _logger.LogInformation("Settuping connection to head {0} {1} {2}", head.Id, head.Name, head.Ip);
            if (!SetupCommunicationAndAcquireData(head))
            {
                logs.AddLogMessage("Could not connect to screw head, exiting job!!");
                _logger.LogError("Could not connect to screw head, exiting job!!");
                throw new ConnectionAbortedException("Could not connect to screw head, exiting job!!");
            }

            if (!ParseWeberData(ref dataTable, head))
            {
                logs.AddLogMessage("Could not parse screw head data, exiting job!!");
                _logger.LogError("Could not parse screw head data, exiting job!!");
                throw new DataException("Could not parse screw head data, exiting job!!");
            }

            string contents = dataTable.ToCsv();
            string directoryName = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

            if (directoryName != null)
            {
                string outputDir = Path.Combine(directoryName, "Output");
                if (!Directory.Exists(outputDir))
                {
                    _ = Directory.CreateDirectory(outputDir);
                }

                File.WriteAllText(Path.Combine(directoryName, "Output", head.GetFileName()), contents);
                _logger.LogInformation("Head id: {0} saved output file: {1}", head.Id, head.GetFileName());
                logs.AddLogMessage("Head id: {0} saved output file: {1}", head.Id, head.GetFileName());
            }
            else
            {
                logs.AddLogMessage("Data downloaded, but could not find output folder, exiting job!!");
                _logger.LogError("Data downloaded, but could not find output folder, exiting job!!");
                throw new InvalidOperationException("Data downloaded, but could not find output folder, exiting job!!");
            }

            jobLog.Finish = WbrX.Now;
            jobLog.WithoutException = true;
        }
        catch (Exception e)
        {
            _ex = e;
            jobLog.Finish = WbrX.Now;
            jobLog.Exception = $"{e.Message}\n{e.StackTrace}";
            jobLog.WithoutException = false;
            logs.AddLogMessage("Error while running job for head id: {0} name: {1} EXCEPTION: {2}-{3}", headId, headName, e.Message, e.StackTrace);
            _logger.LogError(e, "Error while running job for head id: {0} name: {1}", headId, headName);
        }
        finally
        {
            ReleaseWeberResources();
            logs.AddLogMessage("job completed {0},{1},{2}", headId, headName, headLocation);
            _logger.LogInformation("job completed {0},{1},{2}", headId, headName, headLocation);

            jobLog.JobLogs = logs.GetLogs();
            bool res = _dbManager.AddJobLog(jobLog).Result;
            if (!res)
            {
                _logger.LogInformation("SaveLog save for head FAILED");
            }

            if (_ex != null)
            {
                throw _ex;
            }
        }
    }

    #region Private methods
    private void ReleaseWeberResources()
    {
        _varComm.CloseConnection(true);
        _varComm = null;
        _commonFunctions = null;
    }

    private bool SetupCommunicationAndAcquireData(Head head)
    {
        _varComm = new Services.VarComm(_logger, IPAddress.Parse(head.Ip));
        if (!_varComm.ReceiveVarBlock(32))
        {
            _logger.LogError("Could not receive StatSampleBlock!");
            return false;
        }
        _commonFunctions = new C_CommonFunctions();
        return true;
    }

    private bool ParseWeberData(ref DataTable dataTable, Head head)
    {
        dataTable.Rows.Clear();
        if (_varComm.StatSample.Info.Length > 0)
        {
            int num5 = _varComm.StatSample.Info.Position - 1;
            try
            {
                for (int i = 0; i < _varComm.StatSample.Info.Length; i++)
                {
                    if (_varComm.StatSample.Data.ElementAtOrDefault(num5) != null)
                    {
                        DataRow newRow = dataTable.NewRow();
                        newRow[Nr] = i + 1;
                        string text = _commonFunctions.ByteToString(_varComm.StatSample.Data[num5].ProgName);
                        newRow[ProgNum] = text.Length == 0 ? _varComm.StatSample.Data[num5].ProgNum.ToString() : (object)text;
                        double num6;
                        string empty10;
                        if ((_varComm.StatSample.Data[num5].Valid & 1) > 0)
                        {
                            empty10 = _commonFunctions.GetResName(_varComm.StatSample.Data[num5].ResultParam1) + "(" + (_varComm.StatSample.Data[num5].ResultStep1 + 1) + "): ";
                            float num4 = (1f + _commonFunctions.GetResFactor(_varComm.StatSample.Data[num5].ResultParam1) * (_varComm.TorqueConvert - 1f)) * _varComm.StatSample.Data[num5].Res1;
                            string str = empty10;
                            num6 = WbrY.Round(num4, _commonFunctions.GetResDigits(_varComm.StatSample.Data[num5].ResultParam1));
                            empty10 = str + num6.ToString(CultureInfo.InvariantCulture);
                            empty10 += _commonFunctions.GetResUnit(_varComm.StatSample.Data[num5].ResultParam1, _varComm.TorqueUnitName);
                        }
                        else
                        {
                            empty10 = "NotValid";
                        }
                        newRow[Res1] = empty10;
                        if ((_varComm.StatSample.Data[num5].Valid & 2) > 0)
                        {
                            empty10 = _commonFunctions.GetResName(_varComm.StatSample.Data[num5].ResultParam2) + "(" + (_varComm.StatSample.Data[num5].ResultStep2 + 1) + "): ";
                            float num4 = (1f + _commonFunctions.GetResFactor(_varComm.StatSample.Data[num5].ResultParam2) * (_varComm.TorqueConvert - 1f)) * _varComm.StatSample.Data[num5].Res2;
                            string str2 = empty10;
                            num6 = WbrY.Round(num4, _commonFunctions.GetResDigits(_varComm.StatSample.Data[num5].ResultParam2));
                            empty10 = str2 + num6.ToString(CultureInfo.InvariantCulture);
                            empty10 += _commonFunctions.GetResUnit(_varComm.StatSample.Data[num5].ResultParam2, _varComm.TorqueUnitName);
                        }
                        else
                        {
                            empty10 = "NotValid";
                        }
                        newRow[Res2] = empty10;
                        if ((_varComm.StatSample.Data[num5].Valid & 4) > 0)
                        {
                            empty10 = _commonFunctions.GetResName(_varComm.StatSample.Data[num5].ResultParam3) + "(" + (_varComm.StatSample.Data[num5].ResultStep3 + 1) + "): ";
                            float num4 = (1f + _commonFunctions.GetResFactor(_varComm.StatSample.Data[num5].ResultParam3) * (_varComm.TorqueConvert - 1f)) * _varComm.StatSample.Data[num5].Res3;
                            string str3 = empty10;
                            num6 = WbrY.Round(num4, _commonFunctions.GetResDigits(_varComm.StatSample.Data[num5].ResultParam3));
                            empty10 = str3 + num6.ToString(CultureInfo.InvariantCulture);
                            empty10 += _commonFunctions.GetResUnit(_varComm.StatSample.Data[num5].ResultParam3, _varComm.TorqueUnitName);
                        }
                        else
                        {
                            empty10 = "NotValid";
                        }
                        newRow[Res3] = empty10;
                        empty10 = _varComm.StatSample.Data[num5].IONIO switch
                        {
                            1 => "OK",
                            0 => "n. def.",
                            _ => "NOK: NIO" + _varComm.StatSample.Data[num5].IONIO + "(" + (_varComm.StatSample.Data[num5].LastStep + 1) + ")",
                        };
                        newRow[Ionio] = empty10;
                        newRow[Duration] = _varComm.StatSample.Data[num5].ScrewDuration.ToString("f" + 2) + "Second";
                        newRow[Id] = _commonFunctions.ByteToString(_varComm.StatSample.Data[num5].ScrewID);
                        newRow[Cycle] = _varComm.StatSample.Data[num5].Cycle;
                        WbrX dateTime;
                        try
                        {
                            dateTime = new WbrX(_varComm.StatSample.Data[num5].Time.Year, _varComm.StatSample.Data[num5].Time.Month, _varComm.StatSample.Data[num5].Time.Day, _varComm.StatSample.Data[num5].Time.Hour, _varComm.StatSample.Data[num5].Time.Minute, _varComm.StatSample.Data[num5].Time.Second);
                        }
                        catch
                        {
                            dateTime = new WbrX(1, 1, 1, 0, 0, 0);
                        }
                        newRow[Time] = dateTime.ToString(CultureInfo.InvariantCulture);
                        dataTable.Rows.Add(newRow);
                    }
                    num5--;
                    if (num5 < 0)
                    {
                        num5 = 1999;
                    }
                }
                return true;
            }
            catch (Exception exi)
            {
                _logger.LogError(exi, "Error while loading results from head id:'{0}', name:'{1}', ip:'{2}'", head.Id, head.Name, head.Ip);
                return false;
            }
        }
        _logger.LogError("No data to download in screw head id:'{0}', name:'{1}', ip:'{2}'", head.Id, head.Name, head.Ip);
        return false;
    }
    #endregion
}
