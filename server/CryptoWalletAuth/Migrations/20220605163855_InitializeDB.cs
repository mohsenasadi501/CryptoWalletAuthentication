using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoWalletAuth.Migrations
{
    public partial class InitializeDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nonce = table.Column<long>(type: "bigint", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshTokenExpiration = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "Name", "UserId" },
                values: new object[] { 1, "Administrator", 1 });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "Name", "UserId" },
                values: new object[] { 2, "View", 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Nonce", "PublicAddress", "RefreshToken", "RefreshTokenExpiration" },
                values: new object[] { 1, 123456987654L, "0x174A639d18a2EE6590ed1A201F8CCC76A52FFB13", "", new DateTime(2022, 6, 5, 21, 8, 55, 158, DateTimeKind.Local).AddTicks(15) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
