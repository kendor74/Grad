namespace EducationlPlatform.Models.Users
{
    public class StudentTutorRoom
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int TutorId { get; set; }
        public Tutor Tutor { get; set; }

        public int RoomId { get; set; }
        public Room Room{ get; set; }
    }
}
