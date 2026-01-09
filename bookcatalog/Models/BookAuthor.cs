namespace bookcatalog.Models;

public class BookAuthor
{
    public int Id { get; set; }
    
    public int LivroId { get; set; }
    public Book Livro { get; set; }

    public int AutorId { get; set; }
    public Author Autor { get; set; }
}