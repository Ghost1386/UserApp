using UserApp.BusinessLogic.Interfaces;
using UserApp.Common.DTOs;
using UserApp.Common.Enums;
using UserApp.DAL.Interfaces;
using UserApp.Models.Models;

namespace UserApp.BusinessLogic.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IHashService _hashService;

    private const int NumberUsersForPage = 1;

    public UserService(IUserRepository userRepository, IRoleRepository roleRepository, 
        IHashService hashService)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _hashService = hashService;
    }

    public async Task<List<UserGetDto>> GetAll(Identifier identifier)
    {
        var users = await _userRepository.GetAll();

        var listUserGetDto = users.Select(user => new UserGetDto
        {
            Id = user.Id,
            Name = user.Name,
            Age = user.Age,
            Email = user.Email,
            Roles = user.Roles
                .Select(role => ((RoleEnum)role.RoleEnum).ToString())
                .ToList()
        }).ToList();

        var usersForPage = listUserGetDto.Skip((identifier.Id - 1) * NumberUsersForPage)
            .Take(NumberUsersForPage)
            .ToList();

        return usersForPage;
    }

    public async Task<UserGetDto> Get(Identifier identifier)
    {
        var user = await _userRepository.GetWithRoles(identifier);
        
        var userGetDto = new UserGetDto
        {
            Id = user.Id,
            Name = user.Name,
            Age = user.Age,
            Email = user.Email,
            Roles = user.Roles
                .Select(role => ((RoleEnum)role.RoleEnum).ToString())
                .ToList()
        };

        return userGetDto;
    }
    
    public User Get(UserAuthDto userAuthDto)
    {
        var user = _userRepository.Get(userAuthDto);
        
        if (!_hashService.VerifyPasswordHash(userAuthDto.Password, user.PasswordSalt, user.PasswordHash))
        {
            return new User();
        }

        return user;
    }

    public void AddRole(Identifier identifier, RoleEnum roleEnum)
    {
        var user = _userRepository.Get(identifier);

        var newRole = new Role
        {
            RoleEnum = (int)roleEnum,
            User = user
        };
        
        _roleRepository.Create(newRole);
    }

    public bool Create(UserCreateDto userCreateDto)
    {
        if (!_userRepository.IsCreated(userCreateDto.Email))
        {
            _hashService.CreatePasswordHash(userCreateDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            
            var user = new User
            {
                Name = userCreateDto.Name,
                Age = userCreateDto.Age,
                Email = userCreateDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
        
            _userRepository.Create(user);

            var roleList = userCreateDto.Roles.Select(role => new Role
            {
                RoleEnum = role,
                User = user
            }).ToList();
        
            _roleRepository.CreateList(roleList);

            return true;
        }

        return false;
    }

    public void Update(UserUpdateDto userUpdateDto)
    {
        var identifier = new Identifier
        {
            Id = userUpdateDto.Id
        };
        
        var user = _userRepository.Get(identifier);
        
        _hashService.CreatePasswordHash(userUpdateDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
        
        user.Name = userUpdateDto.Name;
        user.Age = userUpdateDto.Age;
        user.Email = userUpdateDto.Email;
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        
        _userRepository.Update(user);
    }

    public void Delete(Identifier identifier)
    {
        _userRepository.Delete(identifier);
    }
}