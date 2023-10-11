using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sales.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class ConfigrelationshipDealandLead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Deal_LeadId",
                table: "Deal");

            migrationBuilder.CreateIndex(
                name: "IX_Deal_LeadId",
                table: "Deal",
                column: "LeadId",
                unique: true,
                filter: "[LeadId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Deal_LeadId",
                table: "Deal");

            migrationBuilder.CreateIndex(
                name: "IX_Deal_LeadId",
                table: "Deal",
                column: "LeadId");
        }
    }
}
