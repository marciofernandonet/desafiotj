using System.ComponentModel.DataAnnotations;

namespace bookcatalog.Models;

public class Author
{
    public int Id { get; set; }

    [StringLength(40)]
    public required string Nome { get; set; }
    
    public List<BookAuthor> LivroAutor { get; set; }
}