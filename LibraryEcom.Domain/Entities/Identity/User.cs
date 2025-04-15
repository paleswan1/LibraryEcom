using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryEcom.Domain.Common.Enum;

namespace LibraryEcom.Domain.Entities.Identity;

public class User : IdentityUser<Guid>
{
    public string Username { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }

    public GenderType Gender { get; set; } 
    
    public string PasswordHash { get; set; }
    
    public string? Address { get; set; }
    
    public string? ImageURL { get; set; }
    
    public DateTime RegisteredDate { get; set; } = DateTime.UtcNow;
    
    public bool IsActive { get; set; }
    
    public int TotalOrders { get; set; }

    

    
}