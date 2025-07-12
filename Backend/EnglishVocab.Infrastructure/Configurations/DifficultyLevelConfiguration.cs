using EnglishVocab.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EnglishVocab.Infrastructure.Configurations
{
    public class DifficultyLevelConfiguration : IEntityTypeConfiguration<DifficultyLevel>
    {
        public void Configure(EntityTypeBuilder<DifficultyLevel> builder)
        {
            // Basic configurations
            builder.HasKey(d => d.Id);
            
            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(d => d.Description)
                .HasMaxLength(255);
                
            builder.Property(d => d.Value)
                .IsRequired();

            // Seed data for difficulty levels
            builder.HasData(
                new DifficultyLevel
                {
                    Id = 1,
                    Name = "Easy",
                    Description = "Words that are commonly used in everyday conversations",
                    Value = 1,
                    DateCreated = DateTime.Now,
                    CreatedBy = "System",
                    ModifiedBy = "System",
                    DateModified = DateTime.Now
                },
                new DifficultyLevel
                {
                    Id = 2,
                    Name = "Medium",
                    Description = "Words that are used in general contexts but less frequently",
                    Value = 2,
                    DateCreated = DateTime.Now,
                    CreatedBy = "System",
                    ModifiedBy = "System",
                    DateModified = DateTime.Now
                },
                new DifficultyLevel
                {
                    Id = 3,
                    Name = "Hard",
                    Description = "Words that are specialized or rarely used in everyday language",
                    Value = 3,
                    DateCreated = DateTime.Now,
                    CreatedBy = "System",
                    ModifiedBy = "System",
                    DateModified = DateTime.Now
                }
            );
        }
    }
} 