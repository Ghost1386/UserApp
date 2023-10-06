using UserApp.Common.Enums;

namespace UserApp.Common.DTOs;

public class UserSortDto
{
    public int Page { get; set; }
    
    public SortEnum SortEnum { get; set; }
    
    public string? Name { get; set; }
    
    public int Age { get; set; }
    
    public string? Email { get; set; }
    
    public RoleEnum RoleEnum { get; set; }
}