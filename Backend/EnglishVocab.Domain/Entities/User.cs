using EnglishVocab.Constants.Constant;
using EnglishVocab.Domain.Common;
using System;
using System.Collections.Generic;

namespace EnglishVocab.Domain
{
    public class User : BaseEntity
    {
        public string Username { get; set; } 
        public string Email { get; set; } 
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; } = UserRole.User;
        public DateTime LastLoginDate { get; set; }
        public bool IsActive { get; set; } = true;
        
        // Navigation properties
        public ICollection<WordSet> WordSets { get; set; } = new List<WordSet>();
        public ICollection<StudySession> StudySessions { get; set; } = new List<StudySession>();
        public ICollection<UserProgress> Progress { get; set; } = new List<UserProgress>();
        
        public User()
        {
            LastLoginDate = DateTime.UtcNow;
        }
        
        public string FullName => $"{FirstName} {LastName}".Trim();
    }
} 