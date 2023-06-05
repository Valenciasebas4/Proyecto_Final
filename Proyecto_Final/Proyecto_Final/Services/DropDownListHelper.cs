using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto_Final.DAL.Entities;
using Proyecto_Final.DAL;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Helpers;

namespace Proyecto_Final.Services
{
    public class DropDownListHelper : IDropDownListHelper
    {
        public readonly DataBaseContext _context;

        public DropDownListHelper(DataBaseContext context)
        {
            _context = context;
        }



        public async Task<IEnumerable<SelectListItem>> GetDDLTrainingsAsync()
        {
            List<SelectListItem> listTrainings = await _context.Trainings
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                })
                .OrderBy(c => c.Text)
                .ToListAsync();

            listTrainings.Insert(0, new SelectListItem
            {
                Text = "Seleccione un entrenamiento...",
                Value = Guid.Empty.ToString(),
                Selected = true
            });

            return listTrainings;
        }


        public async Task<IEnumerable<SelectListItem>> GetDDLCategoriesAsync()
        {
            List<SelectListItem> listCategories = await _context.Categories
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                })
                .OrderBy(c => c.Text)
                .ToListAsync();

            listCategories.Insert(0, new SelectListItem
            {
                Text = "Seleccione una categoría...",
                Value = Guid.Empty.ToString(), 
                Selected = true
            });

            return listCategories;
        }


        public async Task<IEnumerable<SelectListItem>> GetDDLCategoriesAsync(IEnumerable<Category> filterCategories)
        {
            List<Category> categories = await _context.Categories.ToListAsync(); 
            List<Category> categoriesFiltered = new(); 

            foreach (Category category in categories)
                if (!filterCategories.Any(c => c.Id == category.Id))
                    categoriesFiltered.Add(category);

            List<SelectListItem> listCategories = categoriesFiltered
                .Select(c => new SelectListItem
                {
                    Text = c.Name, 
                    Value = c.Id.ToString(),                  
                })
                .OrderBy(c => c.Text)
                .ToList();

            listCategories.Insert(0, new SelectListItem
            {
                Text = "Seleccione una categoría...",
                Value = Guid.Empty.ToString(),
                Selected = true
            });

            return listCategories;
        }

        public async Task<IEnumerable<SelectListItem>> GetDDLCountriesAsync()
        {
            List<SelectListItem> listCountries = await _context.Countries
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                })
                .OrderBy(c => c.Text)
                .ToListAsync();

            listCountries.Insert(0, new SelectListItem
            {
                Text = "Seleccione un país...",
                Value = Guid.Empty.ToString(),
                Selected = true
            });

            return listCountries;
        }


        public async Task<IEnumerable<SelectListItem>> GetDDLStatesAsync(Guid countryId)
        {
            List<SelectListItem> listStates = await _context.States
                .Where(s => s.Country.Id == countryId)
                .Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString(),
                })
                .OrderBy(s => s.Text)
                .ToListAsync();

            listStates.Insert(0, new SelectListItem
            {
                Text = "Seleccione un estado...",
                Value = Guid.Empty.ToString(),
                Selected = true
            });

            return listStates;
        }

        public async Task<IEnumerable<SelectListItem>> GetDDLCitiesAsync(Guid stateId)
        {
            List<SelectListItem> listCities = await _context.Cities
                .Where(c => c.State.Id == stateId)
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                })
                .OrderBy(c => c.Text)
                .ToListAsync();

            listCities.Insert(0, new SelectListItem
            {
                Text = "Seleccione una ciudad...",
                Value = Guid.Empty.ToString(),
                Selected = true
            });

            return listCities;
        }

        public Task<string> GetCountryNameAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetStateNameAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetCityNameAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
