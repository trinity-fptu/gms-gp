using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixTaskDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlanStartDate",
                table: "PurchasingTasks",
                newName: "TaskStartDate");

            migrationBuilder.RenameColumn(
                name: "PlanEndDate",
                table: "PurchasingTasks",
                newName: "TaskEndDate");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaskStartDate",
                table: "PurchasingTasks",
                newName: "PlanStartDate");

            migrationBuilder.RenameColumn(
                name: "TaskEndDate",
                table: "PurchasingTasks",
                newName: "PlanEndDate");

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
    }
}
