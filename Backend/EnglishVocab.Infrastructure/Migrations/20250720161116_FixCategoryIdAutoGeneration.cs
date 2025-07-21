using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EnglishVocab.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixCategoryIdAutoGeneration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DifficultyLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DifficultyLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    RedirectUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    TotalWordsStudied = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    TotalSessions = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    TotalTimeSpent = table.Column<int>(type: "int", nullable: false),
                    MasteredWords = table.Column<int>(type: "int", nullable: false),
                    LearningWords = table.Column<int>(type: "int", nullable: false),
                    StreakDays = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    LastStudyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalPoints = table.Column<int>(type: "int", nullable: false),
                    CurrentStreak = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    LongestStreak = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    TotalWordsLearned = table.Column<int>(type: "int", nullable: false),
                    TotalTimeSpentMinutes = table.Column<int>(type: "int", nullable: false),
                    LastActivity = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StreakUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStatistics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WordSets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    WordCount = table.Column<int>(type: "int", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
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
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    English = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Vietnamese = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Pronunciation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Example = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AudioUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    DifficultyLevelId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Words_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Words_DifficultyLevels_DifficultyLevelId",
                        column: x => x.DifficultyLevelId,
                        principalTable: "DifficultyLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "StudySessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    WordSetId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WordsStudied = table.Column<int>(type: "int", nullable: false),
                    WordsKnown = table.Column<int>(type: "int", nullable: false),
                    WordsUnknown = table.Column<int>(type: "int", nullable: false),
                    WordsSkipped = table.Column<int>(type: "int", nullable: false),
                    PointsEarned = table.Column<int>(type: "int", nullable: false),
                    StudyMode = table.Column<int>(type: "int", nullable: false),
                    ShuffleWords = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CorrectAnswers = table.Column<int>(type: "int", nullable: false),
                    IncorrectAnswers = table.Column<int>(type: "int", nullable: false),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    WordId = table.Column<int>(type: "int", nullable: false),
                    MasteryLevel = table.Column<int>(type: "int", nullable: false),
                    RepetitionCount = table.Column<int>(type: "int", nullable: false),
                    LastReviewed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NextReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EaseFactorInPercentage = table.Column<int>(type: "int", nullable: false),
                    IntervalInDays = table.Column<int>(type: "int", nullable: false),
                    TimesReviewed = table.Column<int>(type: "int", nullable: false),
                    LastReviewedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReviewCount = table.Column<int>(type: "int", nullable: false),
                    CorrectCount = table.Column<int>(type: "int", nullable: false),
                    IncorrectCount = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProgresses_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WordSetWords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WordId = table.Column<int>(type: "int", nullable: false),
                    WordSetId = table.Column<int>(type: "int", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordSetWords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordSetWords_WordSets_WordSetId",
                        column: x => x.WordSetId,
                        principalTable: "WordSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WordSetWords_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    StudySessionId = table.Column<int>(type: "int", nullable: true),
                    WordId = table.Column<int>(type: "int", nullable: true),
                    ActionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResponseTimeMs = table.Column<int>(type: "int", nullable: true),
                    EntityType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserActions_StudySessions_StudySessionId",
                        column: x => x.StudySessionId,
                        principalTable: "StudySessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_UserActions_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DateModified", "Description", "ModifiedBy", "Name" },
                values: new object[,]
                {
                    { 1, "System", new DateTime(2025, 7, 20, 23, 11, 15, 695, DateTimeKind.Local).AddTicks(5421), new DateTime(2025, 7, 20, 23, 11, 15, 696, DateTimeKind.Local).AddTicks(3296), "Common greetings and introductions", "System", "Greetings" },
                    { 2, "System", new DateTime(2025, 7, 20, 23, 11, 15, 696, DateTimeKind.Local).AddTicks(3531), new DateTime(2025, 7, 20, 23, 11, 15, 696, DateTimeKind.Local).AddTicks(3533), "Common expressions and phrases", "System", "Expressions" }
                });

            migrationBuilder.InsertData(
                table: "DifficultyLevels",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DateModified", "Description", "ModifiedBy", "Name", "Value" },
                values: new object[,]
                {
                    { 0, "System", new DateTime(2025, 7, 20, 23, 11, 15, 697, DateTimeKind.Local).AddTicks(5124), new DateTime(2025, 7, 20, 23, 11, 15, 697, DateTimeKind.Local).AddTicks(5129), "Basic vocabulary for beginners", "System", "Easy", 0 },
                    { 1, "System", new DateTime(2025, 7, 20, 23, 11, 15, 697, DateTimeKind.Local).AddTicks(5133), new DateTime(2025, 7, 20, 23, 11, 15, 697, DateTimeKind.Local).AddTicks(5134), "Intermediate vocabulary", "System", "Medium", 1 },
                    { 2, "System", new DateTime(2025, 7, 20, 23, 11, 15, 697, DateTimeKind.Local).AddTicks(5136), new DateTime(2025, 7, 20, 23, 11, 15, 697, DateTimeKind.Local).AddTicks(5136), "Advanced vocabulary", "System", "Hard", 2 },
                    { 3, "System", new DateTime(2025, 7, 20, 23, 11, 15, 697, DateTimeKind.Local).AddTicks(5138), new DateTime(2025, 7, 20, 23, 11, 15, 697, DateTimeKind.Local).AddTicks(5139), "Expert level vocabulary", "System", "Very Hard", 3 }
                });

            migrationBuilder.InsertData(
                table: "WordSets",
                columns: new[] { "Id", "Category", "CreatedBy", "CreatedByUserId", "DateCreated", "DateModified", "Description", "ImageUrl", "IsDefault", "IsPublic", "ModifiedBy", "Name", "WordCount" },
                values: new object[,]
                {
                    { 1, "Beginner", "System", "1", new DateTime(2025, 7, 20, 23, 11, 15, 750, DateTimeKind.Local).AddTicks(6665), new DateTime(2025, 7, 20, 23, 11, 15, 750, DateTimeKind.Local).AddTicks(6672), "Essential greetings and phrases for beginners", "/images/wordsets/greetings.jpg", false, true, "System", "Greetings and Basic Phrases", 3 },
                    { 2, "Intermediate", "System", "1", new DateTime(2025, 7, 20, 23, 11, 15, 750, DateTimeKind.Local).AddTicks(6679), new DateTime(2025, 7, 20, 23, 11, 15, 750, DateTimeKind.Local).AddTicks(6680), "Vocabulary related to food, restaurants, and dining", "/images/wordsets/food.jpg", false, true, "System", "Food and Dining", 0 }
                });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "AudioUrl", "CategoryId", "CreatedBy", "DateCreated", "DateModified", "DifficultyLevelId", "English", "Example", "ImageUrl", "ModifiedBy", "Notes", "Pronunciation", "Vietnamese" },
                values: new object[,]
                {
                    { 1, "/audio/hello.mp3", 1, "System", new DateTime(2025, 7, 20, 23, 11, 15, 749, DateTimeKind.Local).AddTicks(8414), new DateTime(2025, 7, 20, 23, 11, 15, 749, DateTimeKind.Local).AddTicks(8426), 0, "Hello", "Hello, how are you today?", "/images/hello.jpg", "System", "Common greeting", "həˈloʊ", "Xin chào" },
                    { 2, "/audio/goodbye.mp3", 1, "System", new DateTime(2025, 7, 20, 23, 11, 15, 749, DateTimeKind.Local).AddTicks(8435), new DateTime(2025, 7, 20, 23, 11, 15, 749, DateTimeKind.Local).AddTicks(8436), 0, "Goodbye", "Goodbye, see you tomorrow!", "/images/goodbye.jpg", "System", "Common farewell", "ɡʊdˈbaɪ", "Tạm biệt" },
                    { 3, "/audio/thankyou.mp3", 2, "System", new DateTime(2025, 7, 20, 23, 11, 15, 749, DateTimeKind.Local).AddTicks(8440), new DateTime(2025, 7, 20, 23, 11, 15, 749, DateTimeKind.Local).AddTicks(8441), 0, "Thank you", "Thank you for your help.", "/images/thankyou.jpg", "System", "Common expression of gratitude", "θæŋk ju", "Cảm ơn" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId_IsRead",
                table: "Notifications",
                columns: new[] { "UserId", "IsRead" });

            migrationBuilder.CreateIndex(
                name: "IX_StudySessions_WordSetId",
                table: "StudySessions",
                column: "WordSetId");

            migrationBuilder.CreateIndex(
                name: "IX_UserActions_StudySessionId",
                table: "UserActions",
                column: "StudySessionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserActions_WordId",
                table: "UserActions",
                column: "WordId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProgresses_UserId_WordId",
                table: "UserProgresses",
                columns: new[] { "UserId", "WordId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProgresses_WordId",
                table: "UserProgresses",
                column: "WordId");

            migrationBuilder.CreateIndex(
                name: "IX_UserStatistics_UserId",
                table: "UserStatistics",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Words_CategoryId",
                table: "Words",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Words_DifficultyLevelId",
                table: "Words",
                column: "DifficultyLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_WordSetWords_WordId",
                table: "WordSetWords",
                column: "WordId");

            migrationBuilder.CreateIndex(
                name: "IX_WordSetWords_WordSetId",
                table: "WordSetWords",
                column: "WordSetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "UserActions");

            migrationBuilder.DropTable(
                name: "UserProgresses");

            migrationBuilder.DropTable(
                name: "UserStatistics");

            migrationBuilder.DropTable(
                name: "WordSetWords");

            migrationBuilder.DropTable(
                name: "StudySessions");

            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "WordSets");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "DifficultyLevels");
        }
    }
}
