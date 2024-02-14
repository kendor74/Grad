using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EducationlPlatform.Models.InterfaceHandler
{
    public class UserServices : IUser
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;
        private readonly RoleManager<IdentityRole> _roleManager;
        //byte[] defualtImage = File.ReadAllBytes("D:\\images\\1.jpg");



        public UserServices(UserManager<User> userManager, IConfiguration config
            , RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _config = config;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }


        private string JwtToken(UserDto user)
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

        public async Task<UserDto> Login(UserDto user)
        {
            var userIdentity = await _userManager.FindByEmailAsync(user.Email);

            if (userIdentity != null)
            {

                //add role
                //user.Role = await _userManager.getR
                await _userManager.CheckPasswordAsync(userIdentity, user.Password);
                user.Email = user.Email;
                user.Password = user.Password;
                user.UserName = userIdentity.UserName;
                //user.Role = role.FirstOrDefault();
                user.JwtToken = JwtToken(user);
                return user;
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

        public async Task<UserDto> SignUp(UserDto user)
        {
            IdentityResult result;
            var identityUser = new User
            {
                //UserName = user.FirstName + 
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                City = user.City,
                Age = user.Age,
                Gender = user.Gender,
                EmailConfirmed = true
            };

           


            try
            {
                result = await _userManager.CreateAsync((User)identityUser, user.Password);
                if (await _roleManager.FindByIdAsync(identityUser.Id) != null && result.Succeeded)
                {
                    //is IdentityUser include the id after CreateAsync

                    //save image into wwwroot ?? directory and display the path
                    string imageName= await UploadImage(user.Image ,identityUser.UserName ,identityUser.Id);
                    identityUser.Image = imageName;
                    await _userManager.UpdateAsync(identityUser);

                    //saving Role of the identity
                    IdentityRole roles = new IdentityRole();

                    roles.Id = identityUser.Id;
                    roles.Name = user.Role;
                    await _roleManager.CreateAsync(roles);
                }
                var Emailtoken = await _userManager.GenerateEmailConfirmationTokenAsync((User)identityUser);
                var userDto= new UserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    City = user.City,
                    Age = user.Age,
                    Image = user.Image
                };
                userDto.EmailToken = Emailtoken;
                userDto.JwtToken = JwtToken(userDto);


                return userDto;
            }
            catch(Exception ex) {
                return new UserDto { Error = ex.Message };
                
            }

            
        }

        public async Task<IEnumerable<IdentityUser>> GetUsers()
        {
            var list = await _userManager.Users.ToListAsync();
            return list;
        }

        public async Task<UserDto> Edit(string id,UserDto userDto)
        {
            var user = await _userManager.FindByIdAsync(id);

            user.Email = userDto.Email;
            user.PhoneNumber = userDto.PhoneNumber;
            user.City = userDto.City;
            user.Gender = userDto.Gender;
            user.Age = userDto.Age;
            //user.Image = userDto.Image;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                userDto.Error = result.Errors.ToString();   
            }
            return userDto;
        }

        public async Task<string> ChangePassword(string id, string currentPassword , string newPassword)
        {
            var user = await _userManager.FindByIdAsync(id);
            var checkPassword = await _userManager.CheckPasswordAsync(user, currentPassword);
            if (checkPassword)
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

            // Save the image to the folder
            var imagePath = Path.Combine("wwwroot/images", imageName);
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await Image.CopyToAsync(stream);
            }

           
            return imagePath;
        }
    }
}
