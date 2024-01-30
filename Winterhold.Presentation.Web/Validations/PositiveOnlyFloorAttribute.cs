using System.ComponentModel.DataAnnotations;

namespace Winterhold.Presentation.Web.Validations;

public class PositiveOnlyFloorAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if ((int)value < 0)
        {
            return new ValidationResult("Floor Number cant be negative");
        } else if ((int)value == 0) 
        { 
            return new ValidationResult("Floor Number cant be Zero");
        } else
        {
            return ValidationResult.Success;
        }
    }
}
