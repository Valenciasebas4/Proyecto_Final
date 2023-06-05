using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.DAL;
using Proyecto_Final.DAL.Entities;

namespace Proyecto_Final.Controllers
{
    public class TrainingUsersController : Controller
    {
        private readonly DataBaseContext _context;

        public TrainingUsersController(DataBaseContext context)
        {
            _context = context;
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: TrainingUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClassDate,Id,CreatedDate,ModifiedDate")] TrainingUser trainingUser)
        {
            if (ModelState.IsValid)
            {
                trainingUser.Id = Guid.NewGuid();
                _context.Add(trainingUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainingUser);
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
        public async Task<IActionResult> Edit(Guid id, [Bind("ClassDate,Id,CreatedDate,ModifiedDate")] TrainingUser trainingUser)
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
