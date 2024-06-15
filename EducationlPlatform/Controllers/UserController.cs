namespace EducationlPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserServices _context;
        
        public UserController(UserServices context)
        {
            _context = context;
        }

        // TOFIX
        [HttpGet("Login")]
        public async Task<IActionResult> Login(string Email, string Password)
        {
            UserDto userDto = new UserDto();
            userDto.Email = Email;
            userDto.Password = Password;

            var result = await _context.Login(userDto);

            var user = User.Identity.Name;
            return (result != null) ? Ok(result) : BadRequest("Invalid Login");
        }


        //Need
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm]UserDto user)
        {
            if (user.Image == null)
            {
                user.ImagePath = "Default";
                string path = (user.Gender == "Male")? "D:\\repo\\EDU\\EduConsummer\\wwwroot\\Upload\\MaleIcon.png" : "D:\\repo\\EDU\\EduConsummer\\wwwroot\\Upload\\FemaleIcon.png";

                var placeholderBytes = System.IO.File.ReadAllBytes(path);
                var defaultFile = 
                new FormFile(new MemoryStream(placeholderBytes), 0, placeholderBytes.Length, $"{user.Gender}Icon", $"{user.Gender}.png")
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/png"
                };
                user.Image = defaultFile;
            }

            if (user.Role == "Tutor" && (user.DepartmentId == null || user.Description == ""))
            {
                return BadRequest("You Must Choose Department and write a description as a Tutor!");
            }

            var result = await _context.SignUp(user.Image,user);

            if (ModelState.IsValid)
                return Ok(result);
            else
                return BadRequest($"Creatation denied!\n {result.Error}");
            
        }





        // TOFIX
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            var user = User.Identity.IsAuthenticated;
            if (user)
                return BadRequest("No account to log out");
            
            bool result = await _context.Logout();
            return (result ? Ok("Logged out") : BadRequest("could not logout"));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Get Users")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _context.GetUsers());
        }

        // TOFIX
        //[HttpPost("Changing Password")]
        //public async Task<IActionResult> ChangePassword(string Email,string CurrentPassword,string NewPassword)
        //{
        //    var result = _context.ChangePassword(Email, CurrentPassword, NewPassword);

        //    return Ok(result);
        //}

        [HttpPut("Edit Profile")]
        public async Task<IActionResult> EditUserProfile([FromForm]UserDto user)
        {
            if (user.Image == null)
            {
                user.ImagePath = "Default";
                string path = (user.Gender == "Male") ? "D:\\repo\\EDU\\EduConsummer\\wwwroot\\Upload\\MaleIcon.png" : "D:\\repo\\EDU\\EduConsummer\\wwwroot\\Upload\\FemaleIcon.png";

                var placeholderBytes = System.IO.File.ReadAllBytes(path);
                var defaultFile =
                new FormFile(new MemoryStream(placeholderBytes), 0, placeholderBytes.Length, $"{user.Gender}Icon", $"{user.Gender}.png")
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/png"
                };
                user.Image = defaultFile;
            }
            var result = await _context.Edit(user.Image,user);
            return Ok(result);
        }
    

    }
}
