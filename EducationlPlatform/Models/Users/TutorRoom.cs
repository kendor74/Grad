namespace EducationlPlatform.Models.Users
{
    public class TutorRoom
    {
        public int TutorId { get; set; }
        public virtual Tutor Tutor { get; set; } = new Tutor();

        public int RoomId { get; set; }
        public virtual Room Room { get; set; }

        public DateTime AccessStartDate { get; set; }
        public DateTime AccessEndDate { get; set; }
    }
}
