using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductAndRawMaterialProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ChemicalClarity",
                table: "RawMaterials",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Diameter",
                table: "RawMaterials",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DiameterUnit",
                table: "RawMaterials",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "RawMaterials",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Length",
                table: "RawMaterials",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MaxToleranceWeight",
                table: "RawMaterials",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MinToleranceWeight",
                table: "RawMaterials",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SizeUnitEnum",
                table: "RawMaterials",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ThreadRatio",
                table: "RawMaterials",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "RawMaterials",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WeightUnit",
                table: "RawMaterials",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Width",
                table: "RawMaterials",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductSize",
                table: "Products",
                type: "int",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChemicalClarity",
                table: "RawMaterials");

            migrationBuilder.DropColumn(
                name: "Diameter",
                table: "RawMaterials");

            migrationBuilder.DropColumn(
                name: "DiameterUnit",
                table: "RawMaterials");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "RawMaterials");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "RawMaterials");

            migrationBuilder.DropColumn(
                name: "MaxToleranceWeight",
                table: "RawMaterials");

            migrationBuilder.DropColumn(
                name: "MinToleranceWeight",
                table: "RawMaterials");

            migrationBuilder.DropColumn(
                name: "SizeUnitEnum",
                table: "RawMaterials");

            migrationBuilder.DropColumn(
                name: "ThreadRatio",
                table: "RawMaterials");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "RawMaterials");

            migrationBuilder.DropColumn(
                name: "WeightUnit",
                table: "RawMaterials");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "RawMaterials");

            migrationBuilder.DropColumn(
                name: "ProductSize",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 24, 21, 26, 2, 171, DateTimeKind.Local).AddTicks(4762), new DateTime(2024, 4, 24, 21, 26, 2, 171, DateTimeKind.Local).AddTicks(4777) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 24, 21, 26, 2, 171, DateTimeKind.Local).AddTicks(4785), new DateTime(2024, 4, 24, 21, 26, 2, 171, DateTimeKind.Local).AddTicks(4786) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 24, 21, 26, 2, 171, DateTimeKind.Local).AddTicks(4787), new DateTime(2024, 4, 24, 21, 26, 2, 171, DateTimeKind.Local).AddTicks(4788) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 24, 21, 26, 2, 171, DateTimeKind.Local).AddTicks(4789), new DateTime(2024, 4, 24, 21, 26, 2, 171, DateTimeKind.Local).AddTicks(4790) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 24, 21, 26, 2, 171, DateTimeKind.Local).AddTicks(4791), new DateTime(2024, 4, 24, 21, 26, 2, 171, DateTimeKind.Local).AddTicks(4792) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 24, 21, 26, 2, 171, DateTimeKind.Local).AddTicks(4793), new DateTime(2024, 4, 24, 21, 26, 2, 171, DateTimeKind.Local).AddTicks(4794) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 24, 21, 26, 2, 171, DateTimeKind.Local).AddTicks(4795), new DateTime(2024, 4, 24, 21, 26, 2, 171, DateTimeKind.Local).AddTicks(4796) });
        }
    }
}
