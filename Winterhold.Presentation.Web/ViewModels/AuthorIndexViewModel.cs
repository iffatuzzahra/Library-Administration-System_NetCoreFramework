 

namespace Winterhold.Presentation.Web.ViewModels;

public class AuthorIndexViewModel
{
    public List<AuthorIndexTableViewModel> Authors { get; set; }
    public string Name { get; set; }
    public PaginationInfoViewModel PaginationInfo { get; set; }
}
