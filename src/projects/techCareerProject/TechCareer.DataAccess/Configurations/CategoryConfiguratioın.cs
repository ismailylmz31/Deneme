using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechCareer.Models.Entities;


public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        // Primary key tanımı
        builder.HasKey(c => c.Id);

        // Name alanı için özellikler
        builder.Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(100);

        // Bir kategori birden fazla event'e sahip olabilir
        builder.HasMany(c => c.Events)
               .WithOne(e => e.Category)
               .HasForeignKey(e => e.CategoryId)
               .OnDelete(DeleteBehavior.Cascade); // Cascade delete davranışı
    }
}
