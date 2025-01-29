using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApp.DbModels;

public class Head
{
    public Head()
    {
        AddidionalValues = new HashSet<AdditionalValue>();
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string Hall { get; set; }
    public string CronExp { get; set; }
    public string Ip { get; set; }
    public ICollection<AdditionalValue> AddidionalValues { get; set; }

}