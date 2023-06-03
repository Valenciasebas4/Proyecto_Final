using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto_Final.DAL.Entities;

namespace Proyecto_Final.Helpers
{
    public interface IDropDownListHelper
    {
        Task<IEnumerable<SelectListItem>> GetDDLCategoriesAsync(); 

        Task<IEnumerable<SelectListItem>> GetDDLCategoriesAsync(IEnumerable<Category> filterCategories); 

        Task<IEnumerable<SelectListItem>> GetDDLCountriesAsync();

        Task<IEnumerable<SelectListItem>> GetDDLStatesAsync(Guid countryId);

        Task<IEnumerable<SelectListItem>> GetDDLCitiesAsync(Guid stateId);
        Task<IEnumerable<SelectListItem>> GetDDLTrainingsAsync();
    }
}
