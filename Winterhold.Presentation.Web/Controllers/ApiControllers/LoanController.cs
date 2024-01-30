using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Winterhold.Presentation.Web.Services;
using Winterhold.Presentation.Web.ViewModels;

namespace Winterhold.Presentation.Web.Controllers.ApiControllers;

[Route("api/Loan")]
[ApiController]
public class LoanController : ControllerBase
{
    private readonly LoanService _service;

    public LoanController(LoanService service)
    {
        _service = service;
    }

    [HttpGet("Insert")]
    public IActionResult GetVmInsert()
    {
        //var vm = _service.GetLoanUpsertVmById(id);
        var loanVm = new LoanUpsertIndexViewModel
        {
            Customers = _service.GetNonExpiredCustomerList(),
            Books = _service.GetAvailableBookList()
        };
        return Ok(loanVm);
    }
    [HttpGet("Update/{id}")]
    public IActionResult GetVmUpdateById(long id)
    {
        var loanVm = _service.GetLoanUpsertVmById(id);
        //loanVm.Loan.LoanDate.ToShortDateString();
        return Ok(loanVm);
    }
    [HttpPost]
    public IActionResult Insert(LoanUpsertViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _service.InsertLoan(vm);
        return Ok();
    }
    [HttpPut]
    public IActionResult Update(LoanUpsertViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _service.UpdateLoan(vm);
        return Ok();
    }

    [HttpGet("Detail/{id}")]
    public IActionResult Detail(long id)
    {
        var vm = _service.GetLoanDetail(id);
        return Ok(vm);
    }
}
