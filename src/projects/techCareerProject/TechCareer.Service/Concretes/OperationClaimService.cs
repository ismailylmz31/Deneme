using AutoMapper;
using Core.AOP.Aspects;
using Core.Security.Entities;
using TechCareer.DataAccess.Repositories.Abstracts;
using TechCareer.Models.Dtos.Roles;
using TechCareer.Service.Abstracts;
using TechCareer.Service.Constants;
using TechCareer.Service.Rules;
using TechCareer.Service.Validations.OperationClaims;

namespace TechCareer.Service.Concretes;

public class OperationClaimService(
    IOperationClaimRepository operationClaimRepository, 
    IMapper mapper,
    OperationClaimBusinessRules businessRules
    ) : IOperationClaimService
{

    [AuthorizeAspect(roles:"Admin")]
    [LoggerAspect]
    [ClearCacheAspect("OperationClaims")]
    [ValidationAspect(typeof(OperationClaimAddValidator))]
    public async Task<OperationClaimResponseDto> AddAsync(OperationClaimAddRequestDto dto,CancellationToken cancellationToken)
    {
        await businessRules.RoleNameIsUnique(dto.Name,cancellationToken);
        var operationClaim = mapper.Map<OperationClaim>(dto);

        var addedOperationClaim = await operationClaimRepository.AddAsync(operationClaim);

        var response = mapper.Map<OperationClaimResponseDto>(addedOperationClaim);

        return response;
    }
    [ClearCacheAspect("OperationClaims")]
    [AuthorizeAspect(roles:"Admin")]
    [LoggerAspect]
    public async Task<string> DeleteAsync(int id,CancellationToken cancellationToken)
    {
        await businessRules.RoleIsPresentCheck(id,cancellationToken);

        var operationClaim = await operationClaimRepository.GetAsync(x=>x.Id==id);

        await operationClaimRepository.DeleteAsync(operationClaim!);

        return RoleMessages.RoleDeleted;
    }
    
    [CacheAspect(cacheKeyTemplate:"OperationClaimsList",bypassCache:false,cacheGroupKey:"OperationClaims")]
    public async Task<List<OperationClaimResponseDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var list = await operationClaimRepository.GetListAsync(include:false,enableTracking:false,cancellationToken:cancellationToken);
        var response = mapper.Map<List<OperationClaimResponseDto>>(list);

        return response;
    }

    public async Task<OperationClaimResponseDto> GetByIdAsync(int id,CancellationToken cancellationToken)
    {
        await businessRules.RoleIsPresentCheck(id,cancellationToken);
        
        var operationClaim = await operationClaimRepository.GetAsync(x=>x.Id==id);
        
        var response = mapper.Map<OperationClaimResponseDto>(operationClaim);

        return response;
    }
}
