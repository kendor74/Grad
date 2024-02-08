using EducationlPlatform.Models.InterfaceHandler;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;

namespace EducationlPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(IUser context ,UserManager<User> userManager, IConfiguration config
            , RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _config = config;
            _roleManager = roleManager;
            _signInManager = signInManager;

        }


        [HttpGet]
        public async Task<IActionResult> Login(string Email, string Password, string Role)
        {
            UserDto userDto = new UserDto();
            userDto.Email = Email;
            userDto.Password = Password;
            userDto.Role = Role;

            var result = await _context.Login(userDto);

            return (result != null) ? Ok(result) : BadRequest("Invalid Login");
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(string Email, string Password, string Phone, string username, IFormFile Image
                                                 , string City, byte Age, string Gender)
        {
            UserDto user = new UserDto
            {
                Email = Email,
                Password = Password,
                PhoneNumber = Phone,
                Image = Image,
                UserName = username,
                City = City,
                Age = Age,
                Gender = Gender
            };


            var result = await _context.SignUp(user);

            //return (result.Error.IsNullOrEmpty()) ? Ok(result) : BadRequest(result.Error);
            return Ok(result);
        }






        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            bool result = await _context.Logout();
            return (result ? Ok("Logged out") : BadRequest("No account to log out"));
        }


        [HttpGet("Get Users")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _context.GetUsers());
        }



    

    }
}
