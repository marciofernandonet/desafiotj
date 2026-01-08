using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookcatalog.Migrations
{
    /// <inheritdoc />
    public partial class CreateReportView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW vw_RelatorioAutorAssuntoLivro AS
                SELECT
                    a.Id   AS AutorId,
                    a.Nome AS AutorNome,

                    s.Id        AS AssuntoId,
                    s.Descricao AS Assunto,

                    l.Id   AS LivroId,
                    l.Titulo,
                    l.Editora,
                    l.AnoPublicacao

                FROM Autor a
                INNER JOIN LivroAutor la   ON la.AutorId = a.Id
                INNER JOIN Livro l        ON l.Id = la.LivroId
                INNER JOIN LivroAssunto ls ON ls.LivroId = l.Id
                INNER JOIN Assunto s      ON s.Id = ls.AssuntoId;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW IF EXISTS vw_BooksReport");
        }
    }
}
