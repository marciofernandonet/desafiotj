namespace bookcatalog.Models;
public class BookSubject
{
    public int LivroId { get; set; }
    public Book Livro { get; set; }

    public int AssuntoId { get; set; }
    public Subject Assunto { get; set; }
}