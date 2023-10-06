using UserApp.Common.DTOs;
using UserApp.Common.Enums;
using UserApp.Models.Models;

namespace UserApp.BusinessLogic.Interfaces;

public interface IUserService
{
    Task<List<UserGetDto>> GetAll(Identifier identifier);

    Task<UserGetDto> Get(Identifier identifier);

    User Get(UserAuthDto userAuthDto);

    void AddRole(Identifier identifier, RoleEnum roleEnum);

    bool Create(UserCreateDto userCreateDto);

    void Update(UserUpdateDto userUpdateDto);

    void Delete(Identifier identifier);
}