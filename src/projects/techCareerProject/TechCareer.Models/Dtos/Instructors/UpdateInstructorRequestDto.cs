namespace TechCareer.Models.Dtos.Instructors;

public sealed record UpdateInstructorRequestDto(Guid Id, string Name, string About);