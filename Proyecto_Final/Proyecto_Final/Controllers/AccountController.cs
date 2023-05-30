using Microsoft.AspNetCore.Mvc;
using Proyecto_Final.DAL;
using Proyecto_Final.Helpers;
using Proyecto_Final.Models;

namespace Proyecto_Final.Controllers
{
    public class AccountController : Controller
    {

        private readonly IUserHelper _userHelper;
        private readonly DataBaseContext _context;
        //private readonly IDropDownListHelper _ddlHelper;
        //private readonly IAzureBlobHelper _azureBlobHelper;

        public AccountController(IUserHelper userHelper, DataBaseContext context /*,IDropDownListHelper dropDownListHelper, IAzureBlobHelper azureBlobHelper*/)
        {
            _userHelper = userHelper;
            _context = context;
            //_ddlHelper = dropDownListHelper;
            //_azureBlobHelper = azureBlobHelper;
        }


        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new LoginViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _userHelper.LoginAsync(loginViewModel);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Email o contraseña incorrectos.");
            }
            return View(loginViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }











    }
}
