using System;
using System.Collections.Generic;

namespace TestApp.Extensions
{
    public static class ListStringExtensions
    {
	    public static void AddLogMessage(this List<string> logs, string message, params object[] args)
	    {
		    if (logs == null)
		    {
			    logs = new List<string>();
		    }
			var mess = DateTime.Now.ToString("o");
			mess = mess + "-" + string.Format(message, args);
		    logs.Add(mess);
	    }

	    public static string GetLogs(this List<string> logs)
	    {
		    var result = 
			    logs != null 
				    ? string.Join(Environment.NewLine, logs) 
				    : string.Empty;

		    return result;
	    }
    }
}
