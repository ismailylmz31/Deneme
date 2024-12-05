


namespace TechCareer.Models.Dtos.VideoEducation;

public class UpdateVideoEducationRequestDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid InstructorId { get; set; }
    public double TotalHour { get; set; }
}
