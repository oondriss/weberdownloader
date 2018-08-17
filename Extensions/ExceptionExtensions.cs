using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Extensions
{
    public static class ExceptionExtensions
    {
        public static string GetLogMessage(this Exception ex)
        {
            return $"{DateTime.Now:o}: EXCEPTION: {ex.Message}\n{ex.StackTrace}\n============================================";
        }
    }
}
