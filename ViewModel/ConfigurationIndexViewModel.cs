using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestApp.DbModels;

namespace TestApp.ViewModel
{
	public class ConfigurationIndexViewModel
    {
		public ConfigurationIndexViewModel(IEnumerable<AdditionalColumn> addcolls, IEnumerable<Head> heads)
		{
			AddColumns = addcolls;
			Heads = heads;
			HeaNewAddColls = new Dictionary<int, string>();
			foreach (var item in AddColumns)
			{
				HeaNewAddColls.Add(item.Id, "");
			}
		}

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
		[Display(Name = "Scheduler expression")]
		public string HeaNewCronExp { get; set; }
		public Dictionary<int, string> HeaNewAddColls { get; set; }
	}
}
