using System.ComponentModel.DataAnnotations;
using Winterhold.DataAccess.Models;

namespace Winterhold.Presentation.Web.Validations;

public class UniqueCategoryNameAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var name = (string)value;
        var dbContext = (WinterholdContext)validationContext.GetService(typeof(WinterholdContext));
        var nameExists = dbContext.Categories.Any(c => c.Name.Equals(name));
        if (nameExists)
        {
            return new ValidationResult("Category Name Should Unique");
        }
        return ValidationResult.Success;
    }

}
