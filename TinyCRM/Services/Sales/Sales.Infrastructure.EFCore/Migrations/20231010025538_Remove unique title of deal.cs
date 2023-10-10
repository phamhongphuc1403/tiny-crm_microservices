using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sales.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class Removeuniquetitleofdeal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Deal_Title",
                table: "Deal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Deal_Title",
                table: "Deal",
                column: "Title",
                unique: true);
        }
    }
}
