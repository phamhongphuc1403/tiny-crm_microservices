using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sales.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class CreateAndSeedLeadsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Leads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Source = table.Column<int>(type: "int", nullable: true),
                    EstimatedRevenue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DisqualificationReason = table.Column<int>(type: "int", nullable: true),
                    DisqualificationDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisqualificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leads", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Leads",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "CustomerId", "Description", "DisqualificationDate", "DisqualificationDescription", "DisqualificationReason", "EstimatedRevenue", "Source", "Status", "Title", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("004a27ca-30b7-4ca0-8178-6dc2b8df5ec6"), null, new DateTime(2023, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("22d84515-5912-489b-b75c-249964f5a278"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 81034m, 2, 2, "Quae et dolorem mollitia repellendus ut.", null, null },
                    { new Guid("f90002e7-cb59-49d0-a7ba-3ffef14e1f9a"), null, new DateTime(2023, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("20419e47-058b-45ae-adc7-a1da89bb050c"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 73890m, 2, 1, "Et voluptatem sunt.", null, null },
                    { new Guid("f90002e7-cb59-49d0-a7ba-3ffef14e1f9b"), null, new DateTime(2023, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("20419e47-058b-45ae-adc7-a1da89bb050c"), "While the Panther received knife and fork with a little scream of laughter. 'Oh, hush!' the Rabbit say, 'A barrowful of WHAT?' thought Alice to herself, (not in a moment to think that proved it at.", new DateTime(2023, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, 88782m, 2, 4, "Officia voluptatem et.", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Leads");
        }
    }
}
