using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final.Models
{
    public class AddTrainingUserViewModel
    {
        [Display(Name = "Entrenamiento")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Guid TrainingId { get; set; }

        public IEnumerable<SelectListItem> Trainings { get; set; }

        public DateTime DateClass { get; set; }
    }
}
