using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sales.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class CreatetableAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: new Guid("004a27ca-30b7-4ca0-8178-6dc2b8df5ec6"),
                columns: new[] { "Source", "Status" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: new Guid("f90002e7-cb59-49d0-a7ba-3ffef14e1f9a"),
                columns: new[] { "Source", "Status" },
                values: new object[] { 1, 0 });

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: new Guid("f90002e7-cb59-49d0-a7ba-3ffef14e1f9b"),
                columns: new[] { "DisqualificationReason", "Source", "Status" },
                values: new object[] { 0, 1, 3 });

            migrationBuilder.InsertData(
                table: "Leads",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "CustomerId", "Description", "DisqualificationDate", "DisqualificationDescription", "DisqualificationReason", "EstimatedRevenue", "Source", "Status", "Title", "UpdatedBy", "UpdatedDate" },
                values: new object[] { new Guid("004a27ca-30b7-4ca0-8178-6dc2b8df5ec7"), null, new DateTime(2023, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("22d84515-5912-489b-b75c-249964f5a278"), "Alice, 'and why it is to-day! And I declare it's too bad, that it was only sobbing,' she thought, 'it's sure to make out what it was: at first was moderate. But the insolence of his.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 72346m, 1, 2, "Et voluptatem sunt.", null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Email",
                table: "Accounts",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DeleteData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: new Guid("004a27ca-30b7-4ca0-8178-6dc2b8df5ec7"));

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: new Guid("004a27ca-30b7-4ca0-8178-6dc2b8df5ec6"),
                columns: new[] { "Source", "Status" },
                values: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: new Guid("f90002e7-cb59-49d0-a7ba-3ffef14e1f9a"),
                columns: new[] { "Source", "Status" },
                values: new object[] { 2, 1 });

            migrationBuilder.UpdateData(
                table: "Leads",
                keyColumn: "Id",
                keyValue: new Guid("f90002e7-cb59-49d0-a7ba-3ffef14e1f9b"),
                columns: new[] { "DisqualificationReason", "Source", "Status" },
                values: new object[] { 1, 2, 4 });
        }
    }
}
