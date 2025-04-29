using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIDevSteam1.Migrations
{
    /// <inheritdoc />
    public partial class LimiteUso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LimiteUso",
                table: "Cupons",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LimiteUso",
                table: "Cupons");
        }
    }
}
