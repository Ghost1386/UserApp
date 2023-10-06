using Microsoft.EntityFrameworkCore;
using UserApp.Common.DTOs;
using UserApp.DAL.Interfaces;
using UserApp.Models;
using UserApp.Models.Models;

namespace UserApp.DAL.Repositorys;

public class UserRepository : IUserRepository
{
    private readonly ApplicationContext _applicationContext;

    public UserRepository(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    
    public async Task<List<User>> GetAll()
    {
        var users = await _applicationContext.Users
            .Include(u => u.Roles)
            .ToListAsync();

        return users;
    }
    
    public async Task<User> GetWithRoles(Identifier identifier)
    {
        var user = await _applicationContext.Users
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Id == identifier.Id);

        return user;
    }
    
    public User Get(Identifier identifier)
    {
        var user = _applicationContext.Users
            .FirstOrDefault(u => u.Id == identifier.Id);

        return user;
    }
    
    public User Get(UserAuthDto userAuthDto)
    {
        var user = _applicationContext.Users
            .FirstOrDefault(u => u.Email == userAuthDto.Email);
        
        return user;
    }

    public async void Create(User user)
    {
        await _applicationContext.Users.AddAsync(user);
    }

    public void Update(User user)
    {
        _applicationContext.Users.Update(user);
        _applicationContext.SaveChanges();
    }

    public void Delete(Identifier identifier)
    {
        _applicationContext.Users.Remove(Get(identifier));
        _applicationContext.SaveChanges();
    }
    
    public bool IsCreated(string email)
    {
        var isCreated = _applicationContext.Users.Any(user => user.Email == email);

        return isCreated;
    }
}