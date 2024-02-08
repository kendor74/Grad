namespace EducationlPlatform.Models.Interfaces.UserInterfaces
{
    public interface IUser
    {
        Task<UserDto> Login(UserDto user);
        Task<UserDto> SignUp(UserDto user);
        Task<bool> Logout();
        Task<IEnumerable<IdentityUser>> GetUsers();
        Task<UserDto> Edit(string id, UserDto userDto);
        Task<string> ChangePassword(string id, string currentPassword, string newPassword);

        //Task<string> JwtToken(UserDto user);
    }
}
