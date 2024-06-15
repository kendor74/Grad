namespace EduConsummer.Models.Users
{
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImagePath { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string Role { get; set; }
        public byte Age { get; set; }
    }
}
