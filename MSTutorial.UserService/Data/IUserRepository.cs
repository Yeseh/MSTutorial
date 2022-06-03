using MSTutorial.UserService.Models;

namespace MSTutorial.UserService.Data;

public interface IUserRepository
{
    public IEnumerable<UserModel> GetAllUsers();

    public UserModel GetUser(int userId);

    public bool CreateUser(UserModel user);

    public bool UpdateUser(UserModel user);

    public bool UserExists(int userId);
    
    public bool UserEmailExists(string email);

    public bool DeleteUser(int userId);

    public bool SaveChanges();
}
