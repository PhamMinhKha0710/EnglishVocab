using EnglishVocab.Domain.Entities;


namespace EnglishVocab.Application.Common.Interfaces
{
    public interface INotificationRepository
    {
        Task<Notification> GetByIdAsync(int id);
        Task<IEnumerable<Notification>> GetByUserIdAsync(string userId);
        Task<IEnumerable<Notification>> GetUnreadByUserIdAsync(string userId);
        Task<int> CountUnreadAsync(string userId);
        Task<bool> MarkAsReadAsync(int id);
        Task<bool> MarkAllAsReadAsync(string userId);
        Task<Notification> AddAsync(Notification notification);
        Task<bool> DeleteAsync(int id);
    }
} 