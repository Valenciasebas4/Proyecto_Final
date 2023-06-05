using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.DAL;
using Proyecto_Final.DAL.Entities;
using Proyecto_Final.Enum;
using Proyecto_Final.Helpers;
using Proyecto_Final.Models;
using Proyecto_Final.Services;

namespace Proyecto_Final.Controllers
{
    public class TrainingUsersController : Controller
    {
        private readonly DataBaseContext _context;
        private readonly IDropDownListHelper _dropDownListHelper;
        private readonly IUserHelper _userHelper;

        public TrainingUsersController(DataBaseContext context, IUserHelper userHelper, IDropDownListHelper dropDownListHelper)
        {
            _context = context;
            _dropDownListHelper = dropDownListHelper;
            _userHelper = userHelper;
        }

        // GET: TrainingUsers
        public async Task<IActionResult> Index()
        {
              return View(await _context.TrainingsUser.ToListAsync());
        }

        // GET: TrainingUsers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.TrainingsUser == null)
            {
                return NotFound();
            }

            var trainingUser = await _context.TrainingsUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingUser == null)
            {
                return NotFound();
            }

            return View(trainingUser);
        }

        // GET: TrainingUsers/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            AddTrainingUserViewModel addTrainingUserViewModel = new()
            {
                Trainings = await _dropDownListHelper.GetDDLTrainingsAsync(),
            };

            return View(addTrainingUserViewModel);
        }

        // POST: TrainingUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(AddTrainingUserViewModel addTrainingUserViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = await _userHelper.GetUserAsync(User.Identity.Name);
                TrainingUser trainingUser = new()
                    {
                        User = addTrainingUserViewModel.User,
                        Training = addTrainingUserViewModel.Training,
                        ClassDate = addTrainingUserViewModel.DateClass,
                        CreatedDate = DateTime.Now,
                    };
                _context.Add(trainingUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                // Redirigir al usuario a una página de éxito o a otra acción según sea necesario
               // return RedirectToAction("Index", "Home");
            }

            // Si el modelo no es válido, regresar a la vista de creación con los errores de validación
            addTrainingUserViewModel.Trainings = await _dropDownListHelper.GetDDLTrainingsAsync();
            return View(addTrainingUserViewModel);
        }

        private async Task FillDropDownListLocation(AddTrainingUserViewModel addTrainingUserViewModel)
        {
            addTrainingUserViewModel.Trainings = await _dropDownListHelper.GetDDLTrainingsAsync();
            
        }

        

        // GET: TrainingUsers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.TrainingsUser == null)
            {
                return NotFound();
            }

            var trainingUser = await _context.TrainingsUser.FindAsync(id);
            if (trainingUser == null)
            {
                return NotFound();
            }
            return View(trainingUser);
        }

        // POST: TrainingUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, TrainingUser trainingUser)
        {
            if (id != trainingUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingUserExists(trainingUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(trainingUser);
        }

        // GET: TrainingUsers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.TrainingsUser == null)
            {
                return NotFound();
            }

            var trainingUser = await _context.TrainingsUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingUser == null)
            {
                return NotFound();
            }

            return View(trainingUser);
        }

        // POST: TrainingUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.TrainingsUser == null)
            {
                return Problem("Entity set 'DataBaseContext.TrainingsUser'  is null.");
            }
            var trainingUser = await _context.TrainingsUser.FindAsync(id);
            if (trainingUser != null)
            {
                _context.TrainingsUser.Remove(trainingUser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingUserExists(Guid id)
        {
          return _context.TrainingsUser.Any(e => e.Id == id);
        }
    }
}
