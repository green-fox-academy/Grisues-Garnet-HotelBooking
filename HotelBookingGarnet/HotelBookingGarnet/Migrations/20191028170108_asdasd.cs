using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelBookingGarnet.Migrations
{
    public partial class asdasd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TaxiReservations",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_TaxiReservations_UserId",
                table: "TaxiReservations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaxiReservations_AspNetUsers_UserId",
                table: "TaxiReservations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaxiReservations_AspNetUsers_UserId",
                table: "TaxiReservations");

            migrationBuilder.DropIndex(
                name: "IX_TaxiReservations_UserId",
                table: "TaxiReservations");

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

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TaxiReservations",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

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
    }
}
