using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Final.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
