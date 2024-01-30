using System.ComponentModel.DataAnnotations;
using Winterhold.Presentation.Web.Validations;

namespace Winterhold.Presentation.Web.ViewModels;

public class CategoryUpsertViewModel
{
    //[UniqueCategoryName]
    public string Name { get; set; } = null!;

    [PositiveOnlyFloor]
    public int Floor { get; set; }

    public string Isle { get; set; } = null!;

    public string Bay { get; set; } = null!;
}
