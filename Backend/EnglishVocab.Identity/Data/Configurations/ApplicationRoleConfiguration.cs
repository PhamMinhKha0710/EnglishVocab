using EnglishVocab.Constants.Constant;
using EnglishVocab.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EnglishVocab.Identity.Data.Configurations
{
    public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.Property(r => r.Description)
                .HasMaxLength(255);

            builder.Property(r => r.CreatedAt)
                .IsRequired();
                
            // Seed Admin and User roles with fixed IDs
            builder.HasData(
                new ApplicationRole
                {
                    Id = 1, // Fixed ID for Admin role
                    Name = UserRole.Admin.ToString(),
                    NormalizedName = UserRole.Admin.ToString().ToUpper(),
                    Description = "Quản trị viên có toàn quyền truy cập hệ thống",
                    CreatedAt = DateTime.UtcNow
                },
                new ApplicationRole
                {
                    Id = 2, // Fixed ID for User role
                    Name = UserRole.User.ToString(),
                    NormalizedName = UserRole.User.ToString().ToUpper(),
                    Description = "Người dùng thông thường",
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
} 