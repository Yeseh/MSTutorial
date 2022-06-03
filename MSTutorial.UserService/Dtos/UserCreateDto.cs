using System.ComponentModel.DataAnnotations;

namespace MSTutorial.UserService.Dtos;

public class UserCreateDto 
{
    [Required]
    public string GivenName { get; set; }
    [Required]
    public string FamilyName { get; set; }
    [Required]
    public string EmailAddress { get; set; }
}
