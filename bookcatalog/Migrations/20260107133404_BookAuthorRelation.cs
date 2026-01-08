using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookcatalog.Migrations
{
    /// <inheritdoc />
    public partial class BookAuthorRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LivroAutor_Autor_AuthorId",
                table: "LivroAutor");

            migrationBuilder.DropForeignKey(
                name: "FK_LivroAutor_Livro_BookId",
                table: "LivroAutor");

            migrationBuilder.DropIndex(
                name: "IX_LivroAutor_AuthorId",
                table: "LivroAutor");

            migrationBuilder.DropIndex(
                name: "IX_LivroAutor_BookId",
                table: "LivroAutor");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "LivroAutor");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "LivroAutor");

            migrationBuilder.CreateIndex(
                name: "IX_LivroAutor_AutorId",
                table: "LivroAutor",
                column: "AutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_LivroAutor_Autor_AutorId",
                table: "LivroAutor",
                column: "AutorId",
                principalTable: "Autor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LivroAutor_Livro_LivroId",
                table: "LivroAutor",
                column: "LivroId",
                principalTable: "Livro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LivroAutor_Autor_AutorId",
                table: "LivroAutor");

            migrationBuilder.DropForeignKey(
                name: "FK_LivroAutor_Livro_LivroId",
                table: "LivroAutor");

            migrationBuilder.DropIndex(
                name: "IX_LivroAutor_AutorId",
                table: "LivroAutor");

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "LivroAutor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "LivroAutor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LivroAutor_AuthorId",
                table: "LivroAutor",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_LivroAutor_BookId",
                table: "LivroAutor",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_LivroAutor_Autor_AuthorId",
                table: "LivroAutor",
                column: "AuthorId",
                principalTable: "Autor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LivroAutor_Livro_BookId",
                table: "LivroAutor",
                column: "BookId",
                principalTable: "Livro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
