namespace EducationlPlatform.Models.Users
{
    public class User : IdentityUser
    {
        public string Image { get; set; }
        public string CreditCard { get; set; }
    }
}