using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookcatalog.Migrations
{
    /// <inheritdoc />
    public partial class AddPkInBookAuthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LivroAutor",
                table: "LivroAutor");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "LivroAutor",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LivroAutor",
                table: "LivroAutor",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_LivroAutor_LivroId_AutorId",
                table: "LivroAutor",
                columns: new[] { "LivroId", "AutorId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LivroAutor",
                table: "LivroAutor");

            migrationBuilder.DropIndex(
                name: "IX_LivroAutor_LivroId_AutorId",
                table: "LivroAutor");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "LivroAutor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LivroAutor",
                table: "LivroAutor",
                columns: new[] { "LivroId", "AutorId" });
        }
    }
}
