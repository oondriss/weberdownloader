using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestApp.Services.DbManager;

public class DbManager : IDbManager
{
    private readonly DatabaseContext _db;
    private readonly ILogger<DbManager> _logger;

    public DbManager(DatabaseContext db, ILogger<DbManager> logger)
    {
        _db = db;
        _logger = logger;
    }

    public bool IsConfigurationComplete()
    {
        return _db.Heads.Any();
    }

    public async Task<IQueryable<Head>> GetHeadsAsync()
    {
        await _db.Heads.LoadAsync();
        return _db.Heads;
    }

    public async Task<Head> GetHead(int id)
    {
        return await _db.Heads.FindAsync(id);
    }

    public IEnumerable<AdditionalValue> GetAdditionalValues(Head head)
    {
        return _db.AdditionalValues.Where(i => i.Head == head);
    }

    public async Task<bool> RemoveHeadAndAdditionalValues(int id)
    {
        bool result;
        Head itemToRemove = await _db.Heads.FindAsync(id);
        if (itemToRemove != null)
        {
            IQueryable<AdditionalValue> addValuesToDelete = _db.AdditionalValues.Where(i => i.Head == itemToRemove);
            _db.AdditionalValues.RemoveRange(addValuesToDelete);
            _db.JobLogs.RemoveRange(_db.JobLogs.Where(i => i.Head == itemToRemove));
            _ = _db.Heads.Remove(itemToRemove);
            try
            {
                _ = await _db.SaveChangesAsync();
                result = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed while deleting HEAD with Id: {0}", id);
                result = false;
            }
        }
        else
        {
            result = false;
            _logger.LogError("Failed while deleting HEAD with Id: {0}, no record found", id);
        }
        return result;
    }

    public async Task<bool> AddHead(string name, string location, string hall, string cron, string ip, Dictionary<int, string> addValues)
    {
        Head newHead = new()
        {
            Name = name,
            Location = location,
            Hall = hall,
            CronExp = cron,
            Ip = ip
        };

        _ = _db.Heads.Add(newHead);

        if (addValues != null && addValues.Keys.Count > 0)
        {
            foreach (KeyValuePair<int, string> addValue in addValues)
            {
                AdditionalColumn addColl = await _db.AdditionalColumns.FindAsync(addValue.Key);
                AdditionalValue newAddValue = new()
                {
                    Column = addColl,
                    Head = newHead,
                    Value = addValue.Value
                };
                _ = _db.AdditionalValues.Add(newAddValue);
            }
        }

        try
        {
            return await _db.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while saving new head");
            return false;
        }
    }

    public IEnumerable<AdditionalColumn> GetAdditionalColumns()
    {
        return _db.AdditionalColumns;
    }

    public async Task<bool> RemoveAdditionalColumn(int id)
    {
        bool result;
        AdditionalColumn itemToRemove = await _db.AdditionalColumns.FindAsync(id);
        if (itemToRemove != null)
        {
            _ = _db.AdditionalColumns.Remove(itemToRemove);
            try
            {
                _ = await _db.SaveChangesAsync();
                result = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed while deleting ADDITIONALCOLUMN with Id: {0}", id);
                result = false;
            }
        }
        else
        {
            result = false;
            _logger.LogError("Failed while deleting ADDITIONALCOLUMN with Id: {0}, no record found", id);
        }
        return result;
    }

    public async Task<bool> AddAdditionalColumn(string addNewName, string addNewDesc)
    {
        try
        {
            _ = _db.AdditionalColumns.Add(new AdditionalColumn
            {
                Name = addNewName,
                Description = addNewDesc
            });

            return await _db.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception while inserting additional column: name={0}, desc={1}", addNewName, addNewDesc);
            return false;
        }
    }

    public async Task<bool> AddJobLog(JobLog instance)
    {
        try
        {
            _ = await _db.JobLogs.AddAsync(instance);
            return await _db.SaveChangesAsync() > 0;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while saving JobLog");
            return false;
        }
    }
}
