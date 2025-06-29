using EnglishVocab.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection;

namespace EnglishVocab.Identity.Data.Dbcontext
{
    public class IdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // Apply all configurations from current assembly
            builder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);
            
            // Configure identity tables
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers");
            builder.Entity<ApplicationRole>().ToTable("AspNetRoles");
            builder.Entity<IdentityUserRole<int>>().ToTable("AspNetUserRoles");
            builder.Entity<IdentityUserClaim<int>>().ToTable("AspNetUserClaims");
            builder.Entity<IdentityUserLogin<int>>().ToTable("AspNetUserLogins");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("AspNetRoleClaims");
            builder.Entity<IdentityUserToken<int>>().ToTable("AspNetUserTokens");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .ConfigureWarnings(warnings =>
                    warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
    }
} 