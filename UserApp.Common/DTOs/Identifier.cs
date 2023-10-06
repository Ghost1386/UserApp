using System.ComponentModel.DataAnnotations;

namespace UserApp.Common.DTOs;

public class Identifier
{
    [Required]
    public int Id { get; set; }
}