namespace bookcatalog.Dtos.Book;

public class UpdateBookDto
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Editora { get; set; }
    public int Edicao { get; set; }
    public string AnoPublicacao { get; set; }
    public int AutorId { get; set; }
    public int AssuntoId { get; set; }
}