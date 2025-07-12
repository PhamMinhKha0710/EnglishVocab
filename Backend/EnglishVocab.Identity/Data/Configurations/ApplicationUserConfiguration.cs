using EnglishVocab.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EnglishVocab.Identity.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.LastName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.IsActive)
                .HasDefaultValue(true);

            builder.Property(u => u.CreatedAt)
                .IsRequired();
                
            // Seed admin user with fixed ID
            var hasher = new PasswordHasher<ApplicationUser>();
            
            builder.HasData(
                new ApplicationUser
                {
                    Id = "1", // Fixed ID for admin user as string
                    Email = "admin@englishvocab.com",
                    NormalizedEmail = "ADMIN@ENGLISHVOCAB.COM",
                    FirstName = "System",
                    LastName = "Administrator",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    PasswordHash = hasher.HashPassword(null, "Admin@123"),
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
} 