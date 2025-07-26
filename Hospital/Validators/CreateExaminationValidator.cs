using FluentValidation;
using Hospital.DTOs.ExaminationDtos;

namespace Hospital.Validators;

public class CreateExaminationValidator : AbstractValidator<CreateExaminationDto>
{
    public CreateExaminationValidator()
    {
        RuleFor(x => x.PatientId)
            .GreaterThan(0).WithMessage("Patient ID is required");

        RuleFor(x => x.ClinicCode)
            .NotEmpty().WithMessage("Clinic is required");

        RuleFor(x => x.DoctorCode)
            .NotEmpty().WithMessage("Doctor is required");

        RuleFor(x => x.Amount)
            .GreaterThanOrEqualTo(0).WithMessage("Amount cannot be negative");

        RuleFor(x => x.Discount)
            .GreaterThanOrEqualTo(0).WithMessage("Discount cannot be negative");

        RuleFor(x => x.Services)
            .NotEmpty().WithMessage("At least one service is required");

        RuleForEach(x => x.Services).SetValidator(new CreateExaminationServiceValidator());
    }
}