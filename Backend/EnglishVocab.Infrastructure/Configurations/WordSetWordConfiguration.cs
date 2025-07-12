using EnglishVocab.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EnglishVocab.Infrastructure.Configurations
{
    public class WordSetWordConfiguration : IEntityTypeConfiguration<WordSetWord>
    {
        public void Configure(EntityTypeBuilder<WordSetWord> builder)
        {
            // Primary key
            builder.HasKey(wsw => wsw.Id);

            // Relationships
            builder.HasOne(wsw => wsw.Word)
                .WithMany()
                .HasForeignKey(wsw => wsw.WordId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(wsw => wsw.WordSet)
                .WithMany()
                .HasForeignKey(wsw => wsw.WordSetId)
                .OnDelete(DeleteBehavior.Cascade);

            // Properties
            builder.Property(wsw => wsw.AddedDate)
                .HasDefaultValueSql("GETUTCDATE()");

            // Seed data
            builder.HasData(
                new WordSetWord { 
                    Id = 1, 
                    WordId = 1, 
                    WordSetId = 1, 
                    AddedDate = DateTime.Now,
                    CreatedBy = "System",
                    ModifiedBy = "System",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                },
                new WordSetWord { 
                    Id = 2, 
                    WordId = 2, 
                    WordSetId = 1, 
                    AddedDate = DateTime.Now,
                    CreatedBy = "System",
                    ModifiedBy = "System",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                },
                new WordSetWord { 
                    Id = 3, 
                    WordId = 3, 
                    WordSetId = 1, 
                    AddedDate = DateTime.Now,
                    CreatedBy = "System",
                    ModifiedBy = "System",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                }
            );
        }
    }
} 