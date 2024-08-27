using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixdb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PlanEndDate",
                table: "PurchasingTasks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PlanStartDate",
                table: "PurchasingTasks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 24, 21, 11, 5, 651, DateTimeKind.Local).AddTicks(8057), new DateTime(2024, 4, 24, 21, 11, 5, 651, DateTimeKind.Local).AddTicks(8081) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 24, 21, 11, 5, 651, DateTimeKind.Local).AddTicks(8093), new DateTime(2024, 4, 24, 21, 11, 5, 651, DateTimeKind.Local).AddTicks(8094) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 24, 21, 11, 5, 651, DateTimeKind.Local).AddTicks(8097), new DateTime(2024, 4, 24, 21, 11, 5, 651, DateTimeKind.Local).AddTicks(8098) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 24, 21, 11, 5, 651, DateTimeKind.Local).AddTicks(8100), new DateTime(2024, 4, 24, 21, 11, 5, 651, DateTimeKind.Local).AddTicks(8102) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 24, 21, 11, 5, 651, DateTimeKind.Local).AddTicks(8103), new DateTime(2024, 4, 24, 21, 11, 5, 651, DateTimeKind.Local).AddTicks(8105) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 24, 21, 11, 5, 651, DateTimeKind.Local).AddTicks(8107), new DateTime(2024, 4, 24, 21, 11, 5, 651, DateTimeKind.Local).AddTicks(8108) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 4, 24, 21, 11, 5, 651, DateTimeKind.Local).AddTicks(8126), new DateTime(2024, 4, 24, 21, 11, 5, 651, DateTimeKind.Local).AddTicks(8128) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlanEndDate",
                table: "PurchasingTasks");

            migrationBuilder.DropColumn(
                name: "PlanStartDate",
                table: "PurchasingTasks");

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
        }
    }
}
