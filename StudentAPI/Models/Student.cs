using System.ComponentModel.DataAnnotations;

namespace StudentAPI.Models;

public class Student
{
    public int id { get; set; }
    
    [Required]
    public string name { get; set; } = "";

    [Range(10,100)]
    public int age { get; set; }
    
    [Required]
    [EmailAddress]
    public string email { get; set; } = "";
    
    [Required]
    [MinLength(6)]
    public string password { get; set; } = "";

}

