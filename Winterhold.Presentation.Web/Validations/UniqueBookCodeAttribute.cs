using System.ComponentModel.DataAnnotations;
using Winterhold.DataAccess.Models;

namespace Winterhold.Presentation.Web.Validations;

public class UniqueBookCodeAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var code = (string)value;
        var dbContext = (WinterholdContext)validationContext.GetService(typeof(WinterholdContext));
        var nameExists = dbContext.Books.Any(c => c.Code.Equals(code));
        if (nameExists)
        {
            return new ValidationResult("Book Code Should Unique");
        }
        return ValidationResult.Success;
    }

}
