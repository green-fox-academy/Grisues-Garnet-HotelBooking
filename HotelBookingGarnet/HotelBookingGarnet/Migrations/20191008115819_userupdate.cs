using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelBookingGarnet.Migrations
{
    public partial class userupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Hotels_HotelId1",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_HotelId1",
                table: "Reservation");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "25ff596c-e2e7-4f64-a219-cc3f07ae648b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "307f1d8e-573e-480a-aaa9-54863d0f2961");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "31d780f7-2476-4415-b367-6b08e8707e6b");

            migrationBuilder.DropColumn(
                name: "HotelId1",
                table: "Reservation");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Reservation",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "HotelId",
                table: "Reservation",
                nullable: false,
                oldClrType: typeof(int));

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
                name: "IX_Reservation_HotelId",
                table: "Reservation",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Hotels_HotelId",
                table: "Reservation",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "HotelId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Hotels_HotelId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_HotelId",
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

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Reservation",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "Reservation",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "HotelId1",
                table: "Reservation",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "25ff596c-e2e7-4f64-a219-cc3f07ae648b", "74a9486e-4c5e-4e34-8769-799ea0f73bd6", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "31d780f7-2476-4415-b367-6b08e8707e6b", "ea2d0262-0029-4fdb-818c-94d3c557ac9c", "Guest", "GUEST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "307f1d8e-573e-480a-aaa9-54863d0f2961", "2d05d85c-b6ac-4b53-952b-7225b09599f9", "Hotel Manager", "HOTEL MANAGER" });

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_HotelId1",
                table: "Reservation",
                column: "HotelId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Hotels_HotelId1",
                table: "Reservation",
                column: "HotelId1",
                principalTable: "Hotels",
                principalColumn: "HotelId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
