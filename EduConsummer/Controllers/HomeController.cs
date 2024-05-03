namespace TestUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        List<Tutor> _list;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _list = new List<Tutor>
            {
                    new Tutor { Id = 1 ,  Name = "Mohamed Abdulaziz",  Descrp  = "Mohamed is an experienced math tutor with a passion for teaching algebra, calculus, and geometry.",
                        Field = "Mathematics", ImageSrc = "Male-tutor-1440x960.jpeg" },
                    new Tutor { Id = 2 ,  Name = "Mohamed El-Hoseny",  Descrp  = "Mohamed specializes in physics tutoring, covering topics such as mechanics, thermodynamics, and electromagnetism.",
                        Field = "Physics", ImageSrc = "201710_students_Johannsen_Shane_Tutoring_02.jpg" },
                    // Add more items as needed
            };
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
            // get from API List of Tutor which Fields = Field.Id, give the function to take id parameter
            return View(_list);
        }

        
        public IActionResult TutorPageIndividual(int id)
        {
            var t = _list.FirstOrDefault(x => x.Id == id);
            return View(t);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }

    
}
