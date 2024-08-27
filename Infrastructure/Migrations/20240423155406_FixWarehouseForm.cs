using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixWarehouseForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeliveryStageId",
                table: "WarehouseForms",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 23, 22, 54, 6, 218, DateTimeKind.Local).AddTicks(5829), new DateTime(2024, 4, 23, 22, 54, 6, 218, DateTimeKind.Local).AddTicks(5846) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 23, 22, 54, 6, 218, DateTimeKind.Local).AddTicks(5856), new DateTime(2024, 4, 23, 22, 54, 6, 218, DateTimeKind.Local).AddTicks(5857) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 23, 22, 54, 6, 218, DateTimeKind.Local).AddTicks(5857), new DateTime(2024, 4, 23, 22, 54, 6, 218, DateTimeKind.Local).AddTicks(5858) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 23, 22, 54, 6, 218, DateTimeKind.Local).AddTicks(5858), new DateTime(2024, 4, 23, 22, 54, 6, 218, DateTimeKind.Local).AddTicks(5859) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 23, 22, 54, 6, 218, DateTimeKind.Local).AddTicks(5859), new DateTime(2024, 4, 23, 22, 54, 6, 218, DateTimeKind.Local).AddTicks(5860) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 23, 22, 54, 6, 218, DateTimeKind.Local).AddTicks(5860), new DateTime(2024, 4, 23, 22, 54, 6, 218, DateTimeKind.Local).AddTicks(5861) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 23, 22, 54, 6, 218, DateTimeKind.Local).AddTicks(5861), new DateTime(2024, 4, 23, 22, 54, 6, 218, DateTimeKind.Local).AddTicks(5862) });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseForms_DeliveryStageId",
                table: "WarehouseForms",
                column: "DeliveryStageId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseForms_DeliveryStages_DeliveryStageId",
                table: "WarehouseForms",
                column: "DeliveryStageId",
                principalTable: "DeliveryStages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseForms_DeliveryStages_DeliveryStageId",
                table: "WarehouseForms");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseForms_DeliveryStageId",
                table: "WarehouseForms");

            migrationBuilder.DropColumn(
                name: "DeliveryStageId",
                table: "WarehouseForms");

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
    }
}
