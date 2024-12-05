

using AutoMapper;
using TechCareer.Models.Dtos.VideoEducation;
using TechCareer.Models.Entities;

namespace TechCareer.Service.Mappers;

public class VideoEducationMapper : Profile
{
    public VideoEducationMapper()
    {
        CreateMap<CreateVideoEducationRequestDto, VideoEducation>();
        CreateMap<VideoEducation, VideoEducationResponseDto>();
        CreateMap<UpdateVideoEducationRequestDto, VideoEducation>();
    }
}
