using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestApp.DbModels;

namespace TestApp.ViewModel
{
	public class ConfigurationIndexViewModel
    {
		public ConfigurationIndexViewModel()
		{
			
		}

	    public void FillHeaNewAddColls()
	    {
		    HeaNewAddColls = new Dictionary<int, string>();
		    foreach (var item in AddColumns)
		    {
			    HeaNewAddColls.Add(item.Id, "");
		    }
		}

		public ConfigurationIndexViewModel(IEnumerable<AdditionalColumn> addcolls, IEnumerable<Head> heads, bool configurationReadyToStart, bool jobsRunning)
		{
			AddColumns = addcolls;
			Heads = heads;
			ConfigurationReadyToStart = configurationReadyToStart;
			JobsRunning = jobsRunning;
		}

		public bool JobsRunning { get; set; }
		public bool ConfigurationReadyToStart { get; set; }
		public IEnumerable<AdditionalColumn> AddColumns { get; set; }

		[Display(Name = "Name")]
		public string AddNewName { get; set; }

		[Display(Name = "Description")]
		public string AddNewDesc { get; set; }

		
		public IEnumerable<Head> Heads { get; set; }

	    [Display(Name = "Name")]
		public string HeaNewName { get; set; }

	    [Display(Name = "Location")]
		public string HeaNewLocation { get; set; }

	    [Display(Name = "Hall")]
		public string HeaNewHall { get; set; }

		[Display(Name = "First start job at")]
		public DateTime FirstTimeStart { get; set; }

		[Display(Name = "Run job every x hours")]
		public int RunEveryHours { get; set; }

		[Display(Name = "Scheduler expression")]
		public string HeaNewCronExp { get; set; }

	    public Dictionary<int, string> HeaNewAddColls { get; set; }
	}
}
