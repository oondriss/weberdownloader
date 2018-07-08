using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NCrontab;
using TestApp.DbModels;

namespace TestApp.Services
{
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

	    public async Task<bool> RemoveHeadAndAdditionalValues(int id)
	    {
		    bool result;
		    var itemToRemove = await _db.Heads.FindAsync(id);
		    if (itemToRemove != null)
		    {
			    var addValuesToDelete = _db.AdditionalValues.Where(i => i.Head == itemToRemove);
				_db.AdditionalValues.RemoveRange(addValuesToDelete);

			    _db.Heads.Remove(itemToRemove);
			    try
			    {
				    await _db.SaveChangesAsync();
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

	    public async Task<bool> AddHead(string name, string location, string hall, string cron, Dictionary<int, string> addValues)
	    {
		    
		    
		    var newHead = new Head
		    {
			    Name = name,
			    Location = location,
			    Hall = hall,
			    CronExp = cron
			};

			_db.Heads.Add(newHead);

		    foreach (var addValue in addValues)
		    {
			    var addColl = await _db.AdditionalColumns.FindAsync(addValue.Key);
			    var newAddValue = new AdditionalValue
			    {
				    Column = addColl,
				    Head = newHead,
				    Value = addValue.Value
			    };
			    _db.AdditionalValues.Add(newAddValue);
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
			var itemToRemove = await _db.AdditionalColumns.FindAsync(id);
			if (itemToRemove != null)
			{
				_db.AdditionalColumns.Remove(itemToRemove);
				try
				{
					await _db.SaveChangesAsync();
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
				_db.AdditionalColumns.Add(new AdditionalColumn
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
	}
}
