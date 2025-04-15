using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryEcom.Domain.Entities;

public class OrderItem
{
    [ForeignKey(nameof(Order))]
    public Guid OrderId { get; set; }
    
    [ForeignKey(nameof(Book))]
    public Guid BookId { get; set; }
    
    public int Quantity { get; set; }
    
    public decimal UnitPrice { get; set; }

    public virtual Order Order { get; set; }
    
    public virtual Book Book { get; set; }
}