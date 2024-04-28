namespace EducationlPlatform.Models.Users
{
    public class Student 
    {
        [Key]
        public int Id { get; set; }
        
        public virtual ICollection<StudentTutorRoom> StudentTutorRooms { get; set; } = new List<StudentTutorRoom>();

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
