 

namespace Winterhold.Presentation.Web.ViewModels;

public class CategoryIndexViewModel
{
    public string Name { get; set; } = null!;
    public List<CategoryIndexTableViewModel>? CategoriesSummary { get; set; }
    public PaginationInfoViewModel PaginationInfo { get; set; }
}
