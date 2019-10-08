using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelBookingGarnet.Migrations
{
    public partial class a : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_AspNetUsers_UserId1",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_UserId1",
                table: "Reservation");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "49530bcb-eefd-4de5-bbde-2a610e5f36de");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b64ae7e0-125b-4376-920c-c7695d052b7a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "db5ff5ef-844a-4fe8-99bf-9586d2549fad");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Reservation");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Reservation",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e152b217-2479-4a2d-ae94-9237ea6edc59", "dbed9407-d20a-43e6-bd4a-12ed64cd0391", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8865cb3e-3ec6-499b-afd5-c54872b918d9", "64af5231-efe9-4921-8c76-7ae30fb2fbcd", "Guest", "GUEST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "58158196-d730-4792-909f-48dc504920e0", "fe029a67-1035-45fa-820d-e48d19285fd5", "Hotel Manager", "HOTEL MANAGER" });

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_UserId",
                table: "Reservation",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_AspNetUsers_UserId",
                table: "Reservation",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_AspNetUsers_UserId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_UserId",
                table: "Reservation");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "58158196-d730-4792-909f-48dc504920e0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8865cb3e-3ec6-499b-afd5-c54872b918d9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e152b217-2479-4a2d-ae94-9237ea6edc59");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Reservation",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Reservation",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "db5ff5ef-844a-4fe8-99bf-9586d2549fad", "3cebbeea-70d4-4117-a3db-3b8f21eed486", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b64ae7e0-125b-4376-920c-c7695d052b7a", "9c75eefd-6213-4668-a6fb-53b656cd3538", "Guest", "GUEST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "49530bcb-eefd-4de5-bbde-2a610e5f36de", "f90d49ed-90a2-4a9a-8079-3c393b44c60f", "Hotel Manager", "HOTEL MANAGER" });

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_UserId1",
                table: "Reservation",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_AspNetUsers_UserId1",
                table: "Reservation",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
