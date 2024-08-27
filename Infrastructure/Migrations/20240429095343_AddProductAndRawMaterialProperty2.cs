using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductAndRawMaterialProperty2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChemicalClarity",
                table: "RawMaterials");

            migrationBuilder.AddColumn<string>(
                name: "MainIngredient",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainIngredient",
                table: "RawMaterials");

            migrationBuilder.AddColumn<double>(
                name: "ChemicalClarity",
                table: "RawMaterials",
                type: "float",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 29, 16, 36, 58, 963, DateTimeKind.Local).AddTicks(980), new DateTime(2024, 4, 29, 16, 36, 58, 963, DateTimeKind.Local).AddTicks(994) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 29, 16, 36, 58, 963, DateTimeKind.Local).AddTicks(1002), new DateTime(2024, 4, 29, 16, 36, 58, 963, DateTimeKind.Local).AddTicks(1002) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 29, 16, 36, 58, 963, DateTimeKind.Local).AddTicks(1003), new DateTime(2024, 4, 29, 16, 36, 58, 963, DateTimeKind.Local).AddTicks(1003) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 29, 16, 36, 58, 963, DateTimeKind.Local).AddTicks(1004), new DateTime(2024, 4, 29, 16, 36, 58, 963, DateTimeKind.Local).AddTicks(1005) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 29, 16, 36, 58, 963, DateTimeKind.Local).AddTicks(1005), new DateTime(2024, 4, 29, 16, 36, 58, 963, DateTimeKind.Local).AddTicks(1006) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 29, 16, 36, 58, 963, DateTimeKind.Local).AddTicks(1006), new DateTime(2024, 4, 29, 16, 36, 58, 963, DateTimeKind.Local).AddTicks(1007) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 29, 16, 36, 58, 963, DateTimeKind.Local).AddTicks(1007), new DateTime(2024, 4, 29, 16, 36, 58, 963, DateTimeKind.Local).AddTicks(1008) });
        }
    }
}
