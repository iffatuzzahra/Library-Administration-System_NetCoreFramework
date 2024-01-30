using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Winterhold.Presentation.Web.Services;
using Winterhold.Presentation.Web.ViewModels;

namespace Winterhold.Presentation.Web.Controllers.ApiControllers;

[Route("api/BookCategory")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly CategoryService _service;
    public CategoryController(CategoryService service)
    {
        _service = service;
    }
    [HttpGet("{name}")]
    public IActionResult GetById(string name)
    {
        var category = _service.GetCategoryByName(name);
        return Ok(category);
    }

    [HttpPost]
    public IActionResult Insert(CategoryUpsertViewModel vm)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _service.InsertCategory(vm);
            return Ok();

        }
        catch (Exception ex)
        {
            return UnprocessableEntity(ex.Message);
        }
    }

    [HttpPut]
    public IActionResult Update(CategoryUpsertViewModel vm)
    {
        if (!ModelState.IsValid
             && !ModelState.GetValidationState("Name").ToString().Equals("Invalid")
             )
        {
            return BadRequest(ModelState);
        }
        _service.UpdateCategory(vm);
        return Ok();
    }
}
