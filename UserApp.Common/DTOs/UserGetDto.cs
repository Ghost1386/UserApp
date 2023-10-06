using UserApp.Common.Enums;

namespace UserApp.Common.DTOs;

public class UserGetDto
{
    public int Id { get; set; }
    
    public string? Name { get; set; }
    
    public int Age { get; set; }
    
    public string? Email { get; set; }

    public List<string>? Roles { get; set; }
}