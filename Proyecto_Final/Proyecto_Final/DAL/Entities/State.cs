﻿using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final.DAL.Entities
{
    public class State : Entity
    {
        [Display(Name = "Dpto/Estado")] 
        [MaxLength(50)] //varchar(50)
        [Required(ErrorMessage = "El campo {0} es obligatorio")] //Not Null
        public string Name { get; set; }

        [Display(Name = "País")] 
        public Country Country { get; set; } //Relacion con País

        //[Display(Name = "Ciudades")] //Nombre que quiero mostrar en la web
         // public ICollection<City> Cities { get; set; }

        //Propiedad de lectura...
        //[Display(Name = "Número Ciudades")] //Nombre que quiero mostrar en la web
        //public int CitiesNumber => Cities == null ? 0 : Cities.Count; //IF TERNARIO: SI state ES (==) null, ENTONCES (?) mandar un 0, SINO (:) mandar el COUNT

    }
}
