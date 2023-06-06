namespace Proyecto_Final.DAL.Entities
{
    public class UserTraining : Entity
    {
       // public Guid UserID { get; set; }

        public Guid TrainingID { get; set; }

        public DateTime? DateOfClass { get; set; }

        public virtual User User { get; set; }

        public virtual Training Training { get; set; }
    }
}
