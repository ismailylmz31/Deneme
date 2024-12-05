using TechCareer.Models.Entities.Enum;

namespace TechCareer.Models.Dtos.VideoEducation;

public class CreateVideoEducationRequestDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public double TotalHour { get; set; }
    public bool IsCertified { get; set; }
    public Level Level { get; set; }
    public string ImageUrl { get; set; }
    public Guid InstructorId { get; set; }
    public string ProgrammingLanguage { get; set; }


};