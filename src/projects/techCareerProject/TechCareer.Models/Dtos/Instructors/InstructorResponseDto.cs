namespace TechCareer.Models.Dtos.Instructors;

public sealed record InstructorResponseDto
{
    public string Name { get; init; }
    public string About { get; init; }
    public Guid Id { get; set; }
}