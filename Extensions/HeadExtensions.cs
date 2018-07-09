using System;
using TestApp.DbModels;

namespace TestApp.Extensions
{
    public static class HeadExtensions
    {
	    public static string GetFileName(this Head head)
	    {
		    var time = DateTime.Now;
		    return $"export_{head.Name}_{head.Location}_{head.Hall}{time.Year:0000}{time.Month:00}{time.Day:00}-{time.Hour:00}.csv";
		}
    }
}
