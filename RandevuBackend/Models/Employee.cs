using System.ComponentModel.DataAnnotations;

namespace RandevuBackend.Models;

public class Employee
{   
    [Key]
    public int? id { get; set; }
    public string Name { get; set; }
    public string Skill { get; set; }
    public int CompanyId { get; set; }
    public DateTime CreateUserTime { get; set;}
}