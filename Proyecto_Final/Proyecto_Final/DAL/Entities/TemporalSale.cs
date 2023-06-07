﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final.DAL.Entities
{
    public class TemporalSale : Entity
    {
        public ICollection<OrderDetail> OrderDetails { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        [JsonIgnore]
        public Product Product { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public float Quantity { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentarios")]
        public string? Remarks { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Valor")]
        public decimal Value => Product == null ? 0 : (decimal)Quantity * Product.Price;
    }
}
