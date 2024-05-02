namespace TestUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();

        }   
        
        public IActionResult Chat()
        {
            return View();
        }

     

        public IActionResult LiveStream()
        {
            return View();
        }

        public IActionResult LiveStreamRoom()
        {
            return View();
        }
        
        
        public IActionResult ExitTheStream()
        {
            return RedirectToAction("LiveStreamRoom");
        }
        



        public IActionResult Tutor()
        {
            List<Tutor> dataList = new List<Tutor>
        {
            new Tutor { Name = "Mohamed Abdulaziz",  Descrp  = "Mohamed is an experienced math tutor with a passion for teaching algebra, calculus, and geometry.",
                Field = "Mathematics",           ImageSrc = "Male-tutor-1440x960.jpeg" },
            new Tutor { Name = "Mohamed El-Hoseny", Descrp  = "Mohamed specializes in physics tutoring, covering topics such as mechanics, thermodynamics, and electromagnetism.",
                Field = "Physics",     ImageSrc = "201710_students_Johannsen_Shane_Tutoring_02.jpg" },
            //new Tutor { Name = "Orange", Descrp  = "Yet another fruit", Field = "Fruit", ImageSrc = "orange.jpg" }
            // Add more items as needed
        };


            return View(dataList);
        }

        public IActionResult TutorPageIndividual()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }

    
}
