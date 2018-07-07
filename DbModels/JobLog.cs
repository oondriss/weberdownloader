using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApp.DbModels
{
	public class JobLog
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public Head Head { get; set; }
		public DateTime Start { get; set; }
		public DateTime Finish { get; set; }
		public bool WithoutException { get; set; }
		public string Exception { get; set; }

	}
}