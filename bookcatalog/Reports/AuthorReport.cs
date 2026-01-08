using bookcatalog.Dtos.Report;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace bookcatalog.Reports;

public class AuthorReport : IDocument
{
    private readonly IEnumerable<ReportView> _data;

    public AuthorReport(IEnumerable<ReportView> data)
    {
        _data = data;
    }

    public DocumentMetadata GetMetadata() => new DocumentMetadata
    {
        Title = "Relatório por Autor"
    };

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(30);
            page.DefaultTextStyle(x => x.FontSize(11));

            page.Header().Element(ComposeHeader);
            page.Content().Element(ComposeContent);
        });
    }

    void ComposeHeader(IContainer container)
    {
        container.PaddingBottom(10).Row(row =>
        {
            row.RelativeItem().Text("Relatório por Autor")
                .FontSize(18)
                .Bold();

            row.ConstantItem(120)
                .AlignRight()
                .Text(DateTime.Now.ToString("dd/MM/yyyy"));
        });
    }

    // 🔹 Conteúdo principal
    void ComposeContent(IContainer container)
    {
        var autores = _data
            .GroupBy(x => x.AutorNome)
            .OrderBy(g => g.Key);

        container.Column(column =>
        {
            foreach (var autor in autores)
            {
                column.Item().PaddingTop(10).Text(autor.Key)
                    .FontSize(14)
                    .Bold()
                    .FontColor(Colors.Blue.Darken2);

                var assuntos = autor
                    .GroupBy(x => x.Assunto)
                    .OrderBy(g => g.Key);

                foreach (var assunto in assuntos)
                {
                    column.Item().PaddingLeft(15).PaddingTop(5)
                        .Text(assunto.Key)
                        .FontSize(12)
                        .Italic()
                        .FontColor(Colors.Grey.Darken2);

                    column.Item().PaddingLeft(25).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(4); // Título
                            columns.RelativeColumn(2); // Editora
                            columns.ConstantColumn(60); // Ano
                        });

                        // Cabeçalho da tabela
                        table.Header(header =>
                        {
                            header.Cell().Text("Título").Bold();
                            header.Cell().Text("Editora").Bold();
                            header.Cell().AlignCenter().Text("Ano").Bold();
                        });

                        foreach (var livro in assunto)
                        {
                            table.Cell().Text(livro.Titulo);
                            table.Cell().Text(livro.Editora);
                            table.Cell().AlignCenter().Text(livro.AnoPublicacao.ToString());
                        }
                    });
                }
            }
        });
    }
}