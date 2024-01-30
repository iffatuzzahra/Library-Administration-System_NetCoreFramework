using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Winterhold.Presentation.Web.Services;

namespace Winterhold.Presentation.Web.Controllers.ApiControllers;

[Route("api/Customer")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly CustomerService _service;

    public CustomerController(CustomerService service)
    {
        _service = service;
    }
    [HttpGet("{number}")]
    public IActionResult GetCustomerPopupData(string number)
    {
        var vm = _service.GetCustomerPopupData(number);
        return Ok(vm);
    }
}
