namespace EducationlPlatform.Models.Users
{
    public class Admin : User
    {
        //hello there here i am back 
        // Temprary
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<string> Comments { get; set; }
        //public virtual User User { get; set; }
    }
}
