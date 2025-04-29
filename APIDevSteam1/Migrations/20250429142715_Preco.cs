using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIDevSteam1.Migrations
{
    /// <inheritdoc />
    public partial class Preco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PrecoOriginal",
                table: "Jogos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecoOriginal",
                table: "Jogos");
        }
    }
}
