using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Final.DAL.Entities;
using Proyecto_Final.DAL;
using Proyecto_Final.Enum;
using Proyecto_Final.Helpers;
using Proyecto_Final.Models;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Final.Controllers
{
    public class UsersController : Controller
    {
        private readonly DataBaseContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IDropDownListHelper _ddlHelper;
        private readonly IAzureBlobHelper _azureBlobHelper;

        public UsersController(DataBaseContext context, IUserHelper userHelper, IDropDownListHelper dropDownListHelper, IAzureBlobHelper azureBlobHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _ddlHelper = dropDownListHelper;
            _azureBlobHelper = azureBlobHelper;
        }

        private string GetUserFullName()
        {
            return _context.Users
                .Where(u => u.Email == User.Identity.Name)
                .Select(u => u.FullName)
                .FirstOrDefault();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            ViewBag.UserFullName = GetUserFullName();
            return View(await _context.Users
                .Include(u => u.City)
                .ThenInclude(c => c.State)
                .ThenInclude(s => s.Country)
                .ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> CreateAdmin()
        {
            ViewBag.UserFullName = GetUserFullName();
            AddUserViewModel addUserViewModel = new()
            {
                Id = Guid.Empty,
                Countries = await _ddlHelper.GetDDLCountriesAsync(),
                States = await _ddlHelper.GetDDLStatesAsync(new Guid()),
                Cities = await _ddlHelper.GetDDLCitiesAsync(new Guid()),
                UserType = UserType.Admin,
            };

            return View(addUserViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdmin(AddUserViewModel addUserViewModel)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty; 

                if (addUserViewModel.ImageFile != null)
                    imageId = await _azureBlobHelper.UploadAzureBlobAsync(addUserViewModel.ImageFile, "users");

                addUserViewModel.ImageId = imageId;
                addUserViewModel.CreatedDate = DateTime.Now;

                User user = await _userHelper.AddUserAsync(addUserViewModel);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Este correo ya está siendo usado.");
                    await FillDropDownListLocation(addUserViewModel);
                    return View(addUserViewModel);
                }

                return RedirectToAction("Index", "Users");
            }

            await FillDropDownListLocation(addUserViewModel);
            return View(addUserViewModel);
        }

        private async Task FillDropDownListLocation(AddUserViewModel addUserViewModel)
        {
            addUserViewModel.Countries = await _ddlHelper.GetDDLCountriesAsync();
            addUserViewModel.States = await _ddlHelper.GetDDLStatesAsync(addUserViewModel.CountryId);
            addUserViewModel.Cities = await _ddlHelper.GetDDLCitiesAsync(addUserViewModel.StateId);
        }

        [HttpGet]
        public JsonResult GetStates(Guid countryId)
        {
            ViewBag.UserFullName = GetUserFullName();
            Country country = _context.Countries
                .Include(c => c.States)
                .FirstOrDefault(c => c.Id == countryId);

            if (country == null) return null;

            return Json(country.States.OrderBy(d => d.Name));
        }

        [HttpGet]
        public JsonResult GetCities(Guid stateId)
        {
            ViewBag.UserFullName = GetUserFullName();
            State state = _context.States
                .Include(s => s.Cities)
                .FirstOrDefault(s => s.Id == stateId);
            if (state == null) return null;

            return Json(state.Cities.OrderBy(c => c.Name));
        }

    }
}
