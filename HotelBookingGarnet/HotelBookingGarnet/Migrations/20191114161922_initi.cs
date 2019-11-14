using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelBookingGarnet.Migrations
{
    public partial class initi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d9bab83-838a-4e05-b8cc-7b48a720da11");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8bcdbb61-4fa1-473a-974a-1b58465946ea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aae671b1-85ea-44e1-9c3a-ec52ef096ad9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a9bdb2eb-ec66-4733-b121-50de5a6997b9", "f0d89ebc-b976-4eb7-acac-e1afe096c3f8", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9a9589c3-33bd-41ae-84b3-ca9a7b8b6431", "b06766f2-5d43-44af-a7aa-e83327125f00", "Guest", "GUEST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2ab37820-c5bb-4f0d-a295-92228b0f72c4", "17950a62-3eb0-4d08-a9b7-4df36e32b3b0", "Hotel Manager", "HOTEL MANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2ab37820-c5bb-4f0d-a295-92228b0f72c4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9a9589c3-33bd-41ae-84b3-ca9a7b8b6431");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a9bdb2eb-ec66-4733-b121-50de5a6997b9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8bcdbb61-4fa1-473a-974a-1b58465946ea", "6113588b-0571-4f09-b7ba-90f8b5bf61cd", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2d9bab83-838a-4e05-b8cc-7b48a720da11", "7388ba9a-f588-4f22-bed2-be61cab64bad", "Guest", "GUEST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "aae671b1-85ea-44e1-9c3a-ec52ef096ad9", "736203ff-4cde-41c4-a5fc-01d3b32c748f", "Hotel Manager", "HOTEL MANAGER" });
        }
    }
}
