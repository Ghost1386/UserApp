using UserApp.Models.Models;

namespace UserApp.DAL.Interfaces;

public interface IRoleRepository
{
   void CreateList(List<Role> roles);
   
   void Create(Role role);
}