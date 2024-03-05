namespace EducationlPlatform.Models.Users
{
    public class Student 
    {
        [Key]
        public int Id { get; set; }
        
        //public virtual ICollection<StudentRoom>? Rooms { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
