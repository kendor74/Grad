namespace EducationlPlatform.Models.Users
{
    public class StudentTutorRoom
    {
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        public int TutorId { get; set; }
        public virtual Tutor Tutor { get; set; }

        public int RoomId { get; set; }
        public virtual Room Room{ get; set; }

    }
}
