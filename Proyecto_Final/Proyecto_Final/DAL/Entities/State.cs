using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final.DAL.Entities
{
    public class State : Entity
    {
        [Display(Name = "Dpto/Estado")] 
        [MaxLength(50)]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Name { get; set; }

        [Display(Name = "País")]
        [JsonIgnore]
        public Country Country { get; set; } //Relacion con País

        [Display(Name = "Ciudades")] 
         public ICollection<City> Cities { get; set; }

        //Propiedad de lectura...
        [Display(Name = "Número Ciudades")] 
        public int CitiesNumber => Cities == null ? 0 : Cities.Count; //IF TERNARIO: SI state ES (==) null, ENTONCES (?) mandar un 0, SINO (:) mandar el COUNT

    }
}
