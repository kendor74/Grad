using EducationlPlatform.Models.Services;

namespace EducationlPlatform.Models.Users
{
    public class Student 
    {
        public virtual List<Tutor> Tutors { get; set; }
        public virtual List<Transaction> Transactions { get; set; }
        public virtual List<Room> Rooms { get; set; }
        public virtual User User { get; set; }
    }
}
