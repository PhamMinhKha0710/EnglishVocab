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

            // Relationships without navigation properties
            builder.HasOne<Word>()
                .WithMany()
                .HasForeignKey(wsw => wsw.WordId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<WordSet>()
                .WithMany()
                .HasForeignKey(wsw => wsw.WordSetId)
                .OnDelete(DeleteBehavior.Cascade);

            // Properties
            builder.Property(wsw => wsw.AddedDate)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
} 