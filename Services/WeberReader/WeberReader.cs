using System.Threading;
using Microsoft.Extensions.Logging;

namespace TestApp.Services
{
	public class WeberReader : IWeberReader
    {
		private ILogger _logger;
		public WeberReader(ILogger<WeberReader> logger)
		{
			_logger = logger;
		}
		public void ReadWeberData()
		{
			Thread.Sleep(2000);
			
			Thread.Sleep(5000);
		}
    }
}
