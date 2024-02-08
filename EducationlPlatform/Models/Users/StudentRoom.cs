namespace EducationlPlatform.Models.Users
{
    public class StudentRoom
    {
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        public int RoomId { get; set; }
        public virtual Room Room{ get; set; }

        public DateTime AccessStartDate { get; set; }
        public DateTime AccessEndDate { get; set; }
    }
}
