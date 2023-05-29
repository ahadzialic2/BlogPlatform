using Microsoft.AspNetCore.Mvc;

namespace BlogPlatform.API.Controllers;

public class UserController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}