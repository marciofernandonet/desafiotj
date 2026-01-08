namespace bookcatalog.Models;
using System.ComponentModel.DataAnnotations;

public class Book
{
    public int Id { get; set; }
    
    [StringLength(40)]
    public required string Titulo { get; set; }
    
    [StringLength(40)]
    public required string Editora { get; set; }

    public int Edicao { get; set; }

    [StringLength(4)]
    public required string AnoPublicacao { get; set; }

    public List<BookAuthor> LivroAutor { get; set; }
    public List<BookSubject> LivroAssunto { get; set; }
}