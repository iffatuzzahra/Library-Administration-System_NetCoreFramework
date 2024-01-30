using System.ComponentModel.DataAnnotations;
using Winterhold.DataAccess.Models;

namespace Winterhold.Presentation.Web.Validations;

public class UniqueCustomerNumberAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var code = (string)value;
        var dbContext = (WinterholdContext)validationContext.GetService(typeof(WinterholdContext));
        var nameExists = dbContext.Customers.Any(c => c.MembershipNumber.Equals(code));
        if (nameExists)
        {
            return new ValidationResult("Customer Number Should Unique");
        }
        return ValidationResult.Success;
    }

}
