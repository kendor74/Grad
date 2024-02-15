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


        [HttpPost]
        public ActionResult UploadFile(User model)
        {
            if (model.Image != null && model.Image.Length > 0)
            {
                // Process the uploaded file here
                var fileName = Path.GetFileName(model.Image.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    model.Image.CopyTo(stream);
                }

                // Additional processing logic

                ViewBag.Path = "uploads/" + fileName;
                return RedirectToAction("UserProfile", "User"); // Redirect to a different action after processing
            }

            // Handle the case where no file was selected
            ModelState.AddModelError("File", "Please select a file");
            return View("UserProfile");
        }




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
            return View("UserDetails", users);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(string Email, string Paswoord)
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
                Gender = "Female",
                PhoneNumber = "01142647033",
                Age = 22,

            };
            ViewBag.Path = "";
            //user.ImagePath = "icon-5359554_640.png";
            return View(user);
        }

        [HttpGet]
        public ActionResult EditProfile()
        {
            User user = new User
            {
                Email = "test@gmail.com",
                UserName = "Test#20",
                FirstName = "Test ",
                LastName = "Test",
                Role = "Stuendt",
                City = "Cairo",
                Gender = "Female",
                PhoneNumber = "01142647033",
                Age = 22,

            };
            ViewBag.Path = "";
            //user.ImagePath = "icon-5359554_640.png";
            return View(user);
        }
    }   
}
