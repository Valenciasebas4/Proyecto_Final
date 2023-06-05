using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final.DAL.Entities
{
    public class TrainingUser : Entity
    {
        //public int UserId { get; set; }
        public User User { get; set; }

        public Training Training { get; set; }

        [Display(Name = "Fecha de clase")]
        public virtual DateTime? ClassDate { get; set; }


    }
}
