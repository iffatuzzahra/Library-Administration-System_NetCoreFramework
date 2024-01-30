using Microsoft.AspNetCore.Mvc.Rendering;

namespace Winterhold.Presentation.Web.ViewModels;

public class BookUpsertIndexViewModel
{
    public BookUpsertViewModel Book { get; set; }

    public List<SelectListItem>? Authors { get; set; }
}
