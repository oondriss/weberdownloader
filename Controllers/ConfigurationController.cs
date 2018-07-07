using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestApp.Extensions;
using TestApp.Services;
using TestApp.ViewModel;

namespace TestApp.Controllers
{
	public class ConfigurationController : Controller
    {
		private readonly IDbManager _dbManager;

		public ConfigurationController(IDbManager dbManager)
		{
			_dbManager = dbManager;
		}

        public IActionResult Index()
        {
			var model = new ConfigurationIndexViewModel(_dbManager.GetAdditionalColumns(),
														_dbManager.GetHeads());

			return View(model);
        }

	    public IActionResult DeleteHead(string id)
	    {
		    return RedirectToAction("Index");
	    }

		[HttpPost]
		[ValidateAntiForgeryToken]
	    public IActionResult AddHead(ConfigurationIndexViewModel model)
	    {
		    return RedirectToAction("Index");
		}

		public async Task<IActionResult> DeleteColumn(string id)
		{
			if (int.TryParse(id, out var aid))
			{
				if (await _dbManager.RemoveAdditionalColumn(aid))
				{
					TempData.AddMessage("Record successfully deleted.", MessageType.Success);
				}
				else
				{
					TempData.AddMessage("Exception has occured, check logs and try again.", MessageType.Danger);
				}
			}
			else
			{
				TempData.AddMessage($"Bad parameters, cannot remove item {id}", MessageType.Warning);
			}
			return RedirectToAction("Index");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateAdditionalColumn(ConfigurationIndexViewModel model)
		{
			if (ModelState.IsValid)
			{
				if (!await _dbManager.AddAdditionalColumn(model.AddNewName, model.AddNewDesc))
				{
					TempData.AddMessage("Exception has occured, check logs and try again.", MessageType.Danger);
				}
				else
				{
					TempData.AddMessage("Item succesfully saved.", MessageType.Success);
				}
			}
			else
			{
				TempData.AddMessage("Data was not correctly valitaded, try again.", MessageType.Warning);
			}

			return RedirectToAction("Index");
		}
	}
}
