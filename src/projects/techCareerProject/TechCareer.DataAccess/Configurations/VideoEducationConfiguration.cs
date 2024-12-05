using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TechCareer.Models.Entities;

public class VideoEducationConfiguration : IEntityTypeConfiguration<VideoEducation>
{
    public void Configure(EntityTypeBuilder<VideoEducation> builder)
    {
        builder.ToTable("VideoEducations");
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Id).HasColumnName("VideoEducationId");
        builder.Property(v => v.Title).HasColumnName("Title").IsRequired().HasMaxLength(200);
        builder.Property(v => v.Description).HasColumnName("Description").HasMaxLength(1000);
        builder.Property(v => v.TotalHour).HasColumnName("TotalHour");
        builder.Property(v => v.IsCertified).HasColumnName("IsCertified");
        builder.Property(v => v.Level).HasColumnName("Level").HasConversion<int>();
        builder.Property(v => v.ImageUrl).HasColumnName("ImageUrl").HasMaxLength(500);
        builder.Property(v => v.InstructorId).HasColumnName("InstructorId").IsRequired();
        builder.Property(v => v.ProgrammingLanguage).HasColumnName("ProgrammingLanguage").HasMaxLength(100);

        builder.HasOne(v => v.Instructor)
               .WithMany(i => i.VideoEducations)
               .HasForeignKey(v => v.InstructorId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property(v => v.CreatedDate)
               .HasColumnName("CreatedDate")
               .IsRequired();
    }
}
