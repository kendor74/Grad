namespace EducationlPlatform.Models.Users
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public override string UserName { get; set; }
        public string? ImagePath { get; set; }
        public byte Age { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        //credit card
        public DateTime RegisteredDate { get; set; } = DateTime.Now;

        
    }
}

