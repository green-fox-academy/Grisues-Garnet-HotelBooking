using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelBookingGarnet.Migrations
{
    public partial class Init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "26d50913-deee-427b-b0df-a791d5d21ef7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a181229b-7b4c-4bcd-856f-ede56e8f8f9b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e6f7e3ce-2673-4c7d-8101-d501bcbb0d81");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c75227de-0deb-4e3f-94e4-4d9abf6d483d", "fa02bc1d-56c9-4b08-bfce-68e0db05ed80", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0c6349ec-06de-439f-9513-d68bdb5242ad", "e143a4cc-3d75-4228-b166-d8ee43733286", "Guest", "GUEST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "94d80831-cdf5-4832-9528-8347b6713448", "385e78ef-5cd4-414b-a13d-72fadc6ffcfd", "Hotel Manager", "HOTEL MANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0c6349ec-06de-439f-9513-d68bdb5242ad");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94d80831-cdf5-4832-9528-8347b6713448");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c75227de-0deb-4e3f-94e4-4d9abf6d483d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e6f7e3ce-2673-4c7d-8101-d501bcbb0d81", "2aa7fb60-d7f0-4ae8-997e-32b4c65622d0", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a181229b-7b4c-4bcd-856f-ede56e8f8f9b", "557b2d93-25e3-4700-a1d1-12cad3d7de83", "Guest", "GUEST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "26d50913-deee-427b-b0df-a791d5d21ef7", "0329e478-2b21-4b92-b486-936a1f825423", "Hotel Manager", "HOTEL MANAGER" });
        }
    }
}
