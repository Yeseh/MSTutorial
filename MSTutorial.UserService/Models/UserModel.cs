using System.ComponentModel.DataAnnotations;

namespace MSTutorial.UserService.Models;

public class UserModel
{
    [Key]
    [Required]
    public int Id { get; set; } 
    [Required]
    public string GivenName { get; set; }
    [Required]
    public string FamilyName { get; set; }
    [Required]
    public string EmailAddress { get; set; }
}

