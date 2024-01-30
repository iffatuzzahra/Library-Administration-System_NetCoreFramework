using Microsoft.AspNetCore.Mvc;
using Winterhold.Presentation.Web.Services;
using Winterhold.Presentation.Web.Validations;
using Winterhold.Presentation.Web.ViewModels;

namespace Winterhold.Presentation.Web.Controllers;
[Route("BookCategory")]
[ApiController]
public class CategoryController : Controller
{
    private readonly CategoryService _service;

    public CategoryController(CategoryService service)
    {
        _service = service;
    }

    public IActionResult Index(string? name, int pageNumber = 1)
    {
        var vm = _service.GetAllCategory(name, pageNumber, Constants.ITEMS_PER_PAGE);
        return View(vm);
    }

    [HttpGet("Delete/{name}")]
    public IActionResult Delete(string name)
    {
        _service.DeleteCategory(name);
        return RedirectToAction("Index");
    }
}
