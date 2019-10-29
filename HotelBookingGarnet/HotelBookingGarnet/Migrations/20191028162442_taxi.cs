using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelBookingGarnet.Migrations
{
    public partial class taxi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b96d275-4d4d-48b3-84e8-b7d0200b0428");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8a211b0d-5801-4250-b6f8-e7d05217ebe2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9e9cbf0-77bc-44e3-b660-dbb06e2203bd");

            migrationBuilder.CreateTable(
                name: "TaxiReservations",
                columns: table => new
                {
                    TaxiReservationId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TaxiReservationStart = table.Column<DateTime>(nullable: false),
                    TaxiReservationEnd = table.Column<DateTime>(nullable: false),
                    NumberOfGuest = table.Column<int>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxiReservations", x => x.TaxiReservationId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "359f779d-3142-4ead-b524-1a7d2169a2f5", "75fce94e-6c58-40ac-9b93-4196e5bb576c", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5557edde-bb64-41b7-af7e-b62dc51335c8", "d65fa89e-fa32-47d5-a364-9ffd81171aa4", "Guest", "GUEST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "507201ea-49ed-46dd-8d2a-9c9c9ab61b07", "9c577fb3-1c83-4447-8711-4672b0e681a2", "Hotel Manager", "HOTEL MANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaxiReservations");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "359f779d-3142-4ead-b524-1a7d2169a2f5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "507201ea-49ed-46dd-8d2a-9c9c9ab61b07");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5557edde-bb64-41b7-af7e-b62dc51335c8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8a211b0d-5801-4250-b6f8-e7d05217ebe2", "f7bf6798-e1db-4cb7-b534-aa291bf0934c", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4b96d275-4d4d-48b3-84e8-b7d0200b0428", "51b8cba6-0df3-4f16-acf6-690bbce9d0d5", "Guest", "GUEST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b9e9cbf0-77bc-44e3-b660-dbb06e2203bd", "5216424a-7ff0-40e0-b91d-796bfa1c9c2d", "Hotel Manager", "HOTEL MANAGER" });
        }
    }
}
