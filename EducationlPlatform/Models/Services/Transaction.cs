namespace EducationlPlatform.Models.Services
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float Amount { get; set; }

        //public virtual User StudentId { get; set; }
        //public virtual User TutorId { get; set; }
        //public virtual Room RoomId { get; set; }
    }
}
