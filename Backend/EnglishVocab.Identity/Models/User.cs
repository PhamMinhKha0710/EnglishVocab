using Microsoft.AspNetCore.Identity;
using System;

namespace EnglishVocab.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        
        public ApplicationUser()
        {
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }
        
        public string FullName => $"{FirstName} {LastName}".Trim();
    }
} 