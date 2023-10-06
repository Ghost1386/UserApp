using System.ComponentModel.DataAnnotations;

namespace UserApp.Common.DTOs;

public class UserCreateDto
{
    [Required]
    [DataType(DataType.Text)]
    public string? Name { get; set; }
    
    [Required]
    [Range(0, 150)]
    public int Age { get; set; }
    
    [Required]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    
    [Required]
    public List<int>? Roles { get; set; }
}