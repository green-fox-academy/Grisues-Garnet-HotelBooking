using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelBookingGarnet.Migrations
{
    public partial class c : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ed7def2-29ae-4d35-9c07-54ddd5b16a26");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "49d77a1e-00b0-4bb9-a5fc-7ccab79e1a8c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fb8de299-8295-4ffa-9eee-402a450a9a4e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f03831d4-4ef2-454d-bf06-2bdb3500bbc6", "766cec39-eb1b-4de6-95ac-1424692a3c47", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "12e15292-3b86-4bed-849a-b5c921ccbe1a", "07664828-b25b-431b-a5e5-b7e7234c46f9", "Guest", "GUEST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fe621976-a149-4dd2-81ee-44ac34fbfa3c", "61fc6620-784c-4c78-99e2-feab85310bb0", "Hotel Manager", "HOTEL MANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "12e15292-3b86-4bed-849a-b5c921ccbe1a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f03831d4-4ef2-454d-bf06-2bdb3500bbc6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fe621976-a149-4dd2-81ee-44ac34fbfa3c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1ed7def2-29ae-4d35-9c07-54ddd5b16a26", "023a335e-0c13-44bb-9558-2b2790b63da6", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fb8de299-8295-4ffa-9eee-402a450a9a4e", "8da5889c-d012-453d-a952-71787ceb28af", "Guest", "GUEST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "49d77a1e-00b0-4bb9-a5fc-7ccab79e1a8c", "8e2f7473-8fa3-4ae8-bff0-deeef22266f4", "Hotel Manager", "HOTEL MANAGER" });
        }
    }
}
