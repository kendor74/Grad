namespace TestUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IApiRequest<User> _request;

        public UserController(IApiRequest<User> request) 
        {
            _request = request;
        }

        //TODO

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(User user)
        {
            user.Gender = Request.Form["gender"];


            ViewBag.Error("Creation Denied");
            var result = await _request.Post("api/User/Register", user);

            return (result != null ? RedirectToAction("/Home/Users") : RedirectToAction("Home", "SignUp"));
        }

        //public IActionResult GetProfilePicture()
        //{
        //    //// Get the user's profile picture path from your data storage.
        //    //var userProfilePicturePath = UserProfileService.GetProfilePicturePath(User.Identity.Name);

        //    //if (System.IO.File.Exists(userProfilePicturePath))
        //    //{
        //    //    var imageBytes = System.IO.File.ReadAllBytes(userProfilePicturePath);
        //    //    return File(imageBytes, "image/jpeg"); // Adjust the content type based on your image type.
        //    //}

        //    //// Return a default image or placeholder if the profile picture doesn't exist.
        //    //var defaultImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/default-profile-picture.jpg");
        //    //var defaultImageBytes = System.IO.File.ReadAllBytes(defaultImagePath);
        //    //return File(defaultImageBytes, "image/jpeg");
        //}

        [HttpGet]
        public async Task<IActionResult> Users()
        {
            var users = await _request.GetAll("api/User/Get Users");
            return View(users);
        }


        //TODO

        [HttpGet]
        public async Task<IActionResult> UserById(string id)
        {
            var users = await _request.GetAll($"api/User/{id}");
            return View("UserDetails",users);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(string Email , string Paswoord)
        {
            var result = await _request.Login(Email, Paswoord);
            return View(result);
        }


        public ActionResult UserProfile()
        {

            //should display Student or Tutor
            User user = new User
            {
                Email = "test@gmail.com",
                UserName = "Test#20",
                FirstName = "Test ",
                LastName = "Test",
                Role = "Stuendt",
                City = "Cairo",
                PhoneNumber = "01142647033",
                Age = 22,
                
            };
            //user.ImagePath = "icon-5359554_640.png";
            return View(user);
        }
    }
}
