namespace EducationlPlatform.Models.Services
{
    public class Room
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public float Cost { get; set; }


        public virtual ICollection<StudentTutorRoom> StudentTutorRooms { get; set; } 
        
    }

    enum RoomType : byte
    {
        TutorRoom,
        StudentRoom,
    }
}