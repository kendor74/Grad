namespace EducationlPlatform.Models.Services
{
    public class Room
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public float Cost { get; set; }


        public virtual ICollection<StudentRoom> StudentRooms { get; set; } = new List<StudentRoom>();
        public virtual ICollection<TutorRoom> TutorRooms{ get; set; } = new List<TutorRoom>();
        
    }
}