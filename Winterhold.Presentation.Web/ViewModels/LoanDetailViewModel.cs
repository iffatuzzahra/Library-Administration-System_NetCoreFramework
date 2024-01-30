namespace Winterhold.Presentation.Web.ViewModels;

public class LoanDetailViewModel
{
    public string MembershipNumber { get; set; } = null!;

    public string MemberFullName { get; set; } = null!;

    public string? MemberPhone { get; set; }

    public DateTime MembershipExpireDate { get; set; }

    public string BookTitle { get; set; } = null!;

    public string BookAuthorFullName { get; set; } = null!;
    public string BookCategory { get; set; } = null!;

    public int CategoryFloor { get; set; }

    public string CategoryIsle { get; set; } = null!;

    public string CategoryBay { get; set; } = null!;
}
