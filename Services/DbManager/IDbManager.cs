using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestApp.Services.DbManager;

public interface IDbManager
{
    bool IsConfigurationComplete();
    Task<IQueryable<Head>> GetHeadsAsync();
    Task<Head> GetHead(int id);
    Task<bool> AddHead(string name, string location, string hall, string cron, string ip, Dictionary<int, string> addValues);
    Task<bool> RemoveHeadAndAdditionalValues(int id);
    Task<bool> RemoveAdditionalColumn(int id);
    IEnumerable<AdditionalColumn> GetAdditionalColumns();
    Task<bool> AddAdditionalColumn(string addNewName, string addNewDesc);
    Task<bool> AddJobLog(JobLog instance);
    IEnumerable<AdditionalValue> GetAdditionalValues(Head head);
}