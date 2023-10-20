using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sales.Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeCloseDeal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CloseDateTime",
                table: "Deal",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloseDateTime",
                table: "Deal");
        }
    }
}
