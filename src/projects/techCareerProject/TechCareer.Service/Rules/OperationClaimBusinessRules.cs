using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using Core.CrossCuttingConcerns.Rules;
using TechCareer.DataAccess.Repositories.Abstracts;
using TechCareer.Service.Constants;

namespace TechCareer.Service.Rules;

public class OperationClaimBusinessRules(IOperationClaimRepository operationClaimRepository) : BaseBusinessRules
{
    public async Task RoleNameIsUnique(string name,CancellationToken cancellationToken)
    {
        var isPresent = await operationClaimRepository.AnyAsync(predicate: x => x.Name.Equals(name),
            enableTracking:false,
            cancellationToken: cancellationToken);

        if (isPresent)
        {
            throw new BusinessException(RoleMessages.RoleNameMustBeUnique);
        }
    }



    public async Task RoleIsPresentCheck(int id,CancellationToken cancellationToken)
    {
        var isPresent = await operationClaimRepository.AnyAsync(predicate: x => x.Id == id,
            enableTracking:false,
            cancellationToken:cancellationToken);
        if (!isPresent)
        {
            throw new BusinessException(RoleMessages.RoleNotFound);
        }
        
    }

}
