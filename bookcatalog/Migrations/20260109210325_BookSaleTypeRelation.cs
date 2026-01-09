using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookcatalog.Migrations
{
    /// <inheritdoc />
    public partial class BookSaleTypeRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LivroId",
                table: "TipoVenda",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TipoVenda_LivroId",
                table: "TipoVenda",
                column: "LivroId");

            migrationBuilder.AddForeignKey(
                name: "FK_TipoVenda_Livro_LivroId",
                table: "TipoVenda",
                column: "LivroId",
                principalTable: "Livro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TipoVenda_Livro_LivroId",
                table: "TipoVenda");

            migrationBuilder.DropIndex(
                name: "IX_TipoVenda_LivroId",
                table: "TipoVenda");

            migrationBuilder.DropColumn(
                name: "LivroId",
                table: "TipoVenda");
        }
    }
}
