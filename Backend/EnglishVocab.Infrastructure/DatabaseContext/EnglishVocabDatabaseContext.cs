using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Domain.Entities;
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
    public class EnglishVocabDatabaseContext : DbContext, IApplicationDbContext
    {
        private readonly IUserService _userService;

        public EnglishVocabDatabaseContext(DbContextOptions<EnglishVocabDatabaseContext> options, IUserService userService) : base(options)
        {
            _userService = userService;
        }

        public DbSet<Word> Words { get; set; }
        public DbSet<WordSet> WordSets { get; set; }
        public DbSet<StudySession> StudySessions { get; set; }
        public DbSet<UserProgress> UserProgresses { get; set; }
        public DbSet<UserAction> UserActions { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserStatistics> UserStatistics { get; set; }
        public DbSet<WordSetWord> WordSetWords { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<DifficultyLevel> DifficultyLevels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply all configurations from assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Update audit fields on entities
            foreach (var entry in base.ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.DateCreated = DateTime.UtcNow;
                        entry.Entity.CreatedBy = _userService.UserId;
                        entry.Entity.DateModified = DateTime.UtcNow;
                        entry.Entity.ModifiedBy = _userService.UserId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.DateModified = DateTime.UtcNow;
                        entry.Entity.ModifiedBy = _userService.UserId;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .ConfigureWarnings(warnings =>
                    warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
    }
} 