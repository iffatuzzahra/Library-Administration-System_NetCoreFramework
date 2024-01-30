using Microsoft.AspNetCore.Mvc;

namespace Winterhold.Presentation.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
