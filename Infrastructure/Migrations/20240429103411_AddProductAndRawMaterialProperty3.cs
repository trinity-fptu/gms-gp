using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductAndRawMaterialProperty3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StorageDuration",
                table: "RawMaterials");

            migrationBuilder.DropColumn(
                name: "StorageGuide",
                table: "RawMaterials");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 29, 17, 34, 11, 309, DateTimeKind.Local).AddTicks(9324), new DateTime(2024, 4, 29, 17, 34, 11, 309, DateTimeKind.Local).AddTicks(9337) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 29, 17, 34, 11, 309, DateTimeKind.Local).AddTicks(9341), new DateTime(2024, 4, 29, 17, 34, 11, 309, DateTimeKind.Local).AddTicks(9342) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 29, 17, 34, 11, 309, DateTimeKind.Local).AddTicks(9342), new DateTime(2024, 4, 29, 17, 34, 11, 309, DateTimeKind.Local).AddTicks(9343) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 29, 17, 34, 11, 309, DateTimeKind.Local).AddTicks(9343), new DateTime(2024, 4, 29, 17, 34, 11, 309, DateTimeKind.Local).AddTicks(9344) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 29, 17, 34, 11, 309, DateTimeKind.Local).AddTicks(9344), new DateTime(2024, 4, 29, 17, 34, 11, 309, DateTimeKind.Local).AddTicks(9345) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 29, 17, 34, 11, 309, DateTimeKind.Local).AddTicks(9345), new DateTime(2024, 4, 29, 17, 34, 11, 309, DateTimeKind.Local).AddTicks(9346) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 29, 17, 34, 11, 309, DateTimeKind.Local).AddTicks(9346), new DateTime(2024, 4, 29, 17, 34, 11, 309, DateTimeKind.Local).AddTicks(9347) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "StorageDuration",
                table: "RawMaterials",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StorageGuide",
                table: "RawMaterials",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 29, 16, 53, 43, 113, DateTimeKind.Local).AddTicks(8905), new DateTime(2024, 4, 29, 16, 53, 43, 113, DateTimeKind.Local).AddTicks(8916) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 29, 16, 53, 43, 113, DateTimeKind.Local).AddTicks(8921), new DateTime(2024, 4, 29, 16, 53, 43, 113, DateTimeKind.Local).AddTicks(8921) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 29, 16, 53, 43, 113, DateTimeKind.Local).AddTicks(8922), new DateTime(2024, 4, 29, 16, 53, 43, 113, DateTimeKind.Local).AddTicks(8923) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 29, 16, 53, 43, 113, DateTimeKind.Local).AddTicks(8923), new DateTime(2024, 4, 29, 16, 53, 43, 113, DateTimeKind.Local).AddTicks(8924) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 29, 16, 53, 43, 113, DateTimeKind.Local).AddTicks(8924), new DateTime(2024, 4, 29, 16, 53, 43, 113, DateTimeKind.Local).AddTicks(8925) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 29, 16, 53, 43, 113, DateTimeKind.Local).AddTicks(8925), new DateTime(2024, 4, 29, 16, 53, 43, 113, DateTimeKind.Local).AddTicks(8926) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 29, 16, 53, 43, 113, DateTimeKind.Local).AddTicks(8926), new DateTime(2024, 4, 29, 16, 53, 43, 113, DateTimeKind.Local).AddTicks(8926) });
        }
    }
}
