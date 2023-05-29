using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.DAL;
using Proyecto_Final.DAL.Entities;
using Proyecto_Final.Models;

namespace Proyecto_Final.Controllers
{
    public class CountriesController : Controller
    {
        private readonly DataBaseContext _context;

        public CountriesController(DataBaseContext context)
        {
            _context = context;
        }


        // GET: Countries
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Countries
                .Include(c => c.States) //El Include me hace las veces del INNER JOIN
                .ToListAsync());
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Countries == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Countries/Create
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }


        // POST: Countries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Country country)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    country.CreatedDate = DateTime.Now;
                    _context.Add(country);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                        ModelState.AddModelError(string.Empty, "Ya existe un país con el mismo nombre.");
                    else
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(country);
        }


        // GET: Countries/Edit/5
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Countries == null) return NotFound();

            var country = await _context.Countries.FindAsync(id);
            if (country == null) return NotFound();

            return View(country);
        }


        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Countries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id, Country country)
        {
            if (id != country.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    country.ModifiedDate = DateTime.Now;
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                        ModelState.AddModelError(string.Empty, "Ya existe un país con el mismo nombre.");
                    else
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(country);
        }




        // GET: Countries/Delete/5
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Countries == null) return NotFound();

            var country = await _context.Countries.FirstOrDefaultAsync(m => m.Id == id); //Select * From Countries Where Id = '7a216d04-3048-4757-9b02-f72ded5180bf'

            if (country == null) return NotFound();

            return View(country);
        }


        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Countries == null)
                return Problem("Entity set 'DataBaseContext.Countries' is null.");

            var country = await _context.Countries.FindAsync(id); //Select * From Countries Where Id = '7a216d04-3048-4757-9b02-f72ded5180bf'
            if (country != null) _context.Countries.Remove(country);

            await _context.SaveChangesAsync(); //Delete From Counties where Id = '7a216d04-3048-4757-9b02-f72ded5180bf'
            return RedirectToAction(nameof(Index));
        }



        //////////////////////////////////

        [HttpGet]
        public async Task<IActionResult> AddState(Guid? countryId)
        {
            if (countryId == null) return NotFound();

            Country country = await _context.Countries.FirstOrDefaultAsync(c => c.Id == countryId);

            if (country == null) return NotFound();

            StateViewModel stateViewModel = new()
            {
                CountryId = country.Id,
            };

            return View(stateViewModel);
        }

    }
}
