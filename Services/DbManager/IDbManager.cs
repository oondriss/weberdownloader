using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.DbModels;

namespace TestApp.Services
{
	public interface IDbManager
	{
		bool IsConfigurationComplete();
		Task<IQueryable<Head>> GetHeadsAsync();
		Task<Head> GetHead(int id);
		Task<bool> AddHead(string name, string location, string hall, string cron, Dictionary<int, string> addValues);
		Task<bool> RemoveHeadAndAdditionalValues(int id);
		Task<bool> RemoveAdditionalColumn(int id);
		IEnumerable<AdditionalColumn> GetAdditionalColumns();
		Task<bool> AddAdditionalColumn(string addNewName, string addNewDesc);
		Task<bool> AddJobLog(JobLog instance);
	}
}