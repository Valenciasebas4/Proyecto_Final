﻿using Proyecto_Final.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final.Models
{
    public class EditTemporalSaleViewModel : Entity
    {
        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentarios")]
        public string? Remarks { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Cantidad")]
        [Range(0.0000001, float.MaxValue, ErrorMessage = "Debes de ingresar un valor mayor a cero en la cantidad.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public float Quantity { get; set; }
    }
}