using Microsoft.AspNetCore.Mvc;
using Winterhold.Presentation.Web.Services;
using Winterhold.Presentation.Web.ViewModels;

namespace Winterhold.Presentation.Web.Controllers;
[Route("Book")]
public class BookController : Controller
{
    private readonly BookService _service;
    public BookController(BookService service)
    {
        _service = service;
    }

    [HttpGet("Index/{name}")]
    public IActionResult Index(string? title, string? author, bool? isAvailable, string name, int pageNumber = 1)
    {
        var vm = _service.GetBookByCategory(title, author, isAvailable, name, pageNumber, Constants.ITEMS_PER_PAGE);
        return View(vm);
    }
    [HttpGet("Insert/{name}")]
    public IActionResult Insert(string name)
    {
        var vm = new BookUpsertIndexViewModel
        {
            Authors = _service.GetAuthorList(),
            Book = new BookUpsertViewModel
            {
                CategoryName = name
            }
        };
        return View("Upsert", vm);
    }
    [HttpPost("Insert/{name}")]
    public IActionResult Insert(BookUpsertIndexViewModel vm, string name)
    {
        if(!ModelState.IsValid)
        {
            //if(ModelState.GetValidationState("Book.Code").ToString().Equals("Invalid"))
            vm.Book.Code = null ;
            vm.Authors = _service.GetAuthorList();
            return View("Upsert", vm);
        }
        _service.InsertBook(vm.Book);
        return RedirectToAction("Index", new { name });
    }

    [HttpGet("Update/{code}")]
    public IActionResult Update(string code)
    {
        var vm = _service.GetBookUpsertVmByCode(code);
        return View("Upsert", vm);
    }
    [HttpPost("Update/{code}")]
    public IActionResult Update(BookUpsertIndexViewModel vm, string code)
    {
        if (!ModelState.IsValid
             && !ModelState.GetValidationState("Book.Code").ToString().Equals("Invalid")
           )
        {
            vm.Authors = _service.GetAuthorList();
            return View("Upsert", vm);
        }
        _service.UpdateBook(vm.Book);
        return RedirectToAction("Index", new { name = vm.Book.CategoryName });
    }
    [HttpGet("Delete/{code}")]
    public IActionResult Delete(string code, string name)
    {
        _service.DeleteBook(code);
        return RedirectToAction("Index", new { name });
    }
}
