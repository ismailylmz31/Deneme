using Core.Persistence.Repositories.Entities;

namespace TechCareer.Models.Entities;

public class Instructor : Entity<Guid>
{
    public string Name { get; set; }
    public string About { get; set; }
    public ICollection<VideoEducation>? VideoEducations { get; set; }
}




