using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "StorageDuration",
                table: "RawMaterials",
                type: "float",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 20, 17, 15, 40, 90, DateTimeKind.Local).AddTicks(4607), new DateTime(2024, 4, 20, 17, 15, 40, 90, DateTimeKind.Local).AddTicks(4626) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 20, 17, 15, 40, 90, DateTimeKind.Local).AddTicks(4632), new DateTime(2024, 4, 20, 17, 15, 40, 90, DateTimeKind.Local).AddTicks(4632) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 20, 17, 15, 40, 90, DateTimeKind.Local).AddTicks(4634), new DateTime(2024, 4, 20, 17, 15, 40, 90, DateTimeKind.Local).AddTicks(4635) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 20, 17, 15, 40, 90, DateTimeKind.Local).AddTicks(4636), new DateTime(2024, 4, 20, 17, 15, 40, 90, DateTimeKind.Local).AddTicks(4636) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 20, 17, 15, 40, 90, DateTimeKind.Local).AddTicks(4637), new DateTime(2024, 4, 20, 17, 15, 40, 90, DateTimeKind.Local).AddTicks(4638) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 20, 17, 15, 40, 90, DateTimeKind.Local).AddTicks(4639), new DateTime(2024, 4, 20, 17, 15, 40, 90, DateTimeKind.Local).AddTicks(4640) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 20, 17, 15, 40, 90, DateTimeKind.Local).AddTicks(4641), new DateTime(2024, 4, 20, 17, 15, 40, 90, DateTimeKind.Local).AddTicks(4642) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StorageDuration",
                table: "RawMaterials");

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
    }
}
