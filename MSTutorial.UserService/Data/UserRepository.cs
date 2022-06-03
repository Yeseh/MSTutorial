using MSTutorial.UserService.Models;

namespace MSTutorial.UserService.Data;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public bool CreateUser(UserModel user)
    {
        if (user is null) { throw new ArgumentNullException(nameof(user)); }
        _context.Add(user);
        return SaveChanges();
    }

    public bool DeleteUser(int userId)
    {
        var model = _context.Users.FirstOrDefault(u=>u.Id==userId);
        var deleted = false;
        if (model is not null) 
        { 
            _context.Remove(model); 
            deleted = SaveChanges(); 
        }
        return deleted;
    }

    public bool UserExists(int userId)
    {
        return _context.Users.Any(u => u.Id == userId);
    }
    
    public bool UserEmailExists(string email)
    {
        return _context.Users.Any(u => u.EmailAddress == email);
    }

    public IEnumerable<UserModel> GetAllUsers()
    {
        return _context.Users.ToList();
    }

    public UserModel? GetUser(int userId)
    {
        return _context.Users.FirstOrDefault(u => u.Id == userId);
    }

    public bool UpdateUser(UserModel user)
    {
        if (user is null) { throw new ArgumentNullException(nameof(user)); }
       _context.Update(user);

       return SaveChanges();
    }

    public bool SaveChanges()
    {
        return _context.SaveChanges() >= 0;
    }
}
