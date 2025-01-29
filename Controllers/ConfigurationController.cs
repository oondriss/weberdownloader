using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestApp.DTO;

using TestApp.ViewModel;

namespace TestApp.Controllers;

public class ConfigurationController(IDbManager dbManager, IJobManager jobManager, ICronValitador cronValitador, IIpValidator ipValidator) : Controller
{
    private readonly IDbManager _dbManager = dbManager;
    private readonly IJobManager _jobManager = jobManager;
    private readonly ICronValitador _cronValitador = cronValitador;
    private readonly IIpValidator _ipValidator = ipValidator;

    public async Task<IActionResult> Index()
    {
        ConfigurationIndexViewModel model = new(_dbManager.GetAdditionalColumns(), [.. await _dbManager.GetHeadsAsync()],
                                                    _dbManager.IsConfigurationComplete(), _jobManager.IsJobsRunning());
        model.FillHeaNewAddColls();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult StopJobs()
    {
        if (_jobManager.RemoveAllJobs())
        {
            TempData.AddMessage("Jobs successfully removed.", MessageType.Success);
        }
        else
        {
            TempData.AddMessage("Failed while removing jobs, check logs.", MessageType.Danger);
        }

        TempData["tab"] = "3";

        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> StartJobs()
    {
        if (await _jobManager.CreateAllHeadsJobs())
        {
            TempData.AddMessage("Jobs successfully started.", MessageType.Success);
        }
        else
        {
            TempData.AddMessage("Failed while starting jobs, check logs.", MessageType.Danger);
        }

        TempData["tab"] = "3";

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> DeleteHead(string id)
    {
        if (int.TryParse(id, out int aid))
        {
            if (await _dbManager.RemoveHeadAndAdditionalValues(aid))
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

        TempData["tab"] = "2";
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddHead(ConfigurationIndexViewModel model)
    {
        if (ModelState.IsValid)
        {
            NCrontab.CrontabSchedule cronExpr = _cronValitador.ValidateCron(model.HeaNewCronExp);

            if (cronExpr == null)
            {
                TempData.AddMessage("Error validating cron expression, see <a href='https://crontab.guru/#0_3,7,11,15,19,23_*_*_*' target='_blank'>this</a> example.", MessageType.Danger);
                TempData["tab"] = "2";
                return RedirectToAction("Index");
            }

            if (!_ipValidator.ValidateIPv4(model.HeaNewIp))
            {
                TempData.AddMessage("Error validating IP address:'" + model.HeaNewIp + "'", MessageType.Danger);
                TempData["tab"] = "2";
                return RedirectToAction("Index");
            }

            if (await _dbManager.AddHead(model.HeaNewName, model.HeaNewLocation, model.HeaNewHall, cronExpr.ToString(), model.HeaNewIp, model.HeaNewAddColls))
            {
                TempData.AddMessage("Record successfully added.", MessageType.Success);
            }
            else
            {
                TempData.AddMessage("Exception has occured, check logs and try again.", MessageType.Danger);
            }
        }
        else
        {
            TempData.AddMessage("Data was not correctly valitaded, try again.", MessageType.Warning);
        }

        TempData["tab"] = "2";
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> DeleteColumn(string id)
    {
        if (int.TryParse(id, out int aid))
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
