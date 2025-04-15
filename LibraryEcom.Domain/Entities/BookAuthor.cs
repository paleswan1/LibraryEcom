using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryEcom.Domain.Entities;

public class BookAuthor
{
    [ForeignKey(nameof(Book))]
    public Guid BookId { get; set; }
    
    [ForeignKey(nameof(Author))]
    public Guid AuthorId { get; set; }
    
    public virtual Book Book { get; set; }
    
    public virtual Author Author { get; set; }
    
}