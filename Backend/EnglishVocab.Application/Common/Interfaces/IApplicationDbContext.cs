using EnglishVocab.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EnglishVocab.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Word> Words { get; set; }
        DbSet<WordSet> WordSets { get; set; }
        DbSet<StudySession> StudySessions { get; set; }
        DbSet<UserProgress> UserProgresses { get; set; }
        DbSet<UserAction> UserActions { get; set; }
        DbSet<Notification> Notifications { get; set; }
        DbSet<UserStatistics> UserStatistics { get; set; }
        DbSet<WordSetWord> WordSetWords { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<DifficultyLevel> DifficultyLevels { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
} 