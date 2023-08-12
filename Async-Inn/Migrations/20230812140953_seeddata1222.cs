using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Async_Inn.Migrations
{
    /// <inheritdoc />
    public partial class seeddata1222 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fb17bbfb-7968-4502-b102-0fcc98be5d8b", "ADMINM", "AQAAAAIAAYagAAAAEC4PGH4UlZtExkA3A7nqEcTx+xarJZOz1efrXTPZj9l48vLEdiofNcPoPxQHotF9pg==", "06b9d06a-deaf-49b5-9617-5319fe2cef03" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7e586c11-44a1-4040-acbb-992769f91f96", "DISTRICTMANAGER@EXAMPLE.COM", "AQAAAAIAAYagAAAAEGTIgoaP/5ifjIceeIkF9kdUPJlwJzh0nic/l44tnIt+mdJXa2yqOrjLd+bDprAOBA==", "3ef8b9ad-bd39-4673-baf4-51b59f83883c" });
        }
    }
}
