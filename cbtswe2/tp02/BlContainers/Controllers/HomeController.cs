using Microsoft.AspNetCore.Mvc;
namespace BlContainers.Controllers;
public class HomeController : Controller
{
    public IActionResult Index() => View();
    public IActionResult Creditos() => View();
}
