using System.ComponentModel.DataAnnotations;

namespace bookcatalog.Models;

public class Subject
{
    public int Id { get; set; }
    
    [StringLength(20)]
    public required string Descricao { get; set; }

    public List<BookSubject> LivroAssunto { get; set; }
}