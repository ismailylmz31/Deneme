using TechCareer.Models.Dtos.Roles;

namespace TechCareer.Service.Abstracts;

public interface IOperationClaimService
{
    Task<OperationClaimResponseDto> GetByIdAsync(int id,CancellationToken cancellationToken);
    Task<List<OperationClaimResponseDto>> GetAllAsync(CancellationToken cancellationToken);

    Task<OperationClaimResponseDto> AddAsync(OperationClaimAddRequestDto dto,CancellationToken cancellationToken);

    Task<string> DeleteAsync(int id,CancellationToken cancellationToken);

}
