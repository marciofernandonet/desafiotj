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
        public DbSet<SaleType> TipoVenda { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Id)
                    .ValueGeneratedOnAdd();

                entity.HasOne(x => x.Livro)
                    .WithMany(x => x.LivroAutor)
                    .HasForeignKey(x => x.LivroId);

                entity.HasOne(x => x.Autor)
                    .WithMany(x => x.LivroAutor)
                    .HasForeignKey(x => x.AutorId);

                entity.HasIndex(x => new { x.LivroId, x.AutorId })
                    .IsUnique();
            });

            modelBuilder.Entity<BookSubject>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Id)
                    .ValueGeneratedOnAdd();

                entity.HasOne(x => x.Livro)
                    .WithMany(x => x.LivroAssunto)
                    .HasForeignKey(x => x.LivroId);

                entity.HasOne(x => x.Assunto)
                    .WithMany(x => x.LivroAssunto)
                    .HasForeignKey(x => x.AssuntoId);

                entity.HasIndex(x => new { x.LivroId, x.AssuntoId })
                    .IsUnique();
            });

            modelBuilder.Entity<ReportView>(e =>
            {
                e.HasNoKey();
                e.ToView("vw_RelatorioAutorAssuntoLivro");
            });
        }
    }
}