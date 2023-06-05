using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto_Final.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final.Models
{
    public class AddProductViewModel : EditProductViewModel
    {
        [Display(Name = "Categoría")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Guid CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        [Display(Name = "Foto")]
        public IFormFile? ImageFile { get; set; }
    }
}
