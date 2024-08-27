using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeProductArea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Area",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 20, 11, 29, 55, 654, DateTimeKind.Local).AddTicks(6342), new DateTime(2024, 4, 20, 11, 29, 55, 654, DateTimeKind.Local).AddTicks(6356) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 20, 11, 29, 55, 654, DateTimeKind.Local).AddTicks(6362), new DateTime(2024, 4, 20, 11, 29, 55, 654, DateTimeKind.Local).AddTicks(6363) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 20, 11, 29, 55, 654, DateTimeKind.Local).AddTicks(6364), new DateTime(2024, 4, 20, 11, 29, 55, 654, DateTimeKind.Local).AddTicks(6364) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 20, 11, 29, 55, 654, DateTimeKind.Local).AddTicks(6365), new DateTime(2024, 4, 20, 11, 29, 55, 654, DateTimeKind.Local).AddTicks(6366) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 20, 11, 29, 55, 654, DateTimeKind.Local).AddTicks(6367), new DateTime(2024, 4, 20, 11, 29, 55, 654, DateTimeKind.Local).AddTicks(6367) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 20, 11, 29, 55, 654, DateTimeKind.Local).AddTicks(6368), new DateTime(2024, 4, 20, 11, 29, 55, 654, DateTimeKind.Local).AddTicks(6368) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 20, 11, 29, 55, 654, DateTimeKind.Local).AddTicks(6369), new DateTime(2024, 4, 20, 11, 29, 55, 654, DateTimeKind.Local).AddTicks(6370) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Area",
                table: "Products",
                type: "float",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 20, 9, 12, 32, 300, DateTimeKind.Local).AddTicks(3504), new DateTime(2024, 4, 20, 9, 12, 32, 300, DateTimeKind.Local).AddTicks(3530) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 20, 9, 12, 32, 300, DateTimeKind.Local).AddTicks(3541), new DateTime(2024, 4, 20, 9, 12, 32, 300, DateTimeKind.Local).AddTicks(3542) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 20, 9, 12, 32, 300, DateTimeKind.Local).AddTicks(3543), new DateTime(2024, 4, 20, 9, 12, 32, 300, DateTimeKind.Local).AddTicks(3544) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 20, 9, 12, 32, 300, DateTimeKind.Local).AddTicks(3545), new DateTime(2024, 4, 20, 9, 12, 32, 300, DateTimeKind.Local).AddTicks(3546) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 20, 9, 12, 32, 300, DateTimeKind.Local).AddTicks(3547), new DateTime(2024, 4, 20, 9, 12, 32, 300, DateTimeKind.Local).AddTicks(3561) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 20, 9, 12, 32, 300, DateTimeKind.Local).AddTicks(3563), new DateTime(2024, 4, 20, 9, 12, 32, 300, DateTimeKind.Local).AddTicks(3564) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 20, 9, 12, 32, 300, DateTimeKind.Local).AddTicks(3565), new DateTime(2024, 4, 20, 9, 12, 32, 300, DateTimeKind.Local).AddTicks(3566) });
        }
    }
}
