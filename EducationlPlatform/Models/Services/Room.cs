namespace EducationlPlatform.Models.Services
{
    public class Room
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public float Cost { get; set; }
        public List<string> ContentList { get; set; }
    }
}
