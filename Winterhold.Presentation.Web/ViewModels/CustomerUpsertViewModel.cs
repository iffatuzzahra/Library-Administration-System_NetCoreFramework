using Winterhold.Presentation.Web.Validations;

namespace Winterhold.Presentation.Web.ViewModels;

public class CustomerUpsertViewModel
{
    [UniqueCustomerNumber]
    public string MembershipNumber { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public DateTime BirthDate { get; set; } = DateTime.Now;

    public string Gender { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

}
