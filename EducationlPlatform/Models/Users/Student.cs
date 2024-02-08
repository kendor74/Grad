
namespace EducationlPlatform.Models.Users
{
    public class Student 
    {
        public int StudentId { get; set; }

        
        public virtual ICollection<StudentRoom>? Rooms { get; set; }
        
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
