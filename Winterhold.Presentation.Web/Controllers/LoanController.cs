using Microsoft.AspNetCore.Mvc;
using Winterhold.Presentation.Web.Services;
using Winterhold.Presentation.Web.ViewModels;

namespace Winterhold.Presentation.Web.Controllers;
[ApiController]
[Route("Loan")]
public class LoanController : Controller
{
    private readonly LoanService _service;

    public LoanController(LoanService service)
    {
        _service = service;
    }

    public IActionResult Index(string? title, string? name, bool? isDuePass, int pageNumber = 1)
    {
        var vm = _service.GetAllLoans(title, name, isDuePass, pageNumber, Constants.ITEMS_PER_PAGE);
        return View(vm);
    }

    [HttpGet("Return/{id}")]
    public IActionResult ReturnLoanBook(long id)
    {
        _service.ReturnLoanBook(id);
        return RedirectToAction("Index");
    }
}
