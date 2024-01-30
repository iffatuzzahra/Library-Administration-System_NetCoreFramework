 

namespace Winterhold.Presentation.Web.ViewModels;

public class CustomerIndexViewModel
{
    public List<CustomerIndexTableViewModel>? Customers { get; set; }

    public string Number { get; set; }
    public string Name { get; set; }
    public bool? IsExpired { get; set; } = null;
    public PaginationInfoViewModel PaginationInfo { get; set; }
}
