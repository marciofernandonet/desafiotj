using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookcatalog.Migrations
{
    /// <inheritdoc />
    public partial class AddPkInBookSubject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LivroAssunto",
                table: "LivroAssunto");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "LivroAssunto",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LivroAssunto",
                table: "LivroAssunto",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_LivroAssunto_LivroId_AssuntoId",
                table: "LivroAssunto",
                columns: new[] { "LivroId", "AssuntoId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LivroAssunto",
                table: "LivroAssunto");

            migrationBuilder.DropIndex(
                name: "IX_LivroAssunto_LivroId_AssuntoId",
                table: "LivroAssunto");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "LivroAssunto");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LivroAssunto",
                table: "LivroAssunto",
                columns: new[] { "LivroId", "AssuntoId" });
        }
    }
}
