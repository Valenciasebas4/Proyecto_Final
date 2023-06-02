using Microsoft.AspNetCore.Mvc;
using Proyecto_Final.DAL.Entities;

namespace Proyecto_Final.Models
{
    public class HomeViewModel
    {
        public ICollection<Product> Products { get; set; }

        //Esta propiedad me muestra cuánto productos llevo agregados al carrito de compras.
        public float Quantity { get; set; }
    }
}
