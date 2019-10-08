using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelBookingGarnet.Migrations
{
    public partial class date : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3b02a9b9-0773-4512-85df-1454df966ea9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3bd060b8-66c7-4f0d-9ab7-5d9c94761c5f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "95bac20d-e379-4eb6-ae99-4f829c71078e");

            migrationBuilder.DropColumn(
                name: "ReservationEnd",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "ReservationStart",
                table: "Reservation");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "af58dc39-affd-494b-8dd6-499dc83e20e4", "c67f64ec-d3ee-4e83-a36d-166770db4111", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d332223f-3580-49d7-a40a-e45ea415ec6c", "b0b6244c-09a5-4e6d-b44a-5e259b9e3b33", "Guest", "GUEST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f2906ac9-5611-4e8d-bc27-470de5836c2c", "9c73e18f-b348-4760-8d17-5e78adce7be6", "Hotel Manager", "HOTEL MANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af58dc39-affd-494b-8dd6-499dc83e20e4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d332223f-3580-49d7-a40a-e45ea415ec6c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f2906ac9-5611-4e8d-bc27-470de5836c2c");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReservationEnd",
                table: "Reservation",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ReservationStart",
                table: "Reservation",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3b02a9b9-0773-4512-85df-1454df966ea9", "6f74388c-eaed-4903-b99a-11085ec8e327", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3bd060b8-66c7-4f0d-9ab7-5d9c94761c5f", "c4c6bae2-131f-4d28-a8ee-3805e19af2d4", "Guest", "GUEST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "95bac20d-e379-4eb6-ae99-4f829c71078e", "77740ab9-0342-4eee-af15-f4a3d9cf1c9c", "Hotel Manager", "HOTEL MANAGER" });
        }
    }
}
