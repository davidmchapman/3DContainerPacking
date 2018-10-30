using Microsoft.AspNetCore.Mvc;

namespace CromulentBisgetti.DemoApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}