namespace Winterhold.Presentation.Web.ViewModels;

public class BookIndexTableViewModel
{
    public string Code { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string AuthorFirstName { get; set; } = null!;
    public string AuthorLastName { get; set; } = null!;
    public string AuthorTitle { get; set; } = null!;

    public string IsBorrowed { get; set; } = null!;

    public DateTime? ReleaseDate { get; set; }

    public int? TotalPage { get; set; }
    public string? Summary { get; set; }
    public string AuthorFullName
    {
        get
        {
            return $"{AuthorTitle}. {AuthorFirstName} {AuthorLastName}";
        }
    }
}
