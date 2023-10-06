using System.ComponentModel.DataAnnotations;

namespace UserApp.Common.DTOs;

public class UserAuthDto
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}