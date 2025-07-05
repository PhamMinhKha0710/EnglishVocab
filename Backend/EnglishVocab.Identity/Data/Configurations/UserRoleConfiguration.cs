using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EnglishVocab.Identity.Data.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<int>> builder)
        {
            // We need to ensure these IDs match the ones in ApplicationUserConfiguration and ApplicationRoleConfiguration
            var adminRoleId = 1; // Admin role ID
            var adminUserId = 1; // Admin user ID
            
            builder.HasData(
                new IdentityUserRole<int>
                {
                    RoleId = adminRoleId,
                    UserId = adminUserId
                }
            );
        }
    }
} 