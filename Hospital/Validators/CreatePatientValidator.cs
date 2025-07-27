using FluentValidation;
using Hospital.DTOs.PatientDtos;

namespace Hospital.Validators;

public class CreatePatientValidator : AbstractValidator<CreatePatientDto>
{
    public CreatePatientValidator()
    {
        // تم تخصيص رسالة لكل حقل مطلوب
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("الاسم الأول مطلوب")
            .MaximumLength(50).WithMessage("الاسم الأول لا يمكن أن يتجاوز 50 حرفًا");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("الاسم الأخير مطلوب")
            .MaximumLength(50).WithMessage("الاسم الأخير لا يمكن أن يتجاوز 50 حرفًا");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("كود المريض مطلوب")
            .MaximumLength(20).WithMessage("كود المريض لا يمكن أن يتجاوز 20 حرفًا");

        RuleFor(x => x.Mobile)
            .NotEmpty().WithMessage("رقم الموبايل مطلوب")
            .Matches(@"^01[0-9]{9}$").WithMessage("صيغة رقم الموبايل المصري غير صحيحة");

        RuleFor(x => x.NationalId)
            .NotEmpty().WithMessage("الرقم القومي مطلوب")
            .Length(14).WithMessage("الرقم القومي يجب أن يكون 14 رقمًا")
            .Matches(@"^\d{14}$").WithMessage("الرقم القومي يجب أن يحتوي على أرقام فقط");

        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("تاريخ الميلاد مطلوب")
            .LessThan(DateTime.Today).WithMessage("تاريخ الميلاد لا يمكن أن يكون في المستقبل");

        RuleFor(x => x.Gender)
            .NotEmpty().WithMessage("النوع مطلوب")
            .Must(x => x == "ذكر" || x == "أنثى" || x == "انثى").WithMessage("النوع يجب أن يكون 'ذكر' أو 'أنثى'");

        RuleFor(x => x.Nationality)
            .NotEmpty().WithMessage("الجنسية مطلوبة");

        RuleFor(x => x.PatientType)
            .NotEmpty().WithMessage("نوع المريض مطلوب");
    }
}
