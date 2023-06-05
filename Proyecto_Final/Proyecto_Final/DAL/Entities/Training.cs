using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final.DAL.Entities
{


    public class Training : Entity
    {
        [Display(Name = "Entrenamiento")]
        [MaxLength(100)]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Name { get; set; }
        
        [Display(Name = "Descripción")]
        public string? Description { get; set; }
        
        // Opcionalmente, puedes agregar una referencia a una entidad relacionada si es necesario.
        // public ClassType ClassType { get; set; }

        // Otras propiedades adicionales relacionadas con el registro de entrenamientos.

        // Puedes tener una colección de entrenamientos relacionados si es necesario.
       
    }
}
