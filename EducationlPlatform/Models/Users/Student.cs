using EducationlPlatform.Models.Services;

namespace EducationlPlatform.Models.Users
{
    public class Student 
    {
        // Temprary
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual List<Tutor>? Tutors { get; set; }
        public virtual List<Transaction>? Transactions { get; set; }
        public virtual List<Room>? Rooms { get; set; }
        //public virtual User User { get; set; }
    }
}
