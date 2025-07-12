using EnglishVocab.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishVocab.Infrastructure.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Description)
                .HasMaxLength(200);

            builder.Property(c => c.DateCreated)
                .IsRequired();

            builder.Property(c => c.DateModified);

            // Create a unique index on the Name column
            builder.HasIndex(c => c.Name)
                .IsUnique();

            // Define the relationship with Words
            builder.HasMany(c => c.Words)
                .WithOne()
                .HasForeignKey("CategoryId")
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
} 