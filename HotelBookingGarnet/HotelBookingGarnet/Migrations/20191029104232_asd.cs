using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelBookingGarnet.Migrations
{
    public partial class asd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "TaxiReservationEnd",
                table: "TaxiReservations");

            migrationBuilder.AddColumn<string>(
                name: "EndLocal",
                table: "TaxiReservations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartLocal",
                table: "TaxiReservations",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8cbe058f-6306-4b25-be45-4f039d965cba", "ae1f4551-676b-44c0-9a4a-b2479da9b2fc", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5547da10-aaa3-4671-866e-d6bac2a2026f", "34ac4706-3d83-4e65-9460-f8db73201e1e", "Guest", "GUEST" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e1db4793-9a5c-469a-a569-1e598e66a8bd", "9e8afb8c-01be-4194-ba20-a02c000a8332", "Hotel Manager", "HOTEL MANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5547da10-aaa3-4671-866e-d6bac2a2026f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8cbe058f-6306-4b25-be45-4f039d965cba");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e1db4793-9a5c-469a-a569-1e598e66a8bd");

            migrationBuilder.DropColumn(
                name: "EndLocal",
                table: "TaxiReservations");

            migrationBuilder.DropColumn(
                name: "StartLocal",
                table: "TaxiReservations");

            migrationBuilder.AddColumn<DateTime>(
                name: "TaxiReservationEnd",
                table: "TaxiReservations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
    }
}
