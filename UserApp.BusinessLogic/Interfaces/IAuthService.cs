using UserApp.Common.DTOs;

namespace UserApp.BusinessLogic.Interfaces;

public interface IAuthService
{
    string Login(UserAuthDto userAuthDto);

    bool Register(UserCreateDto userCreateDto);
}