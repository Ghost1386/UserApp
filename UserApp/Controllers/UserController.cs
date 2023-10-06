using Microsoft.AspNetCore.Mvc;
using UserApp.BusinessLogic.Interfaces;
using UserApp.Common.DTOs;
using UserApp.Common.Enums;

namespace UserApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;
    
    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }
    
    [HttpGet("/getAll")]
    public IActionResult GetAll([FromQuery]UserSortDto userSortDto)
    {
        try
        {
            var users = _userService.GetAll(userSortDto);

            return StatusCode(200, users);
        }
        catch (Exception e)
        {
            _logger.LogError($"{DateTime.UtcNow}: {e.Message}");
            
            return StatusCode(400, e.Message);
        }
    }
    
    [HttpGet("/get")]
    public async Task<IActionResult> Get([FromQuery]Identifier identifier)
    {
        try
        {
            var user = await _userService.Get(identifier);

            return StatusCode(200, user);
        }
        catch (Exception e)
        {
            _logger.LogError($"{DateTime.UtcNow}: {e.Message}");
            
            return StatusCode(400, e.Message);
        }
    }
    
    [HttpPut("/addRole")]
    public IActionResult AddRole(Identifier identifier, RoleEnum roleEnum)
    {
        try
        {
            _userService.AddRole(identifier, roleEnum);
            
            _logger.LogInformation($"{DateTime.UtcNow}: added role {roleEnum} for user {identifier}");

            return StatusCode(201);
        }
        catch (Exception e)
        {
            _logger.LogError($"{DateTime.UtcNow}: {e.Message}");
            
            return StatusCode(400, e.Message);
        }
    }
    
    [HttpPost("/create")]
    public IActionResult Create(UserCreateDto userCreateDto)
    {
        try
        {
            var isCreated = _userService.Create(userCreateDto);

            if (isCreated)
            {
                _logger.LogInformation($"{DateTime.UtcNow}: added new user {userCreateDto.Email}");
                
                return StatusCode(201);
            }

            return StatusCode(403, "User with this email already exists");
        }
        catch (Exception e)
        {
            _logger.LogError($"{DateTime.UtcNow}: {e.Message}");
            
            return StatusCode(400, e.Message);
        }
    }
    
    [HttpPut("/update")]
    public IActionResult Update(UserUpdateDto userUpdateDto)
    {
        try
        {
            _userService.Update(userUpdateDto);
            
            _logger.LogInformation($"{DateTime.UtcNow}: update user {userUpdateDto.Id}");
            
            return StatusCode(202);
        }
        catch (Exception e)
        {
            _logger.LogError($"{DateTime.UtcNow}: {e.Message}");
            
            return StatusCode(400, e.Message);
        }
    }
    
    [HttpDelete("/delete")]
    public IActionResult Delete(Identifier identifier)
    {
        try
        {
            _userService.Delete(identifier);
            
            _logger.LogInformation($"{DateTime.UtcNow}: update user {identifier.Id}");
            
            return StatusCode(200);
        }
        catch (Exception e)
        {
            _logger.LogError($"{DateTime.UtcNow}: {e.Message}");
            
            return StatusCode(400, e.Message);
        }
    }
}