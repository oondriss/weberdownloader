using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TestApp.Services;
using TestApp.ViewModel;

namespace TestApp.Controllers
{
	public class HomeController : Controller
    {
		private readonly IDbManager _dbManager;

		public HomeController(IDbManager dbManager)
		{
			_dbManager = dbManager;
		}

        public IActionResult Index()
        {
	        if (!_dbManager.IsConfigurationComplete())
			{
				return RedirectToAction("Index", "Configuration");
			}
            return Redirect("/jobs");
        }

        public IActionResult Error()
        {
			return View(new ErrorViewModel
			{
				RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
			});
        }
    }
}
