using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIDevSteam1.Migrations
{
    /// <inheritdoc />
    public partial class CupomCarrinho : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CuponsCarrinhos",
                columns: table => new
                {
                    CupomCarrinhoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CupomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarrinhoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuponsCarrinhos", x => x.CupomCarrinhoId);
                    table.ForeignKey(
                        name: "FK_CuponsCarrinhos_Carinhos_CarrinhoId",
                        column: x => x.CarrinhoId,
                        principalTable: "Carinhos",
                        principalColumn: "CarrinhoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CuponsCarrinhos_Cupons_CupomId",
                        column: x => x.CupomId,
                        principalTable: "Cupons",
                        principalColumn: "CupomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CuponsCarrinhos_CarrinhoId",
                table: "CuponsCarrinhos",
                column: "CarrinhoId");

            migrationBuilder.CreateIndex(
                name: "IX_CuponsCarrinhos_CupomId",
                table: "CuponsCarrinhos",
                column: "CupomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CuponsCarrinhos");
        }
    }
}
