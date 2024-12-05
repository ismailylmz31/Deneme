using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCareer.Models.Dtos.Events;

namespace TechCareer.Service.Validations.Events
{
    public class CreateEventRequestValidator : AbstractValidator<CreateEventRequestDto>
    {
        public CreateEventRequestValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("Invalid category ID.");
        }
    }
}
