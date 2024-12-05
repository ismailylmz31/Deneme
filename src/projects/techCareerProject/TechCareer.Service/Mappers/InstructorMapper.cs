using AutoMapper;
using Core.Security.Entities;
using TechCareer.Models.Dtos.Instructors;
using TechCareer.Models.Entities;

namespace TechCareer.Service.Mappers;

public class InstructorMapper : Profile
{
    public InstructorMapper()
    {
        CreateMap<CreateInstructorRequestDto, Instructor>();
        CreateMap<Instructor, InstructorResponseDto>();
        CreateMap<UpdateInstructorRequestDto, Instructor>();
    }
}
