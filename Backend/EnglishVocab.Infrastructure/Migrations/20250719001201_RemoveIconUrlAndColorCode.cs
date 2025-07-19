using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnglishVocab.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIconUrlAndColorCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorCode",
                table: "DifficultyLevels");

            migrationBuilder.DropColumn(
                name: "IconUrl",
                table: "Categories");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 19, 7, 12, 0, 625, DateTimeKind.Local).AddTicks(3523), new DateTime(2025, 7, 19, 7, 12, 0, 626, DateTimeKind.Local).AddTicks(961) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 19, 7, 12, 0, 626, DateTimeKind.Local).AddTicks(1195), new DateTime(2025, 7, 19, 7, 12, 0, 626, DateTimeKind.Local).AddTicks(1197) });

            migrationBuilder.UpdateData(
                table: "DifficultyLevels",
                keyColumn: "Id",
                keyValue: 0,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 19, 7, 12, 0, 627, DateTimeKind.Local).AddTicks(2054), new DateTime(2025, 7, 19, 7, 12, 0, 627, DateTimeKind.Local).AddTicks(2059) });

            migrationBuilder.UpdateData(
                table: "DifficultyLevels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 19, 7, 12, 0, 627, DateTimeKind.Local).AddTicks(2062), new DateTime(2025, 7, 19, 7, 12, 0, 627, DateTimeKind.Local).AddTicks(2063) });

            migrationBuilder.UpdateData(
                table: "DifficultyLevels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 19, 7, 12, 0, 627, DateTimeKind.Local).AddTicks(2065), new DateTime(2025, 7, 19, 7, 12, 0, 627, DateTimeKind.Local).AddTicks(2066) });

            migrationBuilder.UpdateData(
                table: "DifficultyLevels",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 19, 7, 12, 0, 627, DateTimeKind.Local).AddTicks(2068), new DateTime(2025, 7, 19, 7, 12, 0, 627, DateTimeKind.Local).AddTicks(2068) });

            migrationBuilder.UpdateData(
                table: "WordSets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 19, 7, 12, 0, 678, DateTimeKind.Local).AddTicks(319), new DateTime(2025, 7, 19, 7, 12, 0, 678, DateTimeKind.Local).AddTicks(326) });

            migrationBuilder.UpdateData(
                table: "WordSets",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 19, 7, 12, 0, 678, DateTimeKind.Local).AddTicks(331), new DateTime(2025, 7, 19, 7, 12, 0, 678, DateTimeKind.Local).AddTicks(332) });

            migrationBuilder.UpdateData(
                table: "Words",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 19, 7, 12, 0, 677, DateTimeKind.Local).AddTicks(2377), new DateTime(2025, 7, 19, 7, 12, 0, 677, DateTimeKind.Local).AddTicks(2384) });

            migrationBuilder.UpdateData(
                table: "Words",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 19, 7, 12, 0, 677, DateTimeKind.Local).AddTicks(2394), new DateTime(2025, 7, 19, 7, 12, 0, 677, DateTimeKind.Local).AddTicks(2395) });

            migrationBuilder.UpdateData(
                table: "Words",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 19, 7, 12, 0, 677, DateTimeKind.Local).AddTicks(2399), new DateTime(2025, 7, 19, 7, 12, 0, 677, DateTimeKind.Local).AddTicks(2400) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ColorCode",
                table: "DifficultyLevels",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IconUrl",
                table: "Categories",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified", "IconUrl" },
                values: new object[] { new DateTime(2025, 7, 18, 12, 7, 54, 330, DateTimeKind.Local).AddTicks(1017), new DateTime(2025, 7, 18, 12, 7, 54, 331, DateTimeKind.Local).AddTicks(1492), "/images/categories/greetings.png" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified", "IconUrl" },
                values: new object[] { new DateTime(2025, 7, 18, 12, 7, 54, 331, DateTimeKind.Local).AddTicks(1807), new DateTime(2025, 7, 18, 12, 7, 54, 331, DateTimeKind.Local).AddTicks(1810), "/images/categories/expressions.png" });

            migrationBuilder.UpdateData(
                table: "DifficultyLevels",
                keyColumn: "Id",
                keyValue: 0,
                columns: new[] { "ColorCode", "DateCreated", "DateModified" },
                values: new object[] { "#28a745", new DateTime(2025, 7, 18, 12, 7, 54, 332, DateTimeKind.Local).AddTicks(7542), new DateTime(2025, 7, 18, 12, 7, 54, 332, DateTimeKind.Local).AddTicks(7547) });

            migrationBuilder.UpdateData(
                table: "DifficultyLevels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ColorCode", "DateCreated", "DateModified" },
                values: new object[] { "#ffc107", new DateTime(2025, 7, 18, 12, 7, 54, 332, DateTimeKind.Local).AddTicks(7552), new DateTime(2025, 7, 18, 12, 7, 54, 332, DateTimeKind.Local).AddTicks(7553) });

            migrationBuilder.UpdateData(
                table: "DifficultyLevels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ColorCode", "DateCreated", "DateModified" },
                values: new object[] { "#dc3545", new DateTime(2025, 7, 18, 12, 7, 54, 332, DateTimeKind.Local).AddTicks(7556), new DateTime(2025, 7, 18, 12, 7, 54, 332, DateTimeKind.Local).AddTicks(7557) });

            migrationBuilder.UpdateData(
                table: "DifficultyLevels",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ColorCode", "DateCreated", "DateModified" },
                values: new object[] { "#6f42c1", new DateTime(2025, 7, 18, 12, 7, 54, 332, DateTimeKind.Local).AddTicks(7559), new DateTime(2025, 7, 18, 12, 7, 54, 332, DateTimeKind.Local).AddTicks(7560) });

            migrationBuilder.UpdateData(
                table: "WordSets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 18, 12, 7, 54, 389, DateTimeKind.Local).AddTicks(7699), new DateTime(2025, 7, 18, 12, 7, 54, 389, DateTimeKind.Local).AddTicks(7707) });

            migrationBuilder.UpdateData(
                table: "WordSets",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 18, 12, 7, 54, 389, DateTimeKind.Local).AddTicks(7712), new DateTime(2025, 7, 18, 12, 7, 54, 389, DateTimeKind.Local).AddTicks(7713) });

            migrationBuilder.UpdateData(
                table: "Words",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 18, 12, 7, 54, 389, DateTimeKind.Local).AddTicks(508), new DateTime(2025, 7, 18, 12, 7, 54, 389, DateTimeKind.Local).AddTicks(521) });

            migrationBuilder.UpdateData(
                table: "Words",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 18, 12, 7, 54, 389, DateTimeKind.Local).AddTicks(529), new DateTime(2025, 7, 18, 12, 7, 54, 389, DateTimeKind.Local).AddTicks(530) });

            migrationBuilder.UpdateData(
                table: "Words",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 7, 18, 12, 7, 54, 389, DateTimeKind.Local).AddTicks(533), new DateTime(2025, 7, 18, 12, 7, 54, 389, DateTimeKind.Local).AddTicks(534) });
        }
    }
}
