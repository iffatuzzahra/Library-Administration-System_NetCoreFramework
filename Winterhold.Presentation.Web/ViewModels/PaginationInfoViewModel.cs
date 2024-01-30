namespace Winterhold.Presentation.Web.ViewModels;

public class PaginationInfoViewModel
{
    public int TotalItems { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public double TotalPagination {
        get {
            return Math.Ceiling((double) TotalItems / PageSize);
        } 
    }
}
