using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechCareer.Models.Dtos.VideoEducation;

namespace TechCareer.Service.Validations.VideoEducation;

public class CreateVideoEducationRequestDtoValidator : AbstractValidator<CreateVideoEducationRequestDto>
{
    public CreateVideoEducationRequestDtoValidator()
    {
        RuleFor(dto => dto.Title)
            .NotEmpty().WithMessage("Başlık boş olamaz.")
            .MaximumLength(100).WithMessage("Başlık en fazla 100 karakter olabilir.");

        RuleFor(dto => dto.Description)
            .NotEmpty().WithMessage("Açıklama boş olamaz.")
            .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");

        RuleFor(dto => dto.InstructorId)
            .NotEmpty().WithMessage("Eğitmen kimliği boş olamaz.")
            .NotEqual(Guid.Empty).WithMessage("Geçerli bir eğitmen kimliği girilmelidir.");

        RuleFor(dto => dto.Level)
            .IsInEnum().WithMessage("Geçerli bir seviye seçilmelidir.");

        RuleFor(dto => dto.TotalHour)
            .GreaterThan(0).WithMessage("Toplam saat sıfırdan büyük olmalıdır.")
            .LessThanOrEqualTo(100).WithMessage("Toplam saat en fazla 100 olabilir.");
    }
}