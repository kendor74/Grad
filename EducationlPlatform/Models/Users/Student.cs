namespace EducationlPlatform.Models.Users
{
    public class Student 
    {
        public int Id { get; set; }
        
        public virtual ICollection<StudentTutorRoom> StudentTutorRooms { get; set; }
        
        public ICollection<Contact> Contacts { get; set; }
        
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
