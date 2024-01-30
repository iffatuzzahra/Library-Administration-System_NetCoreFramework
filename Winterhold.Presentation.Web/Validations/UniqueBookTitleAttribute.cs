using System.ComponentModel.DataAnnotations;
using Winterhold.DataAccess.Models;
using Winterhold.Presentation.Web.ViewModels;

namespace Winterhold.Presentation.Web.Validations;

public class UniqueBookTitleAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var book = (BookUpsertViewModel)validationContext.ObjectInstance;

        var dbContext = (WinterholdContext)validationContext.GetService(typeof(WinterholdContext));
        var nameExists = dbContext.Books.Any(c => c.Title.Equals(book.Title));
        if (nameExists)
        {
            
            if (book.Code != null 
                && dbContext.Books.Find(book.Code).Title.Equals(book.Title))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Book Name Should Unique");
        }
        return ValidationResult.Success;
    }

}
