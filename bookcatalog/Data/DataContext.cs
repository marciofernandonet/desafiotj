using bookcatalog.Dtos.Report;
using bookcatalog.Models;
using Microsoft.EntityFrameworkCore;

namespace bookcatalog.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Book> Livro { get; set; }
        public DbSet<Author> Autor { get; set; }
        public DbSet<Subject> Assunto { get; set; }
        public DbSet<BookAuthor> LivroAutor { get; set; }
        public DbSet<BookSubject> LivroAssunto { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>()
                .HasKey(ba => new { ba.LivroId, ba.AutorId });

            modelBuilder.Entity<BookSubject>()
                .HasKey(bs => new { bs.LivroId, bs.AssuntoId });

            modelBuilder.Entity<ReportView>(e =>
            {
                e.HasNoKey();
                e.ToView("vw_RelatorioAutorAssuntoLivro");
            });
        }
    }
}