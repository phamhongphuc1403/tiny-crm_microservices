using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sales.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTypeDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: new Guid("004a27ca-30b7-4ca0-8178-6dc2b8df5ec6"));

            migrationBuilder.DeleteData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: new Guid("004a27ca-30b7-4ca0-8178-6dc2b8df5ec7"));

            migrationBuilder.DeleteData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: new Guid("f90002e7-cb59-49d0-a7ba-3ffef14e1f9a"));

            migrationBuilder.DeleteData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: new Guid("f90002e7-cb59-49d0-a7ba-3ffef14e1f9b"));

            migrationBuilder.CreateIndex(
                name: "IX_Leads_CustomerId",
                table: "Leads",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leads_Accounts_CustomerId",
                table: "Leads",
                column: "CustomerId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leads_Accounts_CustomerId",
                table: "Leads");

            migrationBuilder.DropIndex(
                name: "IX_Leads_CustomerId",
                table: "Leads");

            migrationBuilder.InsertData(
                table: "Leads",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "CustomerId", "Description", "DisqualificationDate", "DisqualificationDescription", "DisqualificationReason", "EstimatedRevenue", "Source", "Status", "Title", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("004a27ca-30b7-4ca0-8178-6dc2b8df5ec6"), null, new DateTime(2023, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("22d84515-5912-489b-b75c-249964f5a278"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 81034m, 1, 1, "Quae et dolorem mollitia repellendus ut.", null, null },
                    { new Guid("004a27ca-30b7-4ca0-8178-6dc2b8df5ec7"), null, new DateTime(2023, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("22d84515-5912-489b-b75c-249964f5a278"), "Alice, 'and why it is to-day! And I declare it's too bad, that it was only sobbing,' she thought, 'it's sure to make out what it was: at first was moderate. But the insolence of his.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 72346m, 1, 2, "Et voluptatem sunt.", null, null },
                    { new Guid("f90002e7-cb59-49d0-a7ba-3ffef14e1f9a"), null, new DateTime(2023, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("20419e47-058b-45ae-adc7-a1da89bb050c"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 73890m, 1, 0, "Et voluptatem sunt.", null, null },
                    { new Guid("f90002e7-cb59-49d0-a7ba-3ffef14e1f9b"), null, new DateTime(2023, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("20419e47-058b-45ae-adc7-a1da89bb050c"), "While the Panther received knife and fork with a little scream of laughter. 'Oh, hush!' the Rabbit say, 'A barrowful of WHAT?' thought Alice to herself, (not in a moment to think that proved it at.", new DateTime(2023, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, 88782m, 1, 3, "Officia voluptatem et.", null, null }
                });
        }
    }
}
