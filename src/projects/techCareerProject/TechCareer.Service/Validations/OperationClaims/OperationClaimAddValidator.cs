using FluentValidation;
using TechCareer.Models.Dtos.Roles;
using TechCareer.Service.Constants;

namespace TechCareer.Service.Validations.OperationClaims;

public class OperationClaimAddValidator : AbstractValidator<OperationClaimAddRequestDto>
{
    public OperationClaimAddValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage(RoleMessages.RoleNameNotBeEmpty)
            .MinimumLength(2).WithMessage(RoleMessages.RoleNameMustBeMinRangeMessage);

    }
}