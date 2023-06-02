
using Proyecto_Final.DAL.Entities;

namespace Proyecto_Final.Models
{
    public class CityViewModel : City
    {
        public Guid StateId { get; set; }
    }
}
