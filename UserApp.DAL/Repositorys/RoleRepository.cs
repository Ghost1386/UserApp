using UserApp.DAL.Interfaces;
using UserApp.Models;
using UserApp.Models.Models;

namespace UserApp.DAL.Repositorys;

public class RoleRepository : IRoleRepository
{
    private readonly ApplicationContext _applicationContext;

    public RoleRepository(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public void CreateList(List<Role> roles)
    {
        _applicationContext.Roles.AddRange(roles);
        _applicationContext.SaveChanges();
    }

    public void Create(Role role)
    {
        _applicationContext.Roles.Add(role);
        _applicationContext.SaveChanges();
    }
}