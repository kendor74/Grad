using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EducationlPlatform.Models.Handler
{
    public class UserServices 
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;
        private readonly Services<Student> _StudentContext;
        private readonly Services<Tutor> _TutorContext;
        

        public UserServices(UserManager<User> userManager, IConfiguration config
            , SignInManager<User> signInManager, Services<Student> studentContext
            , Services<Tutor> tutorServices )
        {
            _userManager = userManager;
            _config = config;
            _signInManager = signInManager;
            _StudentContext = studentContext;
            _TutorContext = tutorServices;
        }


        private string JwtToken(User user)
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

        public async Task<string> Login(UserDto user)
        {
            var userIdentity = await _userManager.FindByEmailAsync(user.Email);

            if (userIdentity != null)
            {
                var con = await _userManager.CheckPasswordAsync(userIdentity,user.Password);
                var result = await _signInManager.PasswordSignInAsync(userIdentity.UserName,user.Password,true,false);

                if (result.Succeeded)
                {
                    var claim = await _userManager.AddClaimAsync(userIdentity, new Claim("UserRole", "Admin"));
                    return "Logged in succeeded";
                }
                return "Logged in Failed, Please try again!";
            }
#pragma warning disable CS8603 // Possible null reference return.
            return null;
        }

        public async Task<bool> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<UserDto> SignUp(IFormFile Image, UserDto user)
        {
            IdentityResult result;
            var identityUser = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                City = user.City,
                Age = user.Age,
                Gender = user.Gender,
                EmailConfirmed = true
            };

            try
            {
                //save image into wwwroot ?? directory and display the path
                if (Image !=null && user.ImagePath == "" )
                {
                    string imageName = await UploadImage(Image, identityUser.UserName, identityUser.Id);
                    identityUser.ImagePath = imageName;
                    user.ImagePath = imageName;

                }
                else
                {
                    if (user.Gender == "Female")
                    {

                        identityUser.ImagePath = "FemaleIcon.png";
                    }
                    else
                    {
                        identityUser.ImagePath = "MaleIcon.png";
                    }
                }


                result = await _userManager.CreateAsync(identityUser, user.Password);

                //creating rules
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(identityUser, user.Role);
                }

                if (user.Role == "Student")
                {
                    Student st = new Student()
                    {
                        UserId = identityUser.Id,
                    };

                    await _StudentContext.Create(st);
                }
                else
                {
                    Tutor tu = new Tutor()
                    {
                        Description = user.Description,
                        DepartmentId = (int)user.DepartmentId,
                        UserId = identityUser.Id,
                    };
                    await _TutorContext.Create(tu);
                }

                //var Emailtoken = await _userManager.GenerateEmailConfirmationTokenAsync((User)identityUser);
                //user.EmailToken = Emailtoken;
                //user.JwtToken = JwtToken(user);


                return user;
            }
            catch(Exception ex) {

                
                
                
                return null;
            }

            
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userManager.Users.ToListAsync();
        }

        //TODO
        public async Task<UserDto> Edit(IFormFile Image, UserDto userDto)
        {
            var user = await _userManager.FindByEmailAsync(userDto.Email);

            user.FirstName   = userDto.FirstName;
            user.LastName    = userDto.LastName;
            user.UserName    = userDto.UserName;
            user.PhoneNumber = userDto.PhoneNumber;
            user.City        = userDto.City;
            user.Gender      = userDto.Gender;
            user.Age         = userDto.Age;
            user.ImagePath = (userDto.ImagePath != "Defualt") ? await UploadImage(Image, userDto.UserName, user.Id) : "Defualt";

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    userDto.Error.Add(item.ToString());
                }
            }
            return userDto;
        }

        //TODO
        public async Task<string> ChangePassword(User user, string currentPassword , string newPassword)
        {
            
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user,currentPassword,newPassword);
                if (result.Succeeded)
                {
                    return newPassword;
                }
                else
                { 
                    return result.Errors.ToString(); 
                }
            }

            return "The Password is Invalid !";
        }
       
        private async Task<string> UploadImage(IFormFile Image , string UserName , string UserId)
        {

            var imageName = $"{UserName}_{UserId}_{Guid.NewGuid().ToString()}.jpg";
            var imagePath = Path.Combine(@"D:\repo\EDU\EduConsummer\wwwroot\Upload\", imageName);

            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
            // Save the image to the folder
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await Image.CopyToAsync(stream);
            }       


            return imageName;
        }

        
    }
}
