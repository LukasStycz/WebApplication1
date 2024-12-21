using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Application.Validation
{
    public class FutureDateAttribute(Type? appliesToType = null) : ValidationAttribute
    {
        private readonly Type? _appliesToType = appliesToType;
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (_appliesToType != null && validationContext.ObjectInstance.GetType() != _appliesToType)
            {
                return ValidationResult.Success;
            }
            if (value is DateTime dateTime )
            {
                if (dateTime <= DateTime.Now)
                return new ValidationResult(ErrorMessage ?? "The date must be in the future.", [validationContext.MemberName!]);
            }
            return ValidationResult.Success;
        }
    }
}