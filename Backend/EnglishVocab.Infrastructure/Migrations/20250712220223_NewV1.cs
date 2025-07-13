using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EnglishVocab.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewV1 : Migration
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
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DifficultyLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    English = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Vietnamese = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Pronunciation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Example = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ExampleTranslation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AudioUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DifficultyLevel = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    Frequency = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
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
                        principalColumn: "Id");
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
                table: "DifficultyLevels",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DateModified", "Description", "ModifiedBy", "Name", "Value" },
                values: new object[,]
                {
                    { 1, "System", new DateTime(2025, 7, 13, 5, 2, 23, 267, DateTimeKind.Local).AddTicks(6907), new DateTime(2025, 7, 13, 5, 2, 23, 268, DateTimeKind.Local).AddTicks(7737), "Words that are commonly used in everyday conversations", "System", "Easy", 1 },
                    { 2, "System", new DateTime(2025, 7, 13, 5, 2, 23, 268, DateTimeKind.Local).AddTicks(8052), new DateTime(2025, 7, 13, 5, 2, 23, 268, DateTimeKind.Local).AddTicks(8055), "Words that are used in general contexts but less frequently", "System", "Medium", 2 },
                    { 3, "System", new DateTime(2025, 7, 13, 5, 2, 23, 268, DateTimeKind.Local).AddTicks(8058), new DateTime(2025, 7, 13, 5, 2, 23, 268, DateTimeKind.Local).AddTicks(8059), "Words that are specialized or rarely used in everyday language", "System", "Hard", 3 }
                });

            migrationBuilder.InsertData(
                table: "WordSets",
                columns: new[] { "Id", "Category", "CreatedBy", "CreatedByUserId", "DateCreated", "DateModified", "Description", "ImageUrl", "IsDefault", "IsPublic", "ModifiedBy", "Name", "WordCount" },
                values: new object[,]
                {
                    { 1, "Beginner", "System", "1", new DateTime(2025, 7, 13, 5, 2, 23, 286, DateTimeKind.Local).AddTicks(6341), new DateTime(2025, 7, 13, 5, 2, 23, 286, DateTimeKind.Local).AddTicks(6346), "Essential greetings and phrases for beginners", "/images/wordsets/greetings.jpg", false, true, "System", "Greetings and Basic Phrases", 3 },
                    { 2, "Intermediate", "System", "1", new DateTime(2025, 7, 13, 5, 2, 23, 286, DateTimeKind.Local).AddTicks(6351), new DateTime(2025, 7, 13, 5, 2, 23, 286, DateTimeKind.Local).AddTicks(6352), "Vocabulary related to food, restaurants, and dining", "/images/wordsets/food.jpg", false, true, "System", "Food and Dining", 0 }
                });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "AudioUrl", "Category", "CategoryId", "CreatedBy", "DateCreated", "DateModified", "DifficultyLevel", "DifficultyLevelId", "English", "Example", "ExampleTranslation", "Frequency", "ImageUrl", "ModifiedBy", "Notes", "Pronunciation", "Vietnamese" },
                values: new object[,]
                {
                    { 1, "/audio/hello.mp3", "Greetings", null, "System", new DateTime(2025, 7, 13, 5, 2, 23, 285, DateTimeKind.Local).AddTicks(9326), new DateTime(2025, 7, 13, 5, 2, 23, 285, DateTimeKind.Local).AddTicks(9331), 0, null, "Hello", "Hello, how are you today?", "Xin chào, hôm nay bạn khỏe không?", 0, "/images/hello.jpg", "System", "Common greeting", "həˈloʊ", "Xin chào" },
                    { 2, "/audio/goodbye.mp3", "Greetings", null, "System", new DateTime(2025, 7, 13, 5, 2, 23, 285, DateTimeKind.Local).AddTicks(9337), new DateTime(2025, 7, 13, 5, 2, 23, 285, DateTimeKind.Local).AddTicks(9338), 0, null, "Goodbye", "Goodbye, see you tomorrow!", "Tạm biệt, hẹn gặp lại ngày mai!", 0, "/images/goodbye.jpg", "System", "Common farewell", "ɡʊdˈbaɪ", "Tạm biệt" },
                    { 3, "/audio/thankyou.mp3", "Expressions", null, "System", new DateTime(2025, 7, 13, 5, 2, 23, 285, DateTimeKind.Local).AddTicks(9341), new DateTime(2025, 7, 13, 5, 2, 23, 285, DateTimeKind.Local).AddTicks(9342), 0, null, "Thank you", "Thank you for your help.", "Cảm ơn vì sự giúp đỡ của bạn.", 0, "/images/thankyou.jpg", "System", "Common expression of gratitude", "θæŋk ju", "Cảm ơn" }
                });

            migrationBuilder.InsertData(
                table: "StudySessions",
                columns: new[] { "Id", "CorrectAnswers", "CreatedBy", "DateCreated", "DateModified", "EndTime", "IncorrectAnswers", "ModifiedBy", "PointsEarned", "ShuffleWords", "StartTime", "Status", "StudyMode", "UserId", "WordSetId", "WordsKnown", "WordsSkipped", "WordsStudied", "WordsUnknown" },
                values: new object[,]
                {
                    { 1, 7, "System", new DateTime(2025, 7, 13, 5, 2, 23, 273, DateTimeKind.Local).AddTicks(9465), new DateTime(2025, 7, 13, 5, 2, 23, 273, DateTimeKind.Local).AddTicks(9470), new DateTime(2025, 7, 13, 4, 32, 23, 273, DateTimeKind.Local).AddTicks(7810), 3, "System", 0, false, new DateTime(2025, 7, 13, 4, 2, 23, 273, DateTimeKind.Local).AddTicks(7810), "active", 0, "1", 1, 0, 0, 10, 0 },
                    { 2, 12, "System", new DateTime(2025, 7, 13, 5, 2, 23, 273, DateTimeKind.Local).AddTicks(9490), new DateTime(2025, 7, 13, 5, 2, 23, 273, DateTimeKind.Local).AddTicks(9611), new DateTime(2025, 7, 12, 5, 47, 23, 273, DateTimeKind.Local).AddTicks(7810), 3, "System", 0, false, new DateTime(2025, 7, 12, 5, 2, 23, 273, DateTimeKind.Local).AddTicks(7810), "active", 4, "1", 2, 0, 0, 15, 0 }
                });

            migrationBuilder.InsertData(
                table: "UserProgresses",
                columns: new[] { "Id", "CorrectCount", "CreatedBy", "DateCreated", "DateModified", "EaseFactorInPercentage", "IncorrectCount", "IntervalInDays", "LastReviewed", "LastReviewedAt", "MasteryLevel", "ModifiedBy", "NextReviewDate", "RepetitionCount", "ReviewCount", "TimesReviewed", "UserId", "WordId" },
                values: new object[,]
                {
                    { 1, 2, "System", new DateTime(2025, 7, 13, 5, 2, 23, 277, DateTimeKind.Local).AddTicks(7130), new DateTime(2025, 7, 13, 5, 2, 23, 277, DateTimeKind.Local).AddTicks(7134), 250, 1, 0, new DateTime(2025, 7, 12, 5, 2, 23, 277, DateTimeKind.Local).AddTicks(4994), new DateTime(2025, 7, 12, 22, 2, 23, 277, DateTimeKind.Utc).AddTicks(6063), 1, "System", new DateTime(2025, 7, 14, 5, 2, 23, 277, DateTimeKind.Local).AddTicks(4994), 0, 0, 0, "1", 1 },
                    { 2, 0, "System", new DateTime(2025, 7, 13, 5, 2, 23, 277, DateTimeKind.Local).AddTicks(7141), new DateTime(2025, 7, 13, 5, 2, 23, 277, DateTimeKind.Local).AddTicks(7142), 250, 0, 0, new DateTime(2025, 7, 12, 22, 2, 23, 277, DateTimeKind.Utc).AddTicks(7138), new DateTime(2025, 7, 12, 22, 2, 23, 277, DateTimeKind.Utc).AddTicks(7139), -1, "System", new DateTime(2025, 7, 13, 5, 2, 23, 277, DateTimeKind.Local).AddTicks(4994), 0, 0, 0, "1", 2 }
                });

            migrationBuilder.InsertData(
                table: "WordSetWords",
                columns: new[] { "Id", "AddedDate", "CreatedBy", "DateCreated", "DateModified", "ModifiedBy", "WordId", "WordSetId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 13, 5, 2, 23, 288, DateTimeKind.Local).AddTicks(7275), "System", new DateTime(2025, 7, 13, 5, 2, 23, 288, DateTimeKind.Local).AddTicks(7408), new DateTime(2025, 7, 13, 5, 2, 23, 288, DateTimeKind.Local).AddTicks(7409), "System", 1, 1 },
                    { 2, new DateTime(2025, 7, 13, 5, 2, 23, 288, DateTimeKind.Local).AddTicks(7413), "System", new DateTime(2025, 7, 13, 5, 2, 23, 288, DateTimeKind.Local).AddTicks(7414), new DateTime(2025, 7, 13, 5, 2, 23, 288, DateTimeKind.Local).AddTicks(7415), "System", 2, 1 },
                    { 3, new DateTime(2025, 7, 13, 5, 2, 23, 288, DateTimeKind.Local).AddTicks(7416), "System", new DateTime(2025, 7, 13, 5, 2, 23, 288, DateTimeKind.Local).AddTicks(7417), new DateTime(2025, 7, 13, 5, 2, 23, 288, DateTimeKind.Local).AddTicks(7418), "System", 3, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

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
