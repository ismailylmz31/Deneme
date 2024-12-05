using FluentValidation;
using TechCareer.Models.Dtos.Instructors;

namespace TechCareer.Service.Validations.Instructors
{
    public class CreateInstructorRequestValidator : AbstractValidator<CreateInstructorRequestDto>
    {
        public CreateInstructorRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.About)
                .NotEmpty().WithMessage("About information is required.")
                .MaximumLength(500).WithMessage("About information cannot exceed 500 characters.");
        }
    }
}
