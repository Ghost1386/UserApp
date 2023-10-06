using Microsoft.AspNetCore.Mvc;
using UserApp.BusinessLogic.Interfaces;
using UserApp.Common.DTOs;

namespace UserApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("/login")]
    public IActionResult Login(UserAuthDto userAuthDto)
    {
        try
        {
            var token = _authService.Login(userAuthDto);
            
            if (!string.IsNullOrEmpty(token))
            {
                _logger.LogInformation($"{DateTime.UtcNow}: user with {userAuthDto.Email} is login");
                
                return StatusCode(200, token);
            }

            return StatusCode(401);
        }
        catch (Exception e)
        {
            _logger.LogError($"{DateTime.UtcNow}: {e.Message}");
            
            return StatusCode(400, e.Message);
        }
    }

    [HttpPost("/register")]
    public IActionResult Register(UserCreateDto userCreateDto)
    {
        try
        {
            var isCreated = _authService.Register(userCreateDto);
            
            if (isCreated)
            {
                _logger.LogInformation($"{DateTime.UtcNow}: user with {userCreateDto.Email} is registered");
                
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
}