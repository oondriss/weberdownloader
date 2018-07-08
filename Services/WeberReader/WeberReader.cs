using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Extensions.Logging;
using Hangfire.Common;

namespace TestApp.Services
{
	public class WeberReader : IWeberReader
    {
		private readonly ILogger _logger;

		public WeberReader(ILogger<WeberReader> logger)
		{
			_logger = logger;
		}

		public void ReadWeberData(int headId, string headName, string headLocation)
		{
			_logger.LogInformation("job started {0},{1},{2}", headId, headName, headLocation);
			Thread.Sleep(2000);
			_logger.LogInformation("job completed {0},{1},{2}", headId, headName, headLocation);
		}
    }
}
