using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelBookingGarnet.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1c3e6fb7-8c6a-4680-81b6-86d4e9c2a196");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5bfde583-82bc-4708-b18b-21f2aba2063c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7898b56e-331b-4ecf-9f04-e0cb1a345ab2");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { "1c3e6fb7-8c6a-4680-81b6-86d4e9c2a196", "626ec225-b5de-4672-940d-1d457d00b7d7", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5bfde583-82bc-4708-b18b-21f2aba2063c", "23ba3ed1-38fe-4f9a-b3ca-566755db1cc4", "Guest", "GUEST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7898b56e-331b-4ecf-9f04-e0cb1a345ab2", "3593009e-eaa1-4e99-afa5-70469aca6e2f", "Hotel Manager", "HOTEL MANAGER" });
        }
    }
}
