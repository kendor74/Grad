namespace EducationlPlatform.Models.Services
{
    public class Contact
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        public int TutorId { get; set; }
        public virtual Tutor Tutor { get; set; }

        public ICollection<Message> Messages { get; set; }
    }


    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }

        public int ContactId { get; set; }
        public virtual Contact Contact { get; set; }

    }
}
