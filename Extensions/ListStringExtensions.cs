using System;
using System.Collections.Generic;

namespace TestApp.Extensions;

public static class ListStringExtensions
{
    public static void AddLogMessage(this List<string> logs, string message, params object[] args)
    {
        logs ??= [];
        string mess = DateTime.Now.ToString("o");
        mess = mess + "-" + string.Format(message, args);
        logs.Add(mess);
    }

    public static string GetLogs(this List<string> logs)
    {
        string result =
            logs != null
                ? string.Join(Environment.NewLine, logs)
                : string.Empty;

        return result;
    }
}
