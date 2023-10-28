namespace EducationlPlatform.Models.Interfaces
{
    public interface IUser
    {
        Task<User> Login(User user);
        Task<User> SignUp(User user);
        Task<bool> Logout();
    
        Task<string> JwtToken(User user);
    }
}
