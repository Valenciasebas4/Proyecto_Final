using Microsoft.AspNetCore.Mvc;
using Proyecto_Final.DAL;
using Proyecto_Final.Helpers;
using Proyecto_Final.Models;
using System.Diagnostics;

namespace Proyecto_Final.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataBaseContext _context;
        private readonly IUserHelper _userHelper;
        //private readonly IOrderHelper _orderHelper;

        public HomeController(ILogger<HomeController> logger, DataBaseContext context,IUserHelper userHelper/*, IOrderHelper orderHelper*/)
        {
            _logger = logger;
            _context = context;
            _userHelper = userHelper;
            //_orderHelper = orderHelper;
        }

        public IActionResult Index()
        {
            //Variables de Sesión
            ViewBag.UserFullName = GetUserFullName();

            return View();

        }


        private string GetUserFullName()
        {
            return _context.Users
                .Where(u => u.Email == User.Identity.Name)
                .Select(u => u.FullName)
                .FirstOrDefault();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("error/404")]
        public IActionResult Error404()
        {
            return View();
        }


    }
}