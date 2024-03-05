namespace EducationlPlatform.Models.Users
{
    public class Tutor
    {
        public int TutorId { get; set; }

        public string Description { get; set; }
        
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        
        //public virtual ICollection<TutorRoom>? Rooms { get; set; }
        

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
