using EnglishVocab.Constants.Constant;
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
            builder.HasKey(d => d.Id);
            
            // Vô hiệu hóa tính năng tự tăng cho cột ID
            builder.Property(d => d.Id)
                .ValueGeneratedNever();
            
            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(d => d.Description)
                .HasMaxLength(200);
                
            builder.Property(d => d.Value)
                .IsRequired();

            
            // Seed data cho DifficultyLevels - cần có cho WordConfiguration
            builder.HasData(
                new DifficultyLevel
                {
                    Id = (int)DifficultyLevelType.Easy,
                    Name = "Easy",
                    Description = "Basic vocabulary for beginners",
                    Value = (int)DifficultyLevelType.Easy,
                    DateCreated = DateTime.Now,
                    CreatedBy = "System",
                    ModifiedBy = "System",
                    DateModified = DateTime.Now
                },
                new DifficultyLevel
                {
                    Id = (int)DifficultyLevelType.Medium,
                    Name = "Medium",
                    Description = "Intermediate vocabulary",
                    Value = (int)DifficultyLevelType.Medium,
                    DateCreated = DateTime.Now,
                    CreatedBy = "System",
                    ModifiedBy = "System",
                    DateModified = DateTime.Now
                },
                new DifficultyLevel
                {
                    Id = (int)DifficultyLevelType.Hard,
                    Name = "Hard",
                    Description = "Advanced vocabulary",
                    Value = (int)DifficultyLevelType.Hard,
                    DateCreated = DateTime.Now,
                    CreatedBy = "System",
                    ModifiedBy = "System",
                    DateModified = DateTime.Now
                },
                new DifficultyLevel
                {
                    Id = (int)DifficultyLevelType.VeryHard,
                    Name = "Very Hard",
                    Description = "Expert level vocabulary",
                    Value = (int)DifficultyLevelType.VeryHard,
                    DateCreated = DateTime.Now,
                    CreatedBy = "System",
                    ModifiedBy = "System",
                    DateModified = DateTime.Now
                }
            );
        }
    }
} 