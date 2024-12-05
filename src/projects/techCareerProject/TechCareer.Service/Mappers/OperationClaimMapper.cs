using AutoMapper;
using Core.Persistence.Extensions;
using Core.Security.Entities;
using TechCareer.Models.Dtos.Roles;

namespace TechCareer.Service.Mappers;

public class OperationClaimMapper : Profile
{
    public OperationClaimMapper()
    {
        CreateMap<OperationClaim, OperationClaimResponseDto>();
        CreateMap<OperationClaimAddRequestDto, OperationClaim>();
        CreateMap<Paginate<OperationClaim>, Paginate<OperationClaimResponseDto>>();
    }
}