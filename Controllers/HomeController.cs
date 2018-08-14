using Microsoft.AspNetCore.Mvc;
using TestApp.Services;
using TestApp.ViewModel;

namespace TestApp.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IDbManager dbManager)
		{
		}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
			return View(new ErrorViewModel
			{
				RequestId = HttpContext.TraceIdentifier
			});
        }
    }
}
