using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final.DAL.Entities
{
    public class UserTraining : Entity
    {
       // public Guid UserID { get; set; }

        public Guid TrainingID { get; set; }

        [Display(Name = "Fecha de clase")]
        public DateTime? DateOfClass { get; set; }

        public virtual User User { get; set; }

        public virtual Training Training { get; set; }
        public string UserId { get; internal set; }
    }
}
