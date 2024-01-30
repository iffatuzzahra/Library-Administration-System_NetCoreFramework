using Microsoft.AspNetCore.Mvc;
using Winterhold.Presentation.Web.Services;
using Winterhold.Presentation.Web.ViewModels;

namespace Winterhold.Presentation.Web.Controllers;
[Route("Customer")]
public class CustomerController : Controller
{
    private readonly CustomerService _service;

    public CustomerController(CustomerService service)
    {
        _service = service;
    }

    public IActionResult Index(string? number, string? name, bool? isExpired, int pageNumber = 1)
    {
        var vm = _service.GetAllCustomer(number, name, isExpired, pageNumber, Constants.ITEMS_PER_PAGE);
        return View(vm);
    }

    [HttpGet("Insert")]
    public IActionResult Insert()
    {
        var vm = new CustomerUpsertViewModel();
        return View("Upsert", vm);
    }
    [HttpPost("Insert")]
    public IActionResult Insert(CustomerUpsertViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            vm.MembershipNumber = null;
            return View("Upsert", vm);
        }
        _service.InsertCustomer(vm);
        return RedirectToAction("Index");
    }

    [HttpGet("Update/{number}")]
    public IActionResult Update(string number)
    {
        var vm = _service.GetCustomerUpsertVmByNumber(number);
        return View("Upsert", vm);
    }
    [HttpPost("Update/{number}")]
    public IActionResult Update(CustomerUpsertViewModel vm)
    {
        if (!ModelState.IsValid && !ModelState.GetValidationState("MembershipNumber").ToString().Equals("Invalid"))
        {
            return View("Upsert", vm);
        }
        _service.UpdateCustomer(vm);
        return RedirectToAction("Index");
    }

    [HttpGet("Delete/{number}")]
    public IActionResult Delete(string number)
    {
        _service.DeleteCustomer(number);
        return RedirectToAction("Index");
    }

    [HttpGet("ExtendMembership/{number}")]
    public IActionResult ExtendMembership(string number)
    {
        _service.ExtendMembershipDate(number);
        return RedirectToAction("Index");
    }

   
}
