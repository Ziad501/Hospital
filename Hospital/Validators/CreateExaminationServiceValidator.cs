using FluentValidation;
using Hospital.DTOs.ExaminationDtos;

namespace Hospital.Validators;

public class CreateExaminationServiceValidator : AbstractValidator<CreateExaminationServiceDto>
{
    public CreateExaminationServiceValidator()
    {
        RuleFor(x => x.ServiceCode)
            .NotEmpty().WithMessage("Service code is required");

        RuleFor(x => x.ServiceName)
            .NotEmpty().WithMessage("Service name is required");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Service price must be greater than 0");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0");
    }
}