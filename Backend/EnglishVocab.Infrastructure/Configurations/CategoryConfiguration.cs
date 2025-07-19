using EnglishVocab.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EnglishVocab.Infrastructure.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            
            // Vô hiệu hóa tính năng tự tăng cho cột ID
            builder.Property(c => c.Id)
                .ValueGeneratedNever();
            
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
                
            builder.Property(c => c.Description)
                .HasMaxLength(500);
            
            // Seed data cho Categories - cần có cho WordConfiguration
            builder.HasData(
                new Category
                {
                    Id = 1,
                    Name = "Greetings",
                    Description = "Common greetings and introductions",
                    DateCreated = DateTime.Now,
                    CreatedBy = "System",
                    ModifiedBy = "System",
                    DateModified = DateTime.Now
                },
                new Category
                {
                    Id = 2,
                    Name = "Expressions",
                    Description = "Common expressions and phrases",
                    DateCreated = DateTime.Now,
                    CreatedBy = "System",
                    ModifiedBy = "System",
                    DateModified = DateTime.Now
                }
            );
        }
    }
} 