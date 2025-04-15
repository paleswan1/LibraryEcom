using System.ComponentModel.DataAnnotations.Schema;
using LibraryEcom.Domain.Common.Base;
using LibraryEcom.Domain.Entities.Identity;

namespace LibraryEcom.Domain.Entities;

public class Cart: BaseEntity<Guid>
{
    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    public User User { get; set; }
    
}