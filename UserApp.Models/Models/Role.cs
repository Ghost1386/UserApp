namespace UserApp.Models.Models;

public class Role
{
    public int Id { get; set; }
    
    public int RoleEnum { get; set; }
    
    public int UserId { get; set; }
    
    public User? User { get; set; }
}