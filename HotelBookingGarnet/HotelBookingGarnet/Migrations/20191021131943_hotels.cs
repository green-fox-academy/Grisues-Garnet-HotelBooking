using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelBookingGarnet.Migrations
{
    public partial class hotels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "578e228f-c599-497f-af9e-301d6e67ec57");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f479210-fbfa-473f-93b9-8859f1c29889");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "98798a39-a81b-49a5-bcde-6708351d0b8b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d44e0a55-f2e4-4802-ab6e-7a5174cbc702", "8f0e7ed7-ab7f-476a-a426-8528f3ba5686", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "65c51e96-e5fb-4172-afcc-dc341efdad92", "dfa1f350-9c3d-405c-9160-1685edf4bb84", "Guest", "GUEST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5fa440ad-4a50-4ea0-ab0a-227531bff536", "00a8a1c6-268c-4ba0-b762-9fbaf3f8a175", "Hotel Manager", "HOTEL MANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5fa440ad-4a50-4ea0-ab0a-227531bff536");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "65c51e96-e5fb-4172-afcc-dc341efdad92");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d44e0a55-f2e4-4802-ab6e-7a5174cbc702");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "98798a39-a81b-49a5-bcde-6708351d0b8b", "d6e90bb0-7e7e-4c37-9e26-a478b90f60e9", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6f479210-fbfa-473f-93b9-8859f1c29889", "e44ef157-c482-4f2a-912b-c14434729abb", "Guest", "GUEST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "578e228f-c599-497f-af9e-301d6e67ec57", "ff648579-65de-4d08-92ec-68a3e8b54a70", "Hotel Manager", "HOTEL MANAGER" });
        }
    }
}
