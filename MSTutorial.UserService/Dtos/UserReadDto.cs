namespace MSTutorial.UserService.Dtos;

public class UserReadDto 
{
    public int Id { get; set; } 
    public string GivenName { get; set; }
    public string FamilyName { get; set; }
    public string EmailAddress { get; set; }
}
