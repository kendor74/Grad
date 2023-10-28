using EducationlPlatform.Models.Services;

namespace EducationlPlatform.Models.User
{
    public class Tutor
    {
        public string Description { get; set; }
        public virtual Field FieldName { get; set; }
        public virtual List<Transaction> Transactions { get; set; }
        public virtual List<Room> Rooms { get; set; }
        public virtual User User { get; set; }
    }
}
