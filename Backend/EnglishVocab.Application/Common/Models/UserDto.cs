using System;

namespace EnglishVocab.Application.Common.Models
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string[] Roles { get; set; }
        
        public string FullName => $"{FirstName} {LastName}".Trim();
    }
} 