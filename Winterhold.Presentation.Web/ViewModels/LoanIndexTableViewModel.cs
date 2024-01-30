namespace Winterhold.Presentation.Web.ViewModels;

public class LoanIndexTableViewModel
{
    public long Id { get; set; }

    public string CustomerFirstName { get; set; } = null!;

    public string? CustomerLastName { get; set; }

    public string BookTitle { get; set; } = null!;

    public DateTime LoanDate { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public string CustomerName { 
        get {
            return $"{CustomerFirstName} {CustomerLastName}";
        } }
}
