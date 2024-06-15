namespace EducationlPlatform.Models.Users
{
    public class Admin
    {
        public int AdminId { get; set; } 

        

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
