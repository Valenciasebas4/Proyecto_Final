using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.DAL;
using Proyecto_Final.DAL.Entities;
using Proyecto_Final.Helpers;
using Proyecto_Final.Models;
using Proyecto_Final.Services;

namespace Proyecto_Final.Controllers
{
    public class UserTrainingController : Controller
    {
        private readonly DataBaseContext _context;
        private readonly IDropDownListHelper _dropDownListHelper;
        private readonly IUserHelper _userHelper;

        public UserTrainingController(DataBaseContext context, IDropDownListHelper dropDownListHelper, IUserHelper userHelper)
        {
            _context = context;
            _dropDownListHelper = dropDownListHelper;
            _userHelper = userHelper;
        }
        public async Task<IActionResult> Index()
        {
      
            
            return _context.UserTrainings != null ?
                        View(await _context.UserTrainings.ToListAsync()) :
                       Problem("Entity set 'DataBaseContext.UserTrainings'  is null.");
            
        }


        public async Task<IActionResult> Create()
        {
            AddUserTrainingViewModel addUserTrainingViewModel = new()
            {
                Trainings = await _dropDownListHelper.GetDDLTrainingsAsync(),
            };

            return View(addUserTrainingViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserTrainingViewModel addUserTrainingViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = await _userHelper.GetUserAsync(User.Identity.Name);
                try
                {                  
                    UserTraining userTraining = new()
                    {
                        
                        DateOfClass = addUserTrainingViewModel.DateClass,
                        CreatedDate = DateTime.Now,
                        Training = await _context.Trainings.FindAsync(addUserTrainingViewModel.TrainingId),
                        User= user,
                    };


                    _context.Add(userTraining);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof( Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un producto con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            addUserTrainingViewModel.Trainings = await _dropDownListHelper.GetDDLTrainingsAsync();
            return View(addUserTrainingViewModel);
        }
    }
}
