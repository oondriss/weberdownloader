using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.DbModels;

namespace TestApp.Services
{
	public interface IDbManager
	{
		bool IsConfigurationComplete();
		IEnumerable<Head> GetHeads();
		Task<Head> GetHead(int id);
		Task<bool> RemoveAdditionalColumn(int id);
		IEnumerable<AdditionalColumn> GetAdditionalColumns();
		Task<bool> AddAdditionalColumn(string addNewName, string addNewDesc);
	}
}