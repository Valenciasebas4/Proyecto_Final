using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Proyecto_Final.DAL.Entities
{
    public class Country : Entity
    {
        /*DataAnnotations*/
        [Display(Name = "País")] 
        [MaxLength(50)] 
        [Required(ErrorMessage = "El campo {0} es obligatorio")] 
        public string Name { get; set; }

        [Display(Name = "Estados")] 
        public ICollection<State> States { get; set; } // Colección de Estados para el país

        [Display(Name = "Número Estados")] 
        public int StateNumber => States == null ? 0 : States.Count; 
    }
}
