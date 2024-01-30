namespace Winterhold.Presentation.Web.ViewModels;

public class LoanUpsertViewModel
{
    public long Id { get; set; }

    public string CustomerNumber { get; set; } = null!;

    public string BookCode { get; set; } = null!;

    public DateTime LoanDate { get; set; }

    public string? Note { get; set; }
}
