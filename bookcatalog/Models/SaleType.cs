namespace bookcatalog.Models;

public class SaleType
{
    public int Id { get; set; }
    public string Tipo { get; set; }
    public decimal Preco { get; set; }
    public int LivroId { get; set; }
    public Book Livro { get; set; }
}