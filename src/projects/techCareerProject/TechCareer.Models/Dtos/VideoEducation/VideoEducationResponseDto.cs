using TechCareer.Models.Entities.Enum;

namespace TechCareer.Models.Dtos.VideoEducation;

public sealed record VideoEducationResponseDto
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public double TotalHour { get; init; }
    public Level Level { get; init; }
    public string ImageUrl { get; init; }
    public Guid InstructorId { get; init; }
    public string ProgrammingLanguage { get; init; }

}