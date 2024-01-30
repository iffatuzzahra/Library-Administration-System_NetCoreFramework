 
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Winterhold.Presentation.Web.ViewModels;

public class BookIndexViewModel
{
    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;
    public bool? IsAvailable { get; set; } = null;
    public string CategoryName { get; set; } = null!;
    public List<BookIndexTableViewModel>? Books { get; set; }
    public PaginationInfoViewModel PaginationInfo { get; set; }
    public List<SelectListItem> AvailableItems { get; } = new List<SelectListItem> {
        new SelectListItem {
            Value = "",
            Text = "All"
        },
        new SelectListItem
        {
            Value = "true",
            Text = "Available Book"
        },
        new SelectListItem
        {
            Value = "false",
            Text = "Not Available Book"
        }
    };
}
