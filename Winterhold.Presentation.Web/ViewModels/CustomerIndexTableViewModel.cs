namespace Winterhold.Presentation.Web.ViewModels;

public class CustomerIndexTableViewModel
{
    public string MembershipNumber { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public DateTime MembershipExpireDate { get; set; }
    public string FullName
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }
}
