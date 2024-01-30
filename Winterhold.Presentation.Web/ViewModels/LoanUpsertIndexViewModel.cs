using Microsoft.AspNetCore.Mvc.Rendering;

namespace Winterhold.Presentation.Web.ViewModels;

public class LoanUpsertIndexViewModel
{
    public LoanUpsertViewModel Loan { get; set; }
    public List<SelectListItem>? Customers { get; set; }
    public List<SelectListItem>? Books { get; set; }
    public string CustomerName { get; set; }
    public string BookTitle { get; set; }
}
