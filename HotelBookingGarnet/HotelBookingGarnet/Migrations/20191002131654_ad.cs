using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelBookingGarnet.Migrations
{
    public partial class ad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "157206cf-2049-43d2-a82b-9801c168c7ae", "eaeba1a7-beec-45dd-9d02-882fcf306cdc" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "1f1b884d-b399-4c09-8378-d3d07816600d", "1d1b6d8d-820a-472a-8dff-ddbbbaac4f65" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "96164cb2-dba6-438d-9d4c-daa269469b3c", "af72b47c-5ed2-40f9-82ba-b0cabd18dd66" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2fc74598-dab5-4495-9221-788fd0549d2d", "5093c5e8-afe0-4e35-95e6-9f940f056a54", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7700db76-9248-4743-98b0-3c1741ead9c2", "fe648e44-44cd-4a56-bec9-e21c366b62e2", "Guest", "GUEST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a422ac0d-1cef-4afc-9d2e-9d441af8c6f9", "d159e6bf-4216-4155-84e3-88ad85c6c905", "Hotel Manager", "HOTEL MANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "2fc74598-dab5-4495-9221-788fd0549d2d", "5093c5e8-afe0-4e35-95e6-9f940f056a54" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "7700db76-9248-4743-98b0-3c1741ead9c2", "fe648e44-44cd-4a56-bec9-e21c366b62e2" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "a422ac0d-1cef-4afc-9d2e-9d441af8c6f9", "d159e6bf-4216-4155-84e3-88ad85c6c905" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1f1b884d-b399-4c09-8378-d3d07816600d", "1d1b6d8d-820a-472a-8dff-ddbbbaac4f65", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "157206cf-2049-43d2-a82b-9801c168c7ae", "eaeba1a7-beec-45dd-9d02-882fcf306cdc", "Guest", "GUEST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "96164cb2-dba6-438d-9d4c-daa269469b3c", "af72b47c-5ed2-40f9-82ba-b0cabd18dd66", "Hotel Manager", "HOTEL MANAGER" });
        }
    }
}
