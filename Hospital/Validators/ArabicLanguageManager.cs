using FluentValidation.Resources;
using System.Globalization;

namespace Hospital.Validators;

public class ArabicLanguageManager : LanguageManager
{
    public ArabicLanguageManager()
    {
        Culture = new CultureInfo("ar");
        AddTranslation("ar", "NotEmptyValidator", "حقل '{PropertyName}' مطلوب.");
        AddTranslation("ar", "MaximumLengthValidator", "'{PropertyName}' يجب ألا يزيد عن {MaxLength} حرفًا.");
        AddTranslation("ar", "MinimumLengthValidator", "'{PropertyName}' يجب ألا يقل عن {MinLength} حرفًا.");
        AddTranslation("ar", "LengthValidator", "'{PropertyName}' يجب أن يكون طوله بين {MinLength} و {MaxLength} حرفًا.");
        AddTranslation("ar", "EmailValidator", "'{PropertyName}' ليس بريدًا إلكترونيًا صحيحًا.");
        AddTranslation("ar", "EqualValidator", "'{PropertyName}' يجب أن يكون مساويًا لـ '{ComparisonValue}'.");
        AddTranslation("ar", "ExactLengthValidator", "'{PropertyName}' يجب أن يكون طوله {MaxLength} حرفًا.");
        AddTranslation("ar", "InclusiveBetweenValidator", "'{PropertyName}' يجب أن يكون بين {From} و {To}.");
        AddTranslation("ar", "MatchesValidator", "'{PropertyName}' ليس بالتنسيق الصحيح.");
    }
}