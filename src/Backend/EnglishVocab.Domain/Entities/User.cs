using System.Collections.Generic;

namespace EnglishVocab.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; } 
        public string PasswordHash { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public bool IsActive { get; set; } = true;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        
        // Navigation properties
        public ICollection<WordSet> WordSets { get; set; } = new List<WordSet>();
        public ICollection<UserProgress> UserProgress { get; set; } = new List<UserProgress>();
        public ICollection<StudySession> StudySessions { get; set; } = new List<StudySession>();
    }
} 