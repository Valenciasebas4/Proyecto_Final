using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.DAL;
using Proyecto_Final.DAL.Entities;
using Proyecto_Final.Helpers;
using Proyecto_Final.Models;
using Proyecto_Final.Services;

namespace Proyecto_Final.Controllers
{
    public class TrainingUserController : Controller
    {
        private readonly DataBaseContext _context;
        private readonly IDropDownListHelper _dropDownListHelper;
        private readonly IUserHelper _userHelper;

        public TrainingUserController(DataBaseContext context, IUserHelper userHelper, IDropDownListHelper dropDownListHelper)
        {
            _context = context;         
            _dropDownListHelper = dropDownListHelper;
            _userHelper = userHelper;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddTrainingUser()
        {
            AddTrainingUserViewModel addTrainingUserViewModel = new()
            {
                Trainings = await _dropDownListHelper.GetDDLTrainingsAsync(),
            };

            return View(addTrainingUserViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTrainingUser(AddTrainingUserViewModel addTrainingUserViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = await _userHelper.GetUserAsync(User.Identity.Name);
                try
                {
                    TrainingUser trainingUser = new()
                    {
                        UserId =  addTrainingUserViewModel.UserId,
                        ClassDate = addTrainingUserViewModel.DateClass,
                        Training = await _context.Trainings.FirstOrDefaultAsync(c => c.Id == addTrainingUserViewModel.TrainingId),
                        //User = await _context.Users.FirstOrDefaultAsync(c => c.Id == addTrainingUserViewModel.UserId),
                        //Cities = new List<City>(),
                        //Country = await _context.Countries.FirstOrDefaultAsync(c => c.Id == stateViewModel.CountryId),
                        //Name = stateViewModel.Name,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = null,
                    };

                    _context.Add(trainingUser);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                        ModelState.AddModelError(string.Empty, "Ya existe un Dpto/Estado con el mismo nombre en este país.");
                    else
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(addTrainingUserViewModel);
        }
    }
}
