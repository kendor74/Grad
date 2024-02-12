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
            ViewBag.Error("Creation Denied");
            var result = await _request.Post("api/User/Register", user);

            return (result != null ? RedirectToAction("/Home/Users") : RedirectToAction("Home", "SignUp"));
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
            return View();
        }
    }
}
