namespace Winterhold.Presentation.Web.ViewModels;

public class AuthorBooksTableViewModel
{
    public string Title { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public string IsBorrowed { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public int? TotalPage { get; set; }
}
