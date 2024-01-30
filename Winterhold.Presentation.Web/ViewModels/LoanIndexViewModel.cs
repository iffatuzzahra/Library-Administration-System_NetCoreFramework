 

namespace Winterhold.Presentation.Web.ViewModels;

public class LoanIndexViewModel
{
    public List<LoanIndexTableViewModel> Loans { get; set; }
    public string Title { get; set; }
    public string Name { get; set; }
    public bool? IsDuePass { get; set; } = null;
    public PaginationInfoViewModel PaginationInfo { get; set; }
}
