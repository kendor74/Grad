using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EducationlPlatform.Models.Interfaces.UserInterfaces;

namespace EducationlPlatform.Models.InterfaceHandler.UsersHandler
{
    public class UserServices : IUser
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;
        private readonly RoleManager<IdentityRole> _roleManager;


        public UserServices(UserManager<User> userManager, IConfiguration config
            , RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _config = config;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }


        public async Task<string> JwtToken(User user)
        {
            IEnumerable<Claim> claims = new List<Claim>
            {
                    new Claim(ClaimTypes.Email , user.UserName),
            };

#pragma warning disable CS8604 // Possible null reference argument.
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                issuer: _config.GetSection("Jwt:Issuer").Value,
                audience: _config.GetSection("Jwt:Audiance").Value,
                signingCredentials: signingCredentials);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return tokenString;
        }

        public Task<User> Login(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Logout()
        {
            throw new NotImplementedException();
        }

        public Task<User> SignUp(User user)
        {
            throw new NotImplementedException();
        }
    }
}
