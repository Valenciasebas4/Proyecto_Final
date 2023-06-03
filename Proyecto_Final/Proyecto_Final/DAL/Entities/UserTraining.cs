using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final.DAL.Entities
{
    public class UserTraining : Entity
    {
        [Display(Name = "Usuario")]
        public User User { get; set; }

        [Display(Name = "Entrenamiento")]
        public Training Trainings { get; set; }

        [Display(Name = "Fecha de emtrenamiento")]
        public virtual DateTime? TrainingDate { get; set; }

        
    }
}
