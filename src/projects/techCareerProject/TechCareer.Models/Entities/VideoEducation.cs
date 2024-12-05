

using Core.Persistence.Repositories.Entities;
using System.Reflection.Metadata;
using TechCareer.Models.Entities.Enum;

namespace TechCareer.Models.Entities;

public class VideoEducation : Entity<int>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public double TotalHour { get; set; }
    public bool IsCertified { get; set; }
    public Level Level { get; set; }
    public string ImageUrl { get; set; }
    public Instructor Instructor { get; set; }
    public Guid InstructorId { get; set; }
    public string ProgrammingLanguage { get; set; }
}