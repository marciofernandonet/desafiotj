namespace bookcatalog.Dtos.Book
{
    public class GetBookDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public int Edicao { get; set; }
        public string AnoPublicacao { get; set; }
        public List<GetBookAuthorDto> LivroAutor { get; set; }
        public List<GetBookSubjectDto> LivroAssunto { get; set; }
    }
}