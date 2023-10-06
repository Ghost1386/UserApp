using UserApp.Common.DTOs;
using UserApp.Models.Models;

namespace UserApp.DAL.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetAll();
    
    Task<User> GetWithRoles(Identifier identifier);
    
    User Get(Identifier identifier);

    User Get(UserAuthDto userAuthDto);

    void Create(User user);

    void Update(User user);
    
    void Delete(Identifier identifier);

    bool IsCreated(string email);
}