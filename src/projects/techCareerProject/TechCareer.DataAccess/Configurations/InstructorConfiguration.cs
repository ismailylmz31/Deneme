using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TechCareer.Models.Entities;

public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder.ToTable("Instructors").HasKey(i => i.Id);

        builder.Property(i => i.Id).HasColumnName("Id").IsRequired();
        builder.Property(i => i.Name).HasColumnName("Name").IsRequired().HasMaxLength(100);
        builder.Property(i => i.About).HasColumnName("About").IsRequired().HasMaxLength(500);
        builder.Property(i => i.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(i => i.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(i => i.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(i => !i.DeletedDate.HasValue);

        builder.HasMany(i => i.VideoEducations)
               .WithOne(v => v.Instructor)
               .HasForeignKey(v => v.InstructorId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
