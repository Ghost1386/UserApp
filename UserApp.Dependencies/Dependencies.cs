using Microsoft.Extensions.DependencyInjection;
using UserApp.BusinessLogic.Interfaces;
using UserApp.BusinessLogic.Services;
using UserApp.DAL.Interfaces;
using UserApp.DAL.Repositorys;

namespace UserApp.Dependencies;

public static class Dependencies
{
    public static void AddIService(this IServiceCollection services)
    {
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IHashService, HashService>();
        services.AddTransient<IUserService, UserService>();
    }
    
    public static void AddIRepository(this IServiceCollection services)
    {
        services.AddTransient<IRoleRepository, RoleRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
    }
}