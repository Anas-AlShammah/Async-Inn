﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Async_Inn.Migrations
{
    /// <inheritdoc />
    public partial class dsfs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Amenities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Layout = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AmenityRoom",
                columns: table => new
                {
                    AmenitiesId = table.Column<int>(type: "int", nullable: false),
                    RoomsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmenityRoom", x => new { x.AmenitiesId, x.RoomsId });
                    table.ForeignKey(
                        name: "FK_AmenityRoom_Amenities_AmenitiesId",
                        column: x => x.AmenitiesId,
                        principalTable: "Amenities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AmenityRoom_Rooms_RoomsId",
                        column: x => x.RoomsId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hotelRooms",
                columns: table => new
                {
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    RoomNumber = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PetFriendly = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hotelRooms", x => new { x.HotelId, x.RoomNumber });
                    table.ForeignKey(
                        name: "FK_hotelRooms_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_hotelRooms_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Amenities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Free Wi-Fi" },
                    { 2, "Swimming Pool" },
                    { 3, "Fitness Center" }
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "City", "Country", "Name", "Phone", "State", "StreetAddress" },
                values: new object[,]
                {
                    { 1, "Seattle", "United States", "Hilton Downtown", 12345678, "Washington", "123 Main St" },
                    { 2, "Seattle", "United States", "Marriott Waterfront", 9876543, "Washington", "456 Pine St" },
                    { 3, "Los Angeles", "United States", "Sheraton Grand", 55555555, "California", "789 Olive St" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Layout", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Standard Room" },
                    { 2, 2, "Deluxe Suite" },
                    { 3, 3, "Penthouse Suite" }
                });

            migrationBuilder.InsertData(
                table: "hotelRooms",
                columns: new[] { "HotelId", "RoomNumber", "PetFriendly", "Rate", "RoomId" },
                values: new object[,]
                {
                    { 1, 101, true, 150.0m, 1 },
                    { 1, 102, false, 180.0m, 2 },
                    { 2, 201, true, 160.0m, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AmenityRoom_RoomsId",
                table: "AmenityRoom",
                column: "RoomsId");

            migrationBuilder.CreateIndex(
                name: "IX_hotelRooms_RoomId",
                table: "hotelRooms",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmenityRoom");

            migrationBuilder.DropTable(
                name: "hotelRooms");

            migrationBuilder.DropTable(
                name: "Amenities");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
