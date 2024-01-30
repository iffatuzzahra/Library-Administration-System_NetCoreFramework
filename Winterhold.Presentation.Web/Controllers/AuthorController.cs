using Microsoft.AspNetCore.Mvc;
using Winterhold.Presentation.Web.Services;
using Winterhold.Presentation.Web.ViewModels;

namespace Winterhold.Presentation.Web.Controllers;
[Route("Author")]
public class AuthorController : Controller
{
    private readonly ILogger<AuthorController> _logger;
    private readonly AuthorService _service;

    public AuthorController(ILogger<AuthorController> logger, AuthorService service)
    {
        _logger = logger;
        _service = service;
    }
    public IActionResult Index(string? name, int pageNumber = 1)
    {
        var vm = _service.GetAllAuthor(name, pageNumber, Constants.ITEMS_PER_PAGE);
        return View(vm);
    }

    [HttpGet("Insert")]
    public IActionResult Insert()
    {
        return View("Upsert");
    }
    [HttpPost("Insert")]
    public IActionResult Insert(AuthorUpsertViewModel vm)
    {
        if((!ModelState.IsValid &&  !ModelState.GetValidationState("Id").ToString().Equals("Invalid")) 
            || vm.Id != 0)
        {
            return View("Upsert", vm); 
        }

        _service.InsertAuthor(vm);
        return RedirectToAction("Index");
    }

    [HttpGet("Update/{id}")]
    public IActionResult Update(long id)
    {
        var vm = _service.GetUpsertVmByAuthorId(id);
        return View("Upsert", vm);
    }
    [HttpPost("Update/{id}")]
    public IActionResult Update(AuthorUpsertViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            return View("Upsert", vm);
        }

        _service.UpdateAuthor(vm);
        return RedirectToAction("Index");
    }

    [HttpGet("Delete/{id}")]
    public IActionResult Delete(long id)
    {
        _service.DeleteAuthor(id);
        return RedirectToAction("Index");
    }
    
    [HttpGet("Details/{id}")]
    public IActionResult Details(long id, int pageNumber = 1)
    {
        var vm = _service.GetAuthorDetails(id, pageNumber, Constants.ITEMS_PER_PAGE);
        return View(vm);
    }

}
