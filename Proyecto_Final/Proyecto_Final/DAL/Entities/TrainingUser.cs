using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final.DAL.Entities
{
    public class TrainingUser : Entity
    {
        [Display(Name = "Usuario")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [Display(Name = "Entrenamiento")]
        public Guid TrainingId { get; set; }
        public Training Training { get; set; }

        [Display(Name = "Fecha de clase")]
        public virtual DateTime? ClassDate { get; set; }


    }
}
