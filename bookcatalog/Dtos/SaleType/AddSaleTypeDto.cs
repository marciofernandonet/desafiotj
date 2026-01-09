namespace bookcatalog.Dtos.SaleType;

public class AddSaleTypeDto
{
    public string Tipo { get; set; }
    public decimal Preco { get; set; }
    public int LivroId { get; set; }
}