using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EnglishVocab.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserActions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Source = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SourceId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WordSets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordSets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudySessions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    WordSetId = table.Column<long>(type: "bigint", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WordsStudied = table.Column<int>(type: "int", nullable: false),
                    CorrectAnswers = table.Column<int>(type: "int", nullable: false),
                    IncorrectAnswers = table.Column<int>(type: "int", nullable: false),
                    StudyMode = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudySessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudySessions_WordSets_WordSetId",
                        column: x => x.WordSetId,
                        principalTable: "WordSets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    English = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Vietnamese = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Pronunciation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Example = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AudioUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DifficultyLevel = table.Column<int>(type: "int", nullable: false),
                    WordSetId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Words_WordSets_WordSetId",
                        column: x => x.WordSetId,
                        principalTable: "WordSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProgress",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    WordId = table.Column<long>(type: "bigint", nullable: false),
                    MasteryLevel = table.Column<int>(type: "int", nullable: false),
                    CorrectCount = table.Column<int>(type: "int", nullable: false),
                    IncorrectCount = table.Column<int>(type: "int", nullable: false),
                    LastReviewed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NextReviewDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProgress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProgress_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "WordSets",
                columns: new[] { "Id", "Category", "CreatedBy", "DateCreated", "DateModified", "Description", "ImageUrl", "IsPublic", "ModifiedBy", "Name", "UserId" },
                values: new object[,]
                {
                    { 1L, "Beginner", "System", new DateTime(2025, 6, 29, 2, 2, 24, 349, DateTimeKind.Local).AddTicks(4643), new DateTime(2025, 6, 29, 2, 2, 24, 349, DateTimeKind.Local).AddTicks(4647), "Essential greetings and phrases for beginners", null, true, "System", "Greetings and Basic Phrases", 1L },
                    { 2L, "Intermediate", "System", new DateTime(2025, 6, 29, 2, 2, 24, 349, DateTimeKind.Local).AddTicks(4651), new DateTime(2025, 6, 29, 2, 2, 24, 349, DateTimeKind.Local).AddTicks(4652), "Vocabulary related to food, restaurants, and dining", null, true, "System", "Food and Dining", 1L }
                });

            migrationBuilder.InsertData(
                table: "StudySessions",
                columns: new[] { "Id", "CorrectAnswers", "CreatedBy", "DateCreated", "DateModified", "EndTime", "IncorrectAnswers", "ModifiedBy", "StartTime", "StudyMode", "UserId", "WordSetId", "WordsStudied" },
                values: new object[,]
                {
                    { 1L, 7, "System", new DateTime(2025, 6, 29, 2, 2, 24, 347, DateTimeKind.Local).AddTicks(8098), new DateTime(2025, 6, 29, 2, 2, 24, 347, DateTimeKind.Local).AddTicks(8517), new DateTime(2025, 6, 29, 1, 32, 24, 346, DateTimeKind.Local).AddTicks(8696), 3, "System", new DateTime(2025, 6, 29, 1, 2, 24, 346, DateTimeKind.Local).AddTicks(8696), 0, 1L, 1L, 10 },
                    { 2L, 12, "System", new DateTime(2025, 6, 29, 2, 2, 24, 347, DateTimeKind.Local).AddTicks(8705), new DateTime(2025, 6, 29, 2, 2, 24, 347, DateTimeKind.Local).AddTicks(8706), new DateTime(2025, 6, 28, 2, 47, 24, 346, DateTimeKind.Local).AddTicks(8696), 3, "System", new DateTime(2025, 6, 28, 2, 2, 24, 346, DateTimeKind.Local).AddTicks(8696), 1, 1L, 2L, 15 }
                });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "AudioUrl", "CreatedBy", "DateCreated", "DateModified", "DifficultyLevel", "English", "Example", "ImageUrl", "ModifiedBy", "Notes", "Pronunciation", "Vietnamese", "WordSetId" },
                values: new object[,]
                {
                    { 1L, "/audio/hello.mp3", "System", new DateTime(2025, 6, 29, 2, 2, 24, 349, DateTimeKind.Local).AddTicks(1731), new DateTime(2025, 6, 29, 2, 2, 24, 349, DateTimeKind.Local).AddTicks(1735), 0, "Hello", "Hello, how are you today?", "/images/hello.jpg", "System", "Common greeting", "həˈloʊ", "Xin chào", 1L },
                    { 2L, "/audio/goodbye.mp3", "System", new DateTime(2025, 6, 29, 2, 2, 24, 349, DateTimeKind.Local).AddTicks(1742), new DateTime(2025, 6, 29, 2, 2, 24, 349, DateTimeKind.Local).AddTicks(1743), 0, "Goodbye", "Goodbye, see you tomorrow!", "/images/goodbye.jpg", "System", "Common farewell", "ɡʊdˈbaɪ", "Tạm biệt", 1L },
                    { 3L, "/audio/thankyou.mp3", "System", new DateTime(2025, 6, 29, 2, 2, 24, 349, DateTimeKind.Local).AddTicks(1746), new DateTime(2025, 6, 29, 2, 2, 24, 349, DateTimeKind.Local).AddTicks(1747), 0, "Thank you", "Thank you for your help.", "/images/thankyou.jpg", "System", "Common expression of gratitude", "θæŋk ju", "Cảm ơn", 1L }
                });

            migrationBuilder.InsertData(
                table: "UserProgress",
                columns: new[] { "Id", "CorrectCount", "CreatedBy", "DateCreated", "DateModified", "IncorrectCount", "LastReviewed", "MasteryLevel", "ModifiedBy", "NextReviewDate", "UserId", "WordId" },
                values: new object[,]
                {
                    { 1L, 2, "System", new DateTime(2025, 6, 29, 2, 2, 24, 348, DateTimeKind.Local).AddTicks(7660), new DateTime(2025, 6, 29, 2, 2, 24, 348, DateTimeKind.Local).AddTicks(7663), 1, new DateTime(2025, 6, 28, 2, 2, 24, 348, DateTimeKind.Local).AddTicks(6644), 1, "System", new DateTime(2025, 6, 30, 2, 2, 24, 348, DateTimeKind.Local).AddTicks(6644), 1L, 1L },
                    { 2L, 0, "System", new DateTime(2025, 6, 29, 2, 2, 24, 348, DateTimeKind.Local).AddTicks(7667), new DateTime(2025, 6, 29, 2, 2, 24, 348, DateTimeKind.Local).AddTicks(7668), 0, null, 0, "System", new DateTime(2025, 6, 29, 2, 2, 24, 348, DateTimeKind.Local).AddTicks(6644), 1L, 2L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudySessions_WordSetId",
                table: "StudySessions",
                column: "WordSetId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProgress_UserId_WordId",
                table: "UserProgress",
                columns: new[] { "UserId", "WordId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProgress_WordId",
                table: "UserProgress",
                column: "WordId");

            migrationBuilder.CreateIndex(
                name: "IX_Words_WordSetId",
                table: "Words",
                column: "WordSetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudySessions");

            migrationBuilder.DropTable(
                name: "UserActions");

            migrationBuilder.DropTable(
                name: "UserProgress");

            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "WordSets");
        }
    }
}
