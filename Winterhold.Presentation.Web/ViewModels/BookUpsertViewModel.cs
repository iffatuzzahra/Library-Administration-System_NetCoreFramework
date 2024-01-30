using Winterhold.Presentation.Web.Validations;

namespace Winterhold.Presentation.Web.ViewModels;

public class BookUpsertViewModel
{
    [UniqueBookCode]
    public string Code { get; set; } = null!;

    [UniqueBookTitle]
    public string Title { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public long AuthorId { get; set; }

    public string? Summary { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public int? TotalPage { get; set; }
}
