using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Domain.Entities;
using EnglishVocab.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishVocab.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly EnglishVocabDatabaseContext _context;

        public NotificationRepository(EnglishVocabDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Notification> AddAsync(Notification notification)
        {
            notification.DateCreated = DateTime.UtcNow;
            notification.DateModified = DateTime.UtcNow;
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
            return notification;
        }

        public async Task<int> CountUnreadAsync(string userId)
        {
            return await _context.Notifications
                .CountAsync(n => n.UserId == userId && !n.IsRead);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
                return false;

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Notification> GetByIdAsync(int id)
        {
            return await _context.Notifications.FindAsync(id);
        }

        public async Task<IEnumerable<Notification>> GetByUserIdAsync(string userId)
        {
            return await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.DateCreated)
                .ToListAsync();
        }

        public async Task<IEnumerable<Notification>> GetUnreadByUserIdAsync(string userId)
        {
            return await _context.Notifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .OrderByDescending(n => n.DateCreated)
                .ToListAsync();
        }

        public async Task<bool> MarkAllAsReadAsync(string userId)
        {
            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .ToListAsync();

            if (!notifications.Any())
                return true;

            foreach (var notification in notifications)
            {
                notification.IsRead = true;
                notification.DateModified = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MarkAsReadAsync(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
                return false;

            notification.IsRead = true;
            notification.DateModified = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 