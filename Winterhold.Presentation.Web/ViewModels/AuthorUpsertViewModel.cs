namespace Winterhold.Presentation.Web.ViewModels;

public class AuthorUpsertViewModel
{
    public long Id { get; set; }

    public string Title { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public DateTime BirthDate { get; set; }

    public DateTime? DeceasedDate { get; set; }

    public string? Education { get; set; }

    public string? Summary { get; set; }
}
