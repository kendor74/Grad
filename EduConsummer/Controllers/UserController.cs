﻿namespace TestUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IApiRequest<User> _request;

        public UserController(IApiRequest<User> request)
        {
            _request = request;
        }

        //TODO

        User user = new User
        {
            Email = "Mohame@gmail.com",
            UserName = "Mohamed#20",
            FirstName = "Mohamed ",
            LastName = "Abd-Allateaf",
            Role = "Graduated",
            City = "Cairo",
            Gender = "Male",
            PhoneNumber = "01142647033",
            Age = 22,

        };

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            //yF&_SkNf577.R.a

            //TODO
            //work on generating exact number depending on each role 
            user.UserName = user.FirstName + "#1";
            
            var result = await _request.Post("api/User/Register", user);

            return (result != null ? RedirectToAction("/User/UserProfile",result) : RedirectToAction("Home", "SignUp"));
        }




        [HttpGet]
        public async Task<IActionResult> Users()
        {
            var users = await _request.GetAll("api/User/Get Users");
            return View(users);
        }


        //TODO

        //[HttpGet]
        //public async Task<IActionResult> UserByEmail(string Email)
        //{
        //    var users = await _request.GetByEmail($"api/User/{Email}");
        //    return View("UserDetails", users);
        //}

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

        public async Task<ActionResult> Logout()
        {
            var result = await _request.Logout();
            return ((result != "Error")?RedirectToAction("Index", "Home"):RedirectToAction("Error","Home"));
        }

        public ActionResult UserProfile()
        {
            //calling Api User Services


            //should display Student or Tutor
            user.LastName = "Abd-Allateaf Mahmoud";
            
                if (user.Gender == "Female")
                    user.ImagePath = "FemaleIcon.png";
                else
                    user.ImagePath = "MaleIcon.png";

            

            return View(user);
        }

        [HttpGet]
        public ActionResult EditProfile()
        {
            

            ViewBag.Path = "";
            user.ImagePath = "MaleIcon.png";
            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult>EditProfile(User user) 
        {
            if (user.Image != null && user.Image.Length > 0)
            {
                // Process the uploaded file here
                var fileName = Path.GetFileName(user.Image.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);
                // Additional processing logic
                user.ImagePath = path;
               
            }

            var result = await _request.Put("api/User/Edit Profile", user);
            if (ModelState.IsValid && result != null)
            {
                return RedirectToAction("UserProfile", "User", result); // Redirect to a different action after processing
            }

            // Handle the case where no file was selected
            ModelState.AddModelError("File", "Please select a file");
            return View("UserProfile");
        }
    }   
}
