using FluentValidation;
using Hospital.DTOs.PatientDtos;

namespace Hospital.Validators;

public class CreatePatientValidator : AbstractValidator<CreatePatientDto>
{
    public CreatePatientValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Patient code is required")
            .MaximumLength(20).WithMessage("Patient code cannot exceed 20 characters");

        RuleFor(x => x.Mobile)
            .NotEmpty().WithMessage("Mobile number is required")
            .Matches(@"^01[0-9]{9}$").WithMessage("Invalid Egyptian mobile number format");

        RuleFor(x => x.NationalId)
            .NotEmpty().WithMessage("National ID is required")
            .Length(14).WithMessage("National ID must be 14 digits")
            .Matches(@"^\d{14}$").WithMessage("National ID must contain only digits");

        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("Birth date is required")
            .LessThan(DateTime.Today).WithMessage("Birth date cannot be in the future");

        RuleFor(x => x.Gender)
            .NotEmpty().WithMessage("Gender is required")
            .Must(x => x == "ذكر" || x == "أنثى").WithMessage("Gender must be either 'ذكر' or 'أنثى'");
    }
}