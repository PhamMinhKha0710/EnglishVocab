using Microsoft.AspNetCore.Identity;
using System;

namespace EnglishVocab.Identity.Models
{
    public class ApplicationRole : IdentityRole<int>
    {
        public string Description { get; set; } 
        public DateTime CreatedAt { get; set; }
        
        public ApplicationRole() : base()
        {
            CreatedAt = DateTime.UtcNow;
        }
        
        public ApplicationRole(string roleName) : base(roleName)
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
} 