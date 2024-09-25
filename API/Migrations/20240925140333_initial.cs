using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDttm = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedDttm", "DateOfBirth", "Email", "FirstName", "LastName", "MiddleName", "PhoneNumber", "Title" },
                values: new object[,]
                {
                    { 1L, new DateTime(2024, 9, 25, 15, 3, 33, 267, DateTimeKind.Local).AddTicks(6155), new DateTime(1970, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Joseph", "Joestar", null, null, "Mr" },
                    { 2L, new DateTime(2024, 9, 25, 15, 3, 33, 267, DateTimeKind.Local).AddTicks(6205), new DateTime(1989, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Samus", "Aran", null, null, "Miss" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
