using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApp.DbModels
{
	public class AdditionalValue
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public AdditionalColumn Column { get; set; }
		public Head Head { get; set; }
		public string Value { get; set; }
	}
}