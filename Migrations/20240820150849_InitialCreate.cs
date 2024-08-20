using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CouponApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Percent = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "Id", "CreatedDate", "IsActive", "Name", "Percent", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("174f660f-723e-40a2-8bd1-25778c4884c7"), new DateTime(2024, 8, 20, 18, 8, 45, 872, DateTimeKind.Local).AddTicks(7139), true, "10OFF", 10, new DateTime(2024, 8, 20, 18, 8, 45, 872, DateTimeKind.Local).AddTicks(7153) },
                    { new Guid("98bc6939-28b0-41a1-b86f-87677e9ba2f2"), new DateTime(2024, 8, 20, 18, 8, 45, 872, DateTimeKind.Local).AddTicks(7158), true, "20OFF", 20, new DateTime(2024, 8, 20, 18, 8, 45, 872, DateTimeKind.Local).AddTicks(7160) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coupons");
        }
    }
}
