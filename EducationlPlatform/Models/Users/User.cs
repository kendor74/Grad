namespace EducationlPlatform.Models.Users
{
    public class User : IdentityUser
    {
        public string? Image { get; set; }
        public byte Age { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        //credit card
        public DateTime RegisteredDate { get; set; } = DateTime.Now;
    }
}