using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Domain;
using EnglishVocab.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Infrastructure.DatabaseContext
{
    public class EnglishVocabDatabaseContext : DbContext
    {
        private readonly IUserService _userService;

        public EnglishVocabDatabaseContext(DbContextOptions<EnglishVocabDatabaseContext> options, IUserService userService) : base(options)
        {
            _userService = userService;
        }

        public DbSet<Word> Words { get; set; }
        public DbSet<WordSet> WordSets { get; set; }
        public DbSet<StudySession> StudySessions { get; set; }
        public DbSet<UserProgress> UserProgress { get; set; }
        public DbSet<UserAction> UserActions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
            
            // Word Configuration
            modelBuilder.Entity<Word>()
                .HasKey(w => w.Id);

            modelBuilder.Entity<Word>()
                .Property(w => w.English)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Word>()
                .Property(w => w.Vietnamese)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Word>()
                .Property(w => w.Pronunciation)
                .HasMaxLength(100);

            modelBuilder.Entity<Word>()  
                .Property(w => w.Example)
                .HasMaxLength(500);

            modelBuilder.Entity<Word>()
                .Property(w => w.Notes)
                .HasMaxLength(500);

            modelBuilder.Entity<Word>()
                .Property(w => w.ImageUrl)
                .HasMaxLength(255);

            modelBuilder.Entity<Word>()
                .Property(w => w.AudioUrl)
                .HasMaxLength(255);

            modelBuilder.Entity<Word>()
                .Property(w => w.DifficultyLevel)
                .IsRequired();

            // Quan hệ Word - WordSet
            modelBuilder.Entity<Word>()
                .HasOne(w => w.WordSet)
                .WithMany(ws => ws.Words)
                .HasForeignKey(w => w.WordSetId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            // Quan hệ Word - UserProgress
            modelBuilder.Entity<Word>()
                .HasMany(w => w.UserProgress)
                .WithOne(up => up.Word)
                .HasForeignKey(up => up.WordId)
                .OnDelete(DeleteBehavior.NoAction);

            // WordSet Configuration
            modelBuilder.Entity<WordSet>()
                .HasKey(ws => ws.Id);

            modelBuilder.Entity<WordSet>()
                .Property(ws => ws.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<WordSet>()
                .Property(ws => ws.Description)
                .HasMaxLength(500);

            modelBuilder.Entity<WordSet>()
                .Property(ws => ws.Category)
                .HasMaxLength(50);

            modelBuilder.Entity<WordSet>()
                .Property(ws => ws.ImageUrl)
                .HasMaxLength(255);

            modelBuilder.Entity<WordSet>()
                .Property(ws => ws.IsPublic)
                .IsRequired();

            // Quan hệ WordSet - UserId (không tham chiếu đến User entity)
            modelBuilder.Entity<WordSet>()
                .Property(ws => ws.UserId)
                .IsRequired();

            // Quan hệ WordSet - Word
            modelBuilder.Entity<WordSet>()
                .HasMany(ws => ws.Words)
                .WithOne(w => w.WordSet)
                .HasForeignKey(w => w.WordSetId)
                .OnDelete(DeleteBehavior.Cascade);

            // Quan hệ WordSet - StudySession
            modelBuilder.Entity<WordSet>()
                .HasMany(ws => ws.StudySessions)
                .WithOne(ss => ss.WordSet)
                .HasForeignKey(ss => ss.WordSetId)
                .OnDelete(DeleteBehavior.NoAction);

            // UserProgress Configuration
            modelBuilder.Entity<UserProgress>()
                .HasKey(up => up.Id);

            modelBuilder.Entity<UserProgress>()
                .Property(up => up.MasteryLevel)
                .IsRequired();

            modelBuilder.Entity<UserProgress>()
                .Property(up => up.CorrectCount)
                .IsRequired();

            modelBuilder.Entity<UserProgress>()
                .Property(up => up.IncorrectCount)
                .IsRequired();

            // Quan hệ UserProgress - UserId (không tham chiếu đến User entity)
            modelBuilder.Entity<UserProgress>()
                .Property(up => up.UserId)
                .IsRequired();

            // Quan hệ UserProgress - Word
            modelBuilder.Entity<UserProgress>()
                .HasOne(up => up.Word)
                .WithMany(w => w.UserProgress)
                .HasForeignKey(up => up.WordId)
                .OnDelete(DeleteBehavior.NoAction);

            // Tạo index để tìm kiếm nhanh
            modelBuilder.Entity<UserProgress>()
                .HasIndex(up => new { up.UserId, up.WordId })
                .IsUnique();

            // StudySession Configuration
            modelBuilder.Entity<StudySession>()
                .HasKey(ss => ss.Id);

            modelBuilder.Entity<StudySession>()
                .Property(ss => ss.StartTime)
                .IsRequired();

            modelBuilder.Entity<StudySession>()
                .Property(ss => ss.WordsStudied)
                .IsRequired();

            modelBuilder.Entity<StudySession>()
                .Property(ss => ss.CorrectAnswers)
                .IsRequired();

            modelBuilder.Entity<StudySession>()
                .Property(ss => ss.IncorrectAnswers)
                .IsRequired();

            modelBuilder.Entity<StudySession>()
                .Property(ss => ss.StudyMode)
                .IsRequired();
            
            // Quan hệ StudySession - UserId (không tham chiếu đến User entity)
            modelBuilder.Entity<StudySession>()
                .Property(ss => ss.UserId)
                .IsRequired();

            // Quan hệ StudySession - WordSet
            modelBuilder.Entity<StudySession>()
                .HasOne(ss => ss.WordSet)
                .WithMany(ws => ws.StudySessions)
                .HasForeignKey(ss => ss.WordSetId)
                .OnDelete(DeleteBehavior.NoAction);

            // UserAction Configuration
            modelBuilder.Entity<UserAction>()
                .HasKey(ua => ua.Id);

            modelBuilder.Entity<UserAction>()
                .Property(ua => ua.ActionType)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<UserAction>()
                .Property(ua => ua.Source)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<UserAction>()
                .Property(ua => ua.Description)
                .HasMaxLength(500);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var userActions = new List<UserAction>();

            foreach (var entry in base.ChangeTracker.Entries<BaseEntity>())
            {
                entry.Entity.DateModified = DateTime.Now;
                entry.Entity.ModifiedBy = _userService.UserId;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.Now;
                    entry.Entity.CreatedBy = _userService.UserId;
                }

                // Không ghi log cho các thao tác UserAction để tránh vòng lặp vô tận
                if (entry.Entity is UserAction) continue;

                userActions.Add(new UserAction
                {
                    ActionType = entry.State.ToString(),
                    Source = entry.Entity.GetType().Name,
                    SourceId = entry.Entity.Id,
                    UserId = _userService.UserId,
                    Description = $"{entry.Entity.GetType().Name} {entry.State.ToString()}.",
                    DateCreated = DateTime.Now,
                    CreatedBy = _userService.UserId,
                    DateModified = DateTime.Now,
                    ModifiedBy = _userService.UserId,
                });
            }

            // First save changes to ensure Ids are generated
            var result = await base.SaveChangesAsync(cancellationToken);

            foreach (var userAction in userActions)
            {
                var entry = base.ChangeTracker.Entries<BaseEntity>()
                    .FirstOrDefault(e => e.Entity.GetType().Name == userAction.Source
                                         && e.Entity.Id > 0);

                if (entry != null)
                {
                    // Update SourceId after save entity
                    userAction.SourceId = entry.Entity.Id;
                }
            }

            // Then add user actions
            if (userActions.Any())
            {
                await Set<UserAction>().AddRangeAsync(userActions, cancellationToken);
                await base.SaveChangesAsync(cancellationToken);
            }

            return result;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .ConfigureWarnings(warnings =>
                    warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
    }
} 