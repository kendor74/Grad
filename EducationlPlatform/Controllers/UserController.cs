namespace EducationlPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _context;
        
        public UserController(IUser context)
        {
            _context = context;
        }


        [HttpGet("Login")]
        public async Task<IActionResult> Login(string Email, string Password, string Role)
        {
            UserDto userDto = new UserDto();
            userDto.Email = Email;
            userDto.Password = Password;
            userDto.Role = Role;

            var result = await _context.Login(userDto);

            return (result != null) ? Ok(result) : BadRequest("Invalid Login");
        }


        //Need
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserDto user)
        {
            
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


        [HttpPost("Changing Password")]
        public async Task<IActionResult> ChangePassword(string id,string currentPassword,string newPassword)
        {
            var result = _context.ChangePassword(id, currentPassword, newPassword);

            return Ok(result);
        }

        [HttpPut("Edit Profile")]
        public async Task<IActionResult> EditUserProfile(UserDto user)
        {
            var result = await _context.Edit(user);
            return Ok(result);
        }
    

    }
}
