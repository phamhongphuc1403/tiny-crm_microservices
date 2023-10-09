using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sales.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class AddforiegnkeyDeadlineProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DealLine_ProductId",
                table: "DealLine",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_DealLine_Product_ProductId",
                table: "DealLine",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DealLine_Product_ProductId",
                table: "DealLine");

            migrationBuilder.DropIndex(
                name: "IX_DealLine_ProductId",
                table: "DealLine");
        }
    }
}
